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
        /// <summary>
        /// Determines whether [is admin register] [the specified account model].
        /// </summary>
        /// <param name="accountModel">The account model.</param>
        /// <returns></returns>
        Task<bool> IsAdminRegister(AccountModel accountModel);

        /// <summary>
        /// Userses this instance.
        /// </summary>
        /// <returns>users</returns>
        IList<AccountModel> Users();

        /// <summary>
        /// Advances the users.
        /// </summary>
        /// <returns>Advanced Users</returns>
        Dictionary<string, int> AdvanceUsers();

        /// <summary>
        /// Determines whether [is remove user] [the specified identifier].
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        Task<bool> IsRemoveUser(int Id);

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
        Task<IList<AccountModel>> checkAsync(string typeOfUser);
    }
}
