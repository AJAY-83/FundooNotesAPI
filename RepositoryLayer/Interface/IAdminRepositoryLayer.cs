using CommonLayer.Model;
using CommonLayer.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
   public interface IAdminRepositoryLayer
    {
        Task<bool> IsAdminRegister(AccountModel accountModel);
        IList<AccountModel> Users();

        IList<AccountModel> AdvanceUsers();

        Task<bool> IsRemoveUser(int Id);

        IList<NotesModel> UsersWithNotes(int Id);

        Task<AdminLoginRequest> AdminLogin(AdminLogin adminModel);
    }
}
