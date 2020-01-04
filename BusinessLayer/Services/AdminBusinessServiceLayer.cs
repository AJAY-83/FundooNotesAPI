using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdminBusinessServiceLayer:IAdminBusinessLayer
    {
        private readonly IAdminRepositoryLayer adminRepositoryLayer;

        public AdminBusinessServiceLayer(IAdminRepositoryLayer adminRepositoryLayer)
        {
            this.adminRepositoryLayer = adminRepositoryLayer;
        }

        public async Task<bool> AdminRegistration(AccountModel model)
        {
            var result = await this.adminRepositoryLayer.IsAdminRegister(model);
            return result;
        }

        public IList<AccountModel> Users()
        {
            var result = this.adminRepositoryLayer.Users();
            return result;
        }

        public IList<AccountModel> AdvanceUsers()
        {
            var result = this.adminRepositoryLayer.AdvanceUsers();
            return result;
        }
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
    }
}
