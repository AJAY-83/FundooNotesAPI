using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectionBusinessLayer.IElectionBL;
using ElectionModelLayer.ElectionModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsituencyController : ControllerBase
    {
        private readonly IConsituency consituency;
        public ConsituencyController(IConsituency consituency)
        {
            this.consituency = consituency;
        }

        string message = "";
        string status = "";

        [HttpPost]
        public async Task<IActionResult> AddConsituency(ConsituencyModel consituencyModel)
        {
            try
            {
                var data = await this.consituency.AddConsituenct(consituencyModel);

                if (data != null)
                {
                    message = "Consituency Added Successfully";
                    status = "True";
                    return Ok(new { status, message, data });
                }
                else {
                    message = "Consituency Not Added";
                    status = "False";
                   return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsituency(int Id)
        {
            try
            {
                var data = await this.consituency.DeleteConsituency(Id);

                if (data == true)
                {
                    message = "Consituency Remove Successfully";
                    status = "True";
                    return Ok(new { status, message });
                }
                else
                {
                    message = "Consituency Not Remove";
                    status = "False";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsituency(ConsituencyModel consituencyModel)
        {
            try
            {
                var data = await this.consituency.UpdateConsituency(consituencyModel);

                if (data != null)
                {
                    message = "Consituency Updated Successfully";
                    status = "True";
                    return Ok(new { status, message,data });
                }
                else
                {
                    message = "Consituency Not Updated";
                    status = "False";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult DisplayConsituency()
        {
            try
            {
                var data =  this.consituency.DisplayConsituency();

                if (data != null)
                {
                    message = "List Of Consituencies";
                    status = "True";
                    return Ok(new { status, message,data });
                }
                else
                {
                    message = " Consituency Not Displayed";
                    status = "False";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}