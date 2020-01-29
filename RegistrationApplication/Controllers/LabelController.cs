// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// -------------------------------------------------------------------------------------------------------------------- 
namespace FundooNotesAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using BusinessLayer.Services;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {

        /// <summary>
        /// The labelbusinesslayer reference of the business layer
        /// </summary>
        private ILabelBusinessLayer labelbusinesslayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelbusinesslayer">The labelbusinesslayer.</param>
        public LabelController(ILabelBusinessLayer labelbusinesslayer)
        {
            this.labelbusinesslayer = labelbusinesslayer;
        }

        string message = "";
        string status = "";
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelmodel">The labelmodel.</param>
        /// <returns>add label or not</returns>
        [HttpPost]
        public async Task<IActionResult> AddLabel(LabelModel addLabel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
                var data = await labelbusinesslayer.AddLabel(addLabel, UserId);
                if (data == true)
                {
                     message = "Label has been added successfully";
                     status = "True";
                    return Ok(new { status, message, addLabel });
                }
                else
                {
                     message = "Label not added";
                     status = "False";
                    return Ok(new { status, message });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>update or not updated string</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateLabel(LabelModel labelModel)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            labelModel.UserId = UserId;
            var data = await labelbusinesslayer.UpdateLabel(labelModel,UserId);

            if (data == true)
            {
                 message = "Label has been Updated";
                status = "True";
                return Ok(new { status, message, labelModel });
            }
            else {
                 message = "Label is not Updated";
                status = "False";
                return Ok(new { status, message });
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>delete or not delete string</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteLabel(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status = await this.labelbusinesslayer.DeleteLabel(Id, UserId);
            if (status == true)
            {
                string messagae = "Lable is deleted successfully";
                return Ok(new { status, messagae });
            }
            else
            {
                string messagae = "Lable is not deleted";
                return Ok(new { status, messagae });
            }
        }

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display the record</returns>
        [HttpGet]
        public IActionResult Display(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var result = this.labelbusinesslayer.Display(UserId);
            return Ok(new { result, Id, UserId });
        }

        [HttpGet("Search")]
        public IActionResult IsSearch(string input)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = this.labelbusinesslayer.IsSearched(input, UserId);
            return Ok(new { data });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("List")]
        public async Task<IActionResult> LabelList(List<string> labels,int NoteId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);

                var result = await this.labelbusinesslayer.InsertListOFLabels(labels, UserId, NoteId);

                return Ok(new { result });
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}