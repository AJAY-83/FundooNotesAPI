using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AdminRepositoryLayer : IAdminRepositoryLayer
    {
        private readonly AuthenticationContext authentication;
        public AdminRepositoryLayer(AuthenticationContext authentication)
        {
            this.authentication = authentication;
        }
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
                    TypeOfUser = accountModel.TypeOfUser
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

        public async Task<bool> AdminLogin(AdminLogin adminModel)
        {
            var data =   this.authentication.UserAccountTable.Where(table => table.Email == adminModel.Email && table.Password==adminModel.Password).SingleOrDefault();

            if (data != null)
            {
                if (data.TypeOfUser == "Admin" || data.TypeOfUser == "admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
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
                if (line.TypeOfUser == "basic" || line.TypeOfUser == "Basic")
                {
                    users.Add(line);
                }
            }
            return users;
        }

        public IList<AccountModel> AdvanceUsers()
        {
            List<AccountModel> users = new List<AccountModel>();

            foreach (var line in this.authentication.UserAccountTable)
            {
                if (line.TypeOfUser == "advance" || line.TypeOfUser == "Advance")
                {
                    users.Add(line);
                }
            }
            return users;
        }

        public async Task<bool> IsRemoveUser(int UserId)
        {
       try { 
                
            foreach (var Id in this.authentication.UserAccountTable)
            {
                var data =   this.authentication.UserAccountTable.Where(u => u.Id == UserId ).FirstOrDefault();
                if (data != null)
                {
                   var result=  this.authentication.UserAccountTable.Remove(data);
                   await this.authentication.SaveChangesAsync();
                    if (result != null)
                    {
                        return true;
                    }
                    {
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
    }
}
