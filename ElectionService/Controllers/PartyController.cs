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
    public class PartyController : ControllerBase
    {
        private readonly IPartyBL partyBL;
        public PartyController(IPartyBL partyBL)
        {
            this.partyBL = partyBL;
        }

        private string status="";
        private string message = "";

        [HttpPost]
        public async Task<IActionResult> AddParty(PartyModel partyModel)
        {
            try
            {
                var data = await this.partyBL.AddParty(partyModel);

                if (data != null)
                {
                    status = "True";
                    message = "Party Added Suceessfully";
                    return Ok(new { status, message, data });
                }
                else
                {

                    status = "False";
                    message = "Party Not Added";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpDelete]
        public async  Task<IActionResult> DeleteParty(int Id)
        {
            try
            {
                var data = await this.partyBL.DeleteParty(Id);

                if (data == true)
                {
                    status = "True";
                    message = "Party Deleted Suceessfully";
                    return Ok(new { status, message, data });
                }
                else
                {
                    status = "False";
                    message = "Party Not Deleted";
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