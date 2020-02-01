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
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateBL candidateBL;
        public CandidateController(ICandidateBL candidateBL)
        {
            this.candidateBL = candidateBL;
        }

        private string status = "";
        private string message = "";

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddCandidate(CandidateModel candidateModel)
        {
            try
            {
                var data =await this.candidateBL.AddCandidate(candidateModel);
                if (data != null)
                {
                    status = "True";
                    message = "Candidate Added Successfully";

                    return Ok(new { status, message, data });
                }
                else
                {
                    status = "False";
                    message = "Candidate Not Added";
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
        public async Task<IActionResult> DeleteCandidate(int Id)
        {
            try
            {
                var data = await this.candidateBL.DeleteCandidate(Id);
                if (data != null)
                {
                    status = "True";
                    message = "Candidate Deleted Successfully";

                    return Ok(new { status, message, data });
                }
                else
                {
                    status = "False";
                    message = "Candidate Not Deleted";
                    return BadRequest(new { status, message });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("")]
        public IActionResult DisplayCandidate()
        {
            try
            {
                var data =  this.candidateBL.DisplayCandidates();
                if (data != null)
                {
                    status = "True";
                    message = "Candidate Display Successfully";

                    return Ok(new { status, message, data });
                }
                else
                {
                    status = "False";
                    message = "Candidate Not Displayed";
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