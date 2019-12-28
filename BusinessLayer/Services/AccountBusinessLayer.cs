// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountBusinessLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Services
{
    using BusinessLayer.Interface;
    using CommonLayer.Constance;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// AccountBusinessLayer class have the validation part 
    /// as well as it will check the values or data
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IAccountBusinessLayer" />
    public class AccountBusinessLayer : IAccountBusinessLayer
    {
        /// <summary>
        /// The account is the reference of the IAccountRepositoryLayer
        /// because flow will go inside the Repository Layer
        /// </summary>
        private IAccountRepositoryLayer account;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBusinessLayer"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountBusinessLayer(IAccountRepositoryLayer account)
        {
            this.account = account;
        }

        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>registration </returns>
        public async Task<bool> Registration(AccountModel model)
        {
            if (model != null)
            {
                var result = await account.Registration(model);

                return result;
            }
            else {
                return false;
            }
        }
        //// redirect url is 
        //// https://localhost:44381/signin-google
        /// <summary>
        /// Logins the specified loginmodel.
        /// </summary>
        /// <param name="loginmodel">The loginmodel.</param>
        /// <returns>login user </returns>
        public async Task<string> Login(LoginModel loginmodel)
        {
            if (loginmodel != null)
            {
                var result = await account.Login(loginmodel);
                return result;
            }
            else
            {
                return ErrorMessages.emptymodel;
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>forget </returns>
        public  async Task<string> ForgetPassword(ForgetPasswordModel passwordModel)
        {
            if (passwordModel != null)
            {
                var result = account.ForgetPassword(passwordModel);
                return result;
            }
            else {
                return ErrorMessages.emptymodel;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetmodel">The resetmodel.</param>
        /// <returns>reset password</returns>
        public async Task<bool> ResetPassword(ResetPasswordModel resetmodel)
        {
            try
            {
                if (resetmodel != null)
                {
                    var result = account.ResetPassword(resetmodel);
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>change Profile picture</returns>
        public async Task<bool> ProfilePicture(int Id, IFormFile file)
        {
            if (Id > 0)
            {
                var result = await this.account.ProfilePicture(Id,file);
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginWithGoogle(bool IsGoogle, int UserId)
        {
            var result = await this.account.IsLoginWithGoogle(IsGoogle,UserId);
            return result;
        }
    }
}
