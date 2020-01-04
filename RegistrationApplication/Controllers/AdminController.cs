using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusinessLayer adminBusinessLayer;
        public AdminController(IAdminBusinessLayer adminBusinessLayer)
        {
            this.adminBusinessLayer = adminBusinessLayer;
        }
        string status = "";

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

        [HttpGet]
        [Route("GetUserByNotes")]
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

        }
    }