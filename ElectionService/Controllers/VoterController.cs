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
    public class VoterController : ControllerBase
    {
        private readonly IVoterBusinessLayer voterBusinessLayer;
        public VoterController(IVoterBusinessLayer voterBusinessLayer)
        {
            this.voterBusinessLayer = voterBusinessLayer;
        }
        private string status = "";
        private string message = "";

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddVoter(VoterModel voterModel)
        {
            try {
                var data = await this.voterBusinessLayer.AddVoter(voterModel);
                if (data != null)
                {
                  
                    return Ok(new { status = "True", message = "Voter Data Added Successfully", data });
                }
                else {
                    status = "False";
                    message = "Voter Data Not Added";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteVoter(int Id)
        {
            try
            {
                var data = await this.voterBusinessLayer.DeleteVoter(Id);
                if (data != null)
                {
            
                    return Ok(new { status = "True", message = "Voter Data Added Successfully", data });
                }
                else
                {
                 
                    return BadRequest(new { status = "False", message = "Voter Data Not Added" });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("VotesConsituencyWise")]
        public IActionResult CountVoter(int Id)
        {
            try
            {
                var data =  this.voterBusinessLayer.ConstituencyWise(Id);
                if (data != null)
                {
                   
                    return Ok(new { status = "True", message = "Voters Count", data });
                }
                else
                {
                   
                    return BadRequest(new { status = "False", message = " Not Added" });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("PartyCandidates")]
        public IActionResult PartyCandidates(string state)
        {
            try
            {
                var data = this.voterBusinessLayer.PartyWise(state);
                if (data != null)
                {
                  
                    return Ok(new { status = "True", message = "Party Candidates ", data });
                }
                else
                {

                    return BadRequest(new { status = "False", message = " Not Added" });
               
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}