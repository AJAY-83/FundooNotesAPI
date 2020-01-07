using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Request;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdminBusinessServiceLayer:IAdminBusinessLayer
    {
        /// <summary>
        /// The admin repository layer
        /// </summary>
        private readonly IAdminRepositoryLayer adminRepositoryLayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminBusinessServiceLayer"/> class.
        /// </summary>
        /// <param name="adminRepositoryLayer">The admin repository layer.</param>
        public AdminBusinessServiceLayer(IAdminRepositoryLayer adminRepositoryLayer)
        {
            this.adminRepositoryLayer = adminRepositoryLayer;
        }

        /// <summary>
        /// Admins the registration.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<bool> AdminRegistration(AccountModel model)
        {
            var result = await this.adminRepositoryLayer.IsAdminRegister(model);
            return result;
        }

        /// <summary>
        /// Userses this instance.
        /// </summary>
        /// <returns></returns>
        public IList<AccountModel> Users()
        {
            var result = this.adminRepositoryLayer.Users();
            return result;
        }

        /// <summary>
        /// Advances the users.
        /// </summary>
        /// <returns></returns>
        public IList<AccountModel> AdvanceUsers()
        {
            var result = this.adminRepositoryLayer.AdvanceUsers();
            return result;
        }

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> RemoveUser(int Id)
        {
            try
            {

                var result = await this.adminRepositoryLayer.IsRemoveUser(Id);
                return result;
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
        /// <exception cref="Exception"></exception>
        public IList<NotesModel> UsersWithNotes(int Id)
        {
            try
            {

                var result =  this.adminRepositoryLayer.UsersWithNotes(Id);
                return result;
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
        public async Task<AdminLoginRequest> AdminLogin(AdminLogin adminModel)
        {
           
                var result = await this.adminRepositoryLayer.AdminLogin(adminModel);
                return result;
           
        }
    }
}
