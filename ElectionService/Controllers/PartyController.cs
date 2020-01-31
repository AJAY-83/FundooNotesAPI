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
        public IActionResult AddParty(PartyModel partyModel)
        {
            var data = this.partyBL.AddParty(partyModel);

            if (data != null)
            {
                status = "True";
                message = "Party Added Suceessfully";
                return Ok(new { status, message, data });
            }
            else {

                status = "Falsse";
                message = "Party Not Added";
                return BadRequest(new { status, message });
            }

        }
    }
}