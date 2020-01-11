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

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelmodel">The labelmodel.</param>
        /// <returns>add label or not</returns>
        [HttpPost]        
        public async Task<IActionResult> AddLabel( LabelModel addLabel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
                var status = await labelbusinesslayer.AddLabel(addLabel, UserId);
                if (status == true)
                {
                    string message = "Label has been added successfully";
                    return Ok(new { status, message, addLabel });
                }
                else
                {
                    string message = "Label not added";
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
            var status = await labelbusinesslayer.UpdateLabel(labelModel);
           
            if (status != null)
            {
                string message = "Label has been Updated";
                return Ok(new { status,message,labelModel });
            }
            else {
                string message = "Label is not Updated";
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
            if (status ==true)
            {
                string messagae = "Lable is deleted successfully";
                return Ok(new { status, messagae });
            }
            else
            {
                string messagae = "Lable is not deleted";
                return Ok(new { status , messagae });
            }
        }

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display the record</returns>
        [HttpGet]        
        public IActionResult Display( int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var result = this.labelbusinesslayer.Display(UserId);
            return Ok(new { result ,Id,UserId});
        }
    }
}