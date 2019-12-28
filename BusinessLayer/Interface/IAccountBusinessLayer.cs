// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAccountBusinessLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interface
{
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// IAccountBusinessLayer is the Interface to perform the Registration operations
    /// </summary>
    public interface IAccountBusinessLayer
    {
        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> Registration(AccountModel model);

        /// <summary>
        /// Logins the specified loginmodel.
        /// </summary>
        /// <param name="loginmodel">The loginmodel.</param>
        /// <returns></returns>
        Task<string> Login(LoginModel loginmodel);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns></returns>
       Task<string> ForgetPassword(ForgetPasswordModel passwordModel);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetmodel">The resetmodel.</param>
        /// <returns></returns>
        Task<bool> ResetPassword(ResetPasswordModel resetmodel);

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>Profile uploaded or not</returns>
        Task<bool> ProfilePicture(int id, IFormFile file);

        Task<bool> LoginWithGoogle(bool IsGoogle,int UserId);
    }
}
