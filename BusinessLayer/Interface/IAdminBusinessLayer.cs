using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface IAdminBusinessLayer
    {
        Task<bool> AdminRegistration(AccountModel model);

        IList<AccountModel> Users();

        IList<AccountModel> AdvanceUsers();

        Task<bool> RemoveUser(int Id);

        IList<NotesModel> UsersWithNotes(int Id);

    }
}
