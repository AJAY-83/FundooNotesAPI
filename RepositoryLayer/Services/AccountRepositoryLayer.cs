// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using CommonLayer.Model;
    using RepositoryLayer.Interface;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
    using RepositoryLayer.Context;
    using System.Linq;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Collections.Generic;
    using System.Text;
    using System.Messaging;
    using CommonLayer.MSMQ;
    using System.Reflection.Metadata;
    using Microsoft.VisualBasic;
    using CommonLayer.Constance;
    using Microsoft.AspNetCore.Http;
    using CloudinaryDotNet;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// AccountRepositoryLayer is class which Inherited from the IAccountRepositoryLayer
    /// </summary>
    public class AccountRepositoryLayer : IAccountRepositoryLayer
    {
        /// <summary>
        /// authentication is the reference of the AuthenticationContext
        /// it is service it perform db related connection operations 
        /// </summary>
        private readonly AuthenticationContext authentication;

        /// <summary>
        /// The configuraion
        /// creating the instance of the  IConfiguration Interface 
        /// it is provides us the configuration with the AppSetting
        /// </summary>
        private readonly IConfiguration configuraion;
        public AccountRepositoryLayer(AuthenticationContext authentication, IConfiguration configuraion)
        {
            this.authentication = authentication;
            this.configuraion = configuraion;
        }

        /// <summary>
        /// Registration method is use to get data from user and perform add data operation into database
        /// </summary>
        /// <param name="model">registration done or not</param>
        /// <returns></returns>
        public async Task<bool> Registration(AccountModel model)
        {
            try
            {
               var  users = new AccountModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNumber = model.MobileNumber,
                    Email = model.Email,
                    Password=model.Password,
                    TypeOfUser=model.TypeOfUser,
                    IsFacebook=model.IsFacebook,
                    IsGoogle=model.IsGoogle
                };

                //// checking the email id is already exists 
                //// if exist he doen't register
                bool UsernameExists = authentication.UserAccountTable.Any(x => x.Email == model.Email);

                if (UsernameExists)
                    return false;

                this.authentication.UserAccountTable.Add(users);
                var result = await this.authentication.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Login checking here or also checks the email id already exists or not 
        /// if exists give the message to the user "Email already exist"
        /// </summary>
        /// <param name="user">user</param>
        /// <returns>Message</returns>
        public async Task<string> Login(LoginModel user)
        {
            if (user == null)
            {
                return ErrorMessages.Invaliduserrequest;
            }

            bool IsValidUser = authentication.UserAccountTable.Any(x => x.Email == user.Email && x.Password==user.Password);
            var row = authentication.UserAccountTable.Where(u => u.Email == user.Email).FirstOrDefault();

            if (IsValidUser)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuraion["SecretKey:Key"]));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                     {
                      new Claim("Id",row.Id.ToString()),
                      new Claim("Email", user.Email),
                      new Claim(ClaimTypes.Role, "User")           
                      };

                var tokeOptions = new JwtSecurityToken(                 
                   claims: claims,
                   expires: DateTime.Now.AddDays(1),
                   signingCredentials: signinCredentials
               );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return  tokenString;
            }
                else
                {
                   return ErrorMessages.InvalidUser;
                }
        }

        /// <summary>
        /// forget password
        /// </summary>
        /// <param name="passwordModel">passwordModel</param>
        /// <returns>string</returns>
        public string ForgetPassword(ForgetPasswordModel passwordModel)
        {
            MSMQ sendmsmq = new MSMQ();
            if (passwordModel == null)
            {
                return ErrorMessages.Invaliduserrequest;
            }

            //// checking the the email is present in database or not 
            bool emailChecking = authentication.UserAccountTable.Any(x => x.Email == passwordModel.Email);
            if (!emailChecking)
            {
                return ErrorMessages.InvalidUsername;
            }
            else {
                ////SymmetricSecurityKye means both end have the same key
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuraion["SecretKey:Key"]));

                //// this is the user credientials like username and password
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                //// stores the calims into the List
        var claims = new List<Claim>
            {
              // new Claim(ClaimTypes.Email, passwordModel.Email),
              new Claim("email", passwordModel.Email),
              new Claim(ClaimTypes.Role, "Forget Password")
           };
                //// stores the JwtSecurityToken properties 
                var tokeOptions = new JwtSecurityToken(
                   
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(5),
                   signingCredentials: signinCredentials
               );
                	
               //// Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
                var tokenString =  new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                sendmsmq.SendTokenQueue(passwordModel.Email,tokenString);
                return tokenString;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(ResetPasswordModel resetPassword)
        {
            var stream = resetPassword.Token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "email").Value;
            bool emailexist = authentication.UserAccountTable.Any(x => x.Email == email);
           //  var original = authentication.UserAccountTable.Find(email);

            if (emailexist)
            {
            // OperationDataContext OdContext = new OperationDataContext();
                //Get Single course which need to update  
                var passwordchange = authentication.UserAccountTable.SingleOrDefault(x =>x.Email  == email);
                
                ////Field which will be update  
                 passwordchange.Password=resetPassword.Password;

                //// executes the appropriate commands to implement the changes to the database  
                var result =authentication.SaveChanges();
                

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>
        /// uploaded or not
        /// </returns>
        public async Task<bool> ProfilePicture(int Id, IFormFile file)
        {
            ImageUpload cloudiNary = new ImageUpload();
            Account account = new Account(cloudiNary.CLOUD_NAME, cloudiNary.API_KEY, cloudiNary.API_SECCRET_KEY);
            cloudiNary.cloudinary = new Cloudinary(account);

            ////var image = (from notes in this.authenticationContext.Notes;
            var image = (from Notes in authentication.UserAccountTable select Id).FirstOrDefault();


            bool idexist = authentication.UserAccountTable.Any(x => x.Id == Id);
            if (idexist)
            {
                var data = this.authentication.UserAccountTable.SingleOrDefault(u => u.Id == Id);
                data.Image = cloudiNary.UploadImage(file);
                var result = await authentication.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsLoginWithGoogle(bool IsGoogle, int UserId)
        {

            //// checking the notes Id and UserId is Availabel or not into the databse
            var data = this.authentication.UserAccountTable.SingleOrDefault(u => u.Id == UserId);

            //// checks that is null or  not null
            if (data.IsGoogle == false)
            {
                ///// this is for login
                data.IsGoogle = true;
                var result = await authentication.SaveChangesAsync();
                return true;
            }
            else
            {
                //// this is for logout
                data.IsGoogle = false;
                var result = await authentication.SaveChangesAsync();
                return false;
            }
        }
    }
}
