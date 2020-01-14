using CommonLayer.Model;
using CommonLayer.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AdminRepositoryLayer : IAdminRepositoryLayer
    {
        /// <summary>
        /// The authentication
        /// </summary>
        private readonly AuthenticationContext authentication;

        private readonly IConfiguration configuraion;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRepositoryLayer"/> class.
        /// </summary>
        /// <param name="authentication">The authentication.</param>
        public AdminRepositoryLayer(AuthenticationContext authentication, IConfiguration configuraion)
        {
            this.authentication = authentication;
            this.configuraion = configuraion;
        }

        /// <summary>
        /// Determines whether [is admin register] [the specified account model].
        /// </summary>
        /// <param name="accountModel">The account model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> IsAdminRegister(AccountModel accountModel)
        {

            try
            {
                var users = new AccountModel()
                {
                    FirstName = accountModel.FirstName,
                    LastName = accountModel.LastName,
                    MobileNumber = accountModel.MobileNumber,
                    Email = accountModel.Email,
                    Password = accountModel.Password,
                    TypeOfUser = "Admin"
                };

                var emailchecking = this.authentication.UserAccountTable.Where(data => data.Email == accountModel.Email).FirstOrDefault();
                if (emailchecking == null)
                {
                    return false;
                }
                //// checking the email id is already exists 
                //// if exist he doen't register
                bool UsernameExists = authentication.UserAccountTable.Any(x => x.Email == accountModel.Email);

                if (UsernameExists)
                {
                    return false;
                }

                this.authentication.UserAccountTable.Add(users);
                var result = await this.authentication.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="adminModel">The admin model.</param>
        /// <returns></returns>
        public async Task<AdminLoginRequest> AdminLogin(AdminLogin adminModel )

        {
           
                AdminLoginRequest adminLoginRequest = new AdminLoginRequest();
                var data = this.authentication.UserAccountTable.Where(table => table.Email == adminModel.Email && table.Password == adminModel.Password).SingleOrDefault();
                var row = authentication.UserAccountTable.Where(u => u.Email == adminModel.Email).FirstOrDefault();

                if (data != null)
                {
                    if (data.TypeOfUser == "Admin" || data.TypeOfUser == "admin")
                    {

                        //var users = new NotesModel()
                        //{
                        //    Title = noteAddRequest.Title,
                        //    Content = noteAddRequest.Content,
                        //    UserId = UserId,
                        //    CreatedDate = DateTime.Now,
                        //    ModifiedDate = DateTime.Now,
                        //    Color = noteAddRequest.Color,
                        //    Reminder = null,
                        //    IsActive = false,
                        //    IsTrash = false,
                        //    IsPin = false,
                        //    IsArchive = false,
                        //    IsNotes = false
                        //};

                        var response = new AdminLoginRequest()
                        {
                            Id = data.Id,
                            FullName = data.FirstName,
                            Email = data.Email
                          

                        };
                        return response;
                    }
                }
            
            return null;
        }

        /// <summary>
        /// Userses this instance.
        /// </summary>
        /// <returns></returns>
        public IList<AccountModel> Users()
        {
            List<AccountModel> users = new List<AccountModel>();

            foreach (var line in this.authentication.UserAccountTable)
            {
                if (line.Services == "basic" || line.TypeOfUser == "Basic" || line.TypeOfUser=="Advance"||line.TypeOfUser=="advance")
                {
                    users.Add(line);
                }
            }
            return users;
        }

        /// <summary>
        /// Advances the users.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,int> AdvanceUsers()
        {
            int basic=0;
            int advance=0;
            Dictionary<string, int> map = new Dictionary<string, int>();
            List<AccountModel> users = new List<AccountModel>();

            foreach (var line in this.authentication.UserAccountTable)
            {
                if (line.Services == "advance" || line.Services == "Advance")
                {
                    advance++;
                }
                else if (line.Services == "basic" || line.Services=="Basic")
                {
                    basic++;
                    
                }
            }
            map.Add("Basic", basic);
            map.Add("Advance", advance);
            return map;
        }

        /// <summary>
        /// Determines whether [is remove user] [the specified user identifier].
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> IsRemoveUser(int UserId)
        {
       try { 
                
            foreach (var Id in this.authentication.UserAccountTable)
            {
                var data =    this.authentication.UserAccountTable.Where(u => u.Id == UserId ).FirstOrDefault();
                if (data != null)
                {
                       var result =  this.authentication.UserAccountTable.Remove(data);
                        await this.authentication.SaveChangesAsync();
                        if (result != null)
                        {
                            return true;
                        }
                        else {
                            return false;
                        }                                         
                }
                else {
                    return false;
                }
            }
            return false;
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Userses the with notes.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public IList<NotesModel> UsersWithNotes(int Id)
        {
            List<NotesModel> users = new List<NotesModel>();

             
            foreach (var line in this.authentication.UserAccountTable)
            {
                if (line.Id==Id)
                {
                    foreach (var note in this.authentication.Notes)
                    {
                         if(line.Id==note.UserId)
                        {
                            users.Add(note);
                        }
                    }
                    
                }
            }
            return users;
        }

        /// <summary>
        /// Checks the asynchronous.
        /// </summary>
        /// <param name="typeOfUser">The type of user.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<AccountModel>> checkAsync(string typeOfUser)
        {
            // var result = string.Join(",", typeOfUser.ToArray());
            string data =  typeOfUser;

            List<AccountModel> note = new List<AccountModel>();
            //// foreach loop to gets the Trashed Fields

            try
            {
                
                    foreach (var users in this.authentication.UserAccountTable)
                    {
                    //// if user clicks on Button without enter any data into textfield it will show all users
                    if (data == null || data==string.Empty)
                    {
                        //// checks the Basic as well as basic
                        if (users.Services == "Basic" || users.Services == "basic" || users.Services == "Advance" || users.Services == "advance")
                        {
                            note.Add(users);
                        }
                    }
                    //// if admin want spacific users like Basic or Advance
                    if (users.Services == data)
                    {
                        note.Add(users);
                    }
                                        
                }
                return note;                                                   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
