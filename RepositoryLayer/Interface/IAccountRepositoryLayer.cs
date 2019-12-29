// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAccountRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------


namespace RepositoryLayer.Interface
{
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IAccountRepositoryLater it is interface perform operation on registration
    /// </summary>
    public interface IAccountRepositoryLayer
    {
        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>registration yes or no</returns>
        Task<bool> Registration(AccountModel model);

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<string> Login(LoginModel loginmodel);

        Task<bool> IsLoginWithGoogle(SocialLoginModel socialLoginModel);
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns></returns>
       string ForgetPassword(ForgetPasswordModel passwordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        Task<bool> ResetPassword(ResetPasswordModel resetmodel);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>uploaded or not</returns>
        Task<bool> ProfilePicture(int Id,IFormFile file);

         Task<bool> IsLoginWithGoogle(bool IsGoogle, int UserId);
    }
}
