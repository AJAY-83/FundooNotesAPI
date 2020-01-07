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
  
    public class AdminController : ControllerBase
    {
        private readonly AuthenticationContext authentication;

        private readonly IConfiguration configuration;


        private readonly IAdminBusinessLayer adminBusinessLayer;
        public AdminController(IAdminBusinessLayer adminBusinessLayer, AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            this.adminBusinessLayer = adminBusinessLayer;
            this.configuration = configuration;
            this.configuration = configuration;
        }
        /// <summary>
        /// The status is use to show the status of the user that is true or false
        /// </summary>
        string status = "";

        /// <summary>
        /// The message show the user proper message for 
        /// </summary>
        string message = "";
        [HttpPost("Admin")]
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
        /// Displays the basic user.
        /// </summary>
        /// <returns></returns>
        [HttpGet("BasicUsers")]
        public IActionResult DisplayBasicUser()
        {
            var result = this.adminBusinessLayer.Users();
            return Ok(new { result });
        }

        [HttpGet("AdvanceUsers")]
        public IActionResult DisplayAdvanceUser()
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
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> RemoveUser(int Id)
        {
            try
            {
                var result = await this.adminBusinessLayer.RemoveUser(Id);
                if (result == true)
                {
                    return Ok(new { result });
                }
                else
                {
                    return BadRequest(new { result });
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
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet("User/{Id}/Notes")]
       
        public IActionResult GetUserByNotes(int Id)
        {
            try
            {
                var result =  this.adminBusinessLayer.UsersWithNotes(Id);
                if (result != null)
                {
                    return Ok(new { result });
                }
                else

                {
                    return BadRequest(new { result });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Admins the login.
        /// </summary>
        /// <param name="adminModel">The admin model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("Login")]
        public async Task<IActionResult> AdminLogin(AdminLogin adminModel)
        {
            try
            {
               
                var data = await this.adminBusinessLayer.AdminLogin(adminModel);
               string token = LoginToken(adminModel);
                if (data != null)
                {
                    status = "True";
                    message = "Admin has been login";
                    return Ok(new {status,message, data });
                }
                else {
                    status = "False";
                    message = "Admin Not Login";
                    return BadRequest(new { status ,message});
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("")]
        public string LoginToken(AdminLogin adminLogin)
        {
            var data = this.authentication.UserAccountTable.Where(table => table.Email == adminLogin.Email && table.Password == adminLogin.Password).SingleOrDefault();
            var row = authentication.UserAccountTable.Where(u => u.Email == adminLogin.Email).FirstOrDefault();
            bool IsValidUser = authentication.UserAccountTable.Any(x => x.Email == adminLogin.Email && x.Password == adminLogin.Password);
            if (data != null)
            {
                if (data.TypeOfUser == "Admin" || data.TypeOfUser == "admin")
                {
                    if (IsValidUser)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey:Key"]));

                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var claims = new List<Claim>
                     {
                      new Claim("Id",row.Id.ToString()),
                      new Claim("Email", adminLogin.Email),
                      new Claim(ClaimTypes.Role, "Admin")
                      };

                        var tokeOptions = new JwtSecurityToken(
                           claims: claims,
                           expires: DateTime.Now.AddDays(1),
                           signingCredentials: signinCredentials
                       );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                        return tokenString;
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
            else
            {
                return "Email or Password is wrong";
            }

        }

    }
}