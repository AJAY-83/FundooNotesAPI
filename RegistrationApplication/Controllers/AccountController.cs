// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooNotesAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using CommonLayer.Request;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// AccountContrtoller have Account name controller to handle the Application
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountBusinessLayer account;





        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountController(IAccountBusinessLayer account)
        {
            this.account = account;
        }

        //[EnableCors]
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("signin-facebook")]
        //public IActionResult SignIn()
        //{
        //    return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        //}

        //[EnableCors]
        //[HttpGet]
        //[AllowAnonymous]
        //[Route("signin-google")]
        //public IActionResult SignInGoogle()
        //{
        //    return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        //}

        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>registration complet</returns>
        [HttpPost("SignUp")] 
        [AllowAnonymous]
        public async Task<IActionResult> Registration(SignUpRequest data)
        {
            var status = await account.Registration(data);

            if (status == true)
             {
                string message = "Registration completed successfully";
                return Ok(new { status, message, data });
             }
            else
            {
                string message = "Not Registered";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Login API  to login the valid user
        /// </summary>
        /// <param name="user">user</param>        
        /// <returns>result</returns>
        // GET api/values
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginModel user)
        {

            var status = await account.Login(user);

            if (status != null)
            {
                string message = "Login Successfully";
                return Ok(new { status, message,user });
            }
            else
            {
                string message = "Not Login";
                return BadRequest(new { status, message }); 
            }
        }

        /// <summary>
        /// Logins the with google.
        /// </summary>
        /// <param name="socialLoginModel">The social login model.</param>
        /// <param name="Url">The URL.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithGoogle(SocialLoginModel socialLoginModel,string Url)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.account.LoginWithGoogle(socialLoginModel);

            if (data == true)
            {
                string message = "Login with Google";
                return Ok(new { data, message });
            }
            else
            {
                string message = "Logout from Google";
                return Ok(new { data, message });
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="passwordModel">The password model.</param>
        /// <returns>result</returns>
        [HttpPost("ForgetPassword")]       
        public IActionResult ForgetPassword(ForgetPasswordModel passwordModel)
        {

            var result = account.ForgetPassword(passwordModel);

            if (result != null)
            {
                string message = "Check your Email ";
                return Ok(new { message, result });
            }
            else
            {
                string message = "Check your Email ";
                return BadRequest( new { message, result });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>result</returns>
        [HttpPut("ResetPassword")]               
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPassword)
        {
            //// var useremail = HttpContext.User.Claims;
           var result = account.ResetPassword(resetPassword);


            if (result != null)
            {
                string message = "Reset Password Successfully";
                return Ok(new { result, message,resetPassword });
            }
            else
            {
                string message = "Not Reset Password";
                return BadRequest(new { result, message });
            }            
        }

        /// <summary>
        /// Profiles the picture.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost("UploadProfile")]                                                     
        public async Task<IActionResult> ProfilePicture( IFormFile file)
        {
            var HostName = Dns.GetHostName();
            int Id = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status=await this.account.ProfilePicture(Id, file);
            if (status != null)
            {
                string message = "Profile Picture has been Uploaded";
                return Ok(new { status, message,file.FileName, HostName });
            }
            else
            {
                string message = "Proifle picture not uploaded";
                return BadRequest(new { status, message });
            }
        }

        //[HttpPost("Logout")]
        //public async Task<ActionResult> Logout()
        //{
        //    await this.signInManager.SignOutAsync();

        //    return Ok();
        //}
    }
}