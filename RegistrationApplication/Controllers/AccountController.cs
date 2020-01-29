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
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;

    /// <summary>
    /// AccountContrtoller have Account name controller to handle the Application
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountBusinessLayer account;

        private readonly AuthenticationContext authentication;

        private readonly IConfiguration configuration;



        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountController(IAccountBusinessLayer account, AuthenticationContext authentication, IConfiguration configuration)
        {
            this.account = account;
            this.authentication = authentication;
            this.configuration = configuration;
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

        string message = "";
        string status = "";
        /// <summary>
        /// Registrations the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>registration complet</returns>
        [HttpPost("SignUp")] 
        [AllowAnonymous]
        public async Task<IActionResult> Registration(SignUpRequest signupdata)
        {
            var data = await account.Registration(signupdata);

            if (data == true)
             {
                 message = "Registration completed successfully";
                 status = "True";
                return Ok(new { status, message, data });
             }
            else
            {
                 message = "Not Registered";
                status = "False";
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

            var data = await account.Login(user);
            var token = LoginToken(user);
            if (data.TypeOfUser != "Admin" || data.TypeOfUser != "admin")
            {
                if (data != null)
                {
                    status = "True";
                    message = "Login Successfully";

                    return Ok(new { status, message, data, token });
                }
                else
                {
                    message = "Not Login";
                    status = "False";
                    return BadRequest(new { status, message });
                }
            }
            else {
                return this.Unauthorized();
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
                 message = "Login with Google";
                 status = "True";
                return Ok(new { status, message, data });
            }
            else
            {
                 status = "False";
                 message = "Logout from Google";
                return Ok(new { status, message });
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

            var data = account.ForgetPassword(passwordModel);

            if (data != null)
            {
                 message = "Check your Email ";
                 status = "True";
                return Ok(new { status,message,data });
            }
            else
            {
                 message = "Check your Email ";
                status = "False";
                return BadRequest( new { status,message });
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
           var data = account.ResetPassword(resetPassword);


            if (data != null)
            {
                 message = "Reset Password Successfully";
                status = "True";
                return Ok(new { status, message,data });
            }
            else
            {
                 message = "Not Reset Password";
                status="False";
                return BadRequest(new { status, message });
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
            var data= await this.account.ProfilePicture(Id, file);
            if (data != null)
            {
                status = "True";
                 message = "Profile Picture has been Uploaded";
                return Ok(new { status, message,file.FileName, HostName });
            }
            else
            {
                status = "False";
                 message = "Proifle picture not uploaded";
                return BadRequest(new { status, message });
            }
        }

        //[HttpPost("Logout")]
        //public async Task<ActionResult> Logout()
        //{
        //    await this.signInManager.SignOutAsync();

        //    return Ok();
        //}


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("LoginToken")]
        public string LoginToken(LoginModel adminLogin)
        {
            try
            {
                // var data =  this.authentication.UserAccountTable.Where(table => table.Email == adminLogin.Email && table.Password == adminLogin.Password).SingleOrDefault();
                var row = authentication.UserAccountTable.Where(u => u.Email == adminLogin.Email).SingleOrDefault();
                bool IsValidUser = authentication.UserAccountTable.Any(x => x.Email == adminLogin.Email && x.Password == adminLogin.Password);

                if (row != null)
                {

                    if (IsValidUser)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey:Key"]));

                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                     {
                      new Claim("Id",row.Id.ToString()),
                      new Claim("Email", adminLogin.Email),
                      new Claim("User",ClaimTypes.Role)
                      };

                        var tokeOptions = new JwtSecurityToken(
                           claims: claims,
                           expires: DateTime.Now.AddDays(1),
                           signingCredentials: signinCredentials
                       );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                        return tokenString.ToString();
                    }
                    else
                    {
                        return "Token Not Generated";
                    }
                }
                else
                {
                    return "Email or Password is wrong";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}