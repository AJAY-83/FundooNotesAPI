using CommonLayer.Model;
using CommonLayer.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
   public interface IAdminBusinessLayer
    {
        /// <summary>
        /// Admins the registration.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> AdminRegistration(AccountModel model);

        /// <summary>
        /// Userses this instance.
        /// </summary>
        /// <returns></returns>
        IList<AccountModel> Users();

        /// <summary>
        /// Advances the users.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, int> AdvanceUsers();

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        Task<bool> RemoveUser(int Id);

        /// <summary>
        /// Userses the with notes.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        IList<NotesModel> UsersWithNotes(int Id);

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="adminModel">The admin model.</param>
        /// <returns></returns>
        Task<AdminLoginRequest> AdminLogin(AdminLogin adminModel);

        /// <summary>
        /// Checks the asynchronous.
        /// </summary>
        /// <param name="typeOfUser">The type of user.</param>
        /// <returns></returns>
        Task<IList<AccountModel>> checkAsync(List<String> typeOfUser);
    }
}
