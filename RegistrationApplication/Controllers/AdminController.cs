using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;

namespace FundooNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly AuthenticationContext authentication;

        private readonly IConfiguration configuration;


        private readonly IAdminBusinessLayer adminBusinessLayer;
        public AdminController(IAdminBusinessLayer adminBusinessLayer, AuthenticationContext authentication, IConfiguration configuration)
        {
            this.adminBusinessLayer = adminBusinessLayer;
            this.configuration = configuration;
            this.authentication = authentication;
        }
        /// <summary>
        /// The status is use to show the status of the user that is true or false
        /// </summary>
        string status = "";

        /// <summary>
        /// The message show the user proper message for 
        /// </summary>
        string message = "";

        /// <summary>
        /// this API for the Admin Registration
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns>smd for data related to the admin</returns>
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminRegistration(AccountModel accountModel)
        {
            var data = await this.adminBusinessLayer.AdminRegistration(accountModel);
            if (data == true)
            {
                status = "True";
                message = "Admin has been  Registered";
                return Ok(new { status, message, data }); ;
            }
            else {
                status = "false";
                message = "Admin Not Registered";
                return Ok(new { status, message, data });
            }
        }

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="adminModel">The admin model.</param>
        /// <returns>adminLogin</returns>     
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin([FromForm] AdminLogin adminLogin)
        {
            try
            {
                 //string Type = User.FindFirst(ClaimTypes.Role)?.ValueType;
              
                
                var data = await this.adminBusinessLayer.AdminLogin(adminLogin);
                string token = LoginToken(adminLogin);
                if (data != null)
                {

                    status = "True";
                    message = "Admin has been login";
                    return Ok(new { status, message, data, token });
                }
                else
                {
                    status = "False";
                    message = "Admin Not Login";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Displays the basic user.
        /// </summary>
        /// <returns>userlist</returns>
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            var result = this.adminBusinessLayer.Users();
            return Ok(new { result });
        }

        /// <summary>
        /// this api give the all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("CountAllUsers")]
        public IActionResult AllUsers()
        {
            var result = this.adminBusinessLayer.AdvanceUsers();
            return Ok(new { result });
        }

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> RemoveUser(int Id)
        {
            try
            {
                var data =  await this.adminBusinessLayer.RemoveUser(Id);
                if (data == true)
                {
                    return Ok(new { data });
                }
                else
                {
                    return BadRequest(new { data });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        /// <summary>
        /// Gets the user by notes.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>id</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("User/{Id}/Notes")]
       
        public IActionResult GetUserByNotes(int Id)
        {
            try
            {
                var data =  this.adminBusinessLayer.UsersWithNotes(Id);
                if (data != null)
                {
                    return Ok(new { data });
                }
                else

                {
                    return BadRequest(new { data });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        /// <summary>
        /// generate the Token for the Admin login
        /// </summary>
        /// <param name="adminLogin"></param>
        /// <returns></returns>
       
        /// <summary>
        /// this is for the user list
        /// </summary>
        /// <param name="usertype"></param>
        /// <returns></returns>
        [HttpGet("AllUsersList")]
        public async Task<IActionResult> checklist(List<string> usertype)
        {
            var data = await this.adminBusinessLayer.checkAsync(usertype);
            if (data != null)
            {
                return Ok(new { data });
            }
            else
            {
                message = "Not Displaying Records";
                return BadRequest(new { message });
            }
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("LoginToken")]
        public string LoginToken(AdminLogin adminLogin)
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
                      new Claim("Admin",ClaimTypes.Role)
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