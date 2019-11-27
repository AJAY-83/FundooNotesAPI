using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using RepositoryLayer.Context;

namespace RepositoryLayer.Services
{
    public class AccountRepositoryLayer : IAccountRepositoryLayer
    {

        private readonly AuthenticationContext authentication;
        public AccountRepositoryLayer(AuthenticationContext authentication)
        {
            this.authentication = authentication;       
        }
        public async Task<string> Registration(AccountModel model)
        {
            try
            {
               var  users = new AccountModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNumber = model.MobileNumber,
                    Email = model.Email,
                    Password=model.Password
                };
                this.authentication.AccountTable.Add(users);
                var result = await this.authentication.SaveChangesAsync();

                if (result > 0)
                {
                    return "Registered Successfully";
                }
                else{
                    return "Not resgistered";
                }
                
                


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        
        }
    }
}
