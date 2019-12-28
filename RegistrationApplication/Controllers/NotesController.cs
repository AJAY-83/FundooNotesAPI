// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
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
    using CommonLayer.Request;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Notes Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private INotesBusinessLayer notesBusinessLayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesBusinessLayer">The notes business layer.</param>
        public NotesController(INotesBusinessLayer notesBusinessLayer)
        {
            this.notesBusinessLayer = notesBusinessLayer;
        }
        /// <summary>
        /// The message
        /// this message show the controller operation like data insert or not
        /// </summary>
        /// 
        string message = "";
        /// <summary>
        /// The status
        /// status have True and False 
        /// </summary>
        string status = "";

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>Actionresult</returns>
        [HttpPost]
        //[Route("")]
        public async Task<IActionResult> AddNotes( NoteAddRequest noteAddRequest)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.AddNotes(noteAddRequest, UserId);
           
            if (data != null)
            {
                 status = "True";
                 message = "Note Added Successfully";
                return Ok(new { status, message, noteAddRequest });
            }
            else {
                status = "False";
                message = "Note Not Added ";
                return BadRequest(new { status, message });
            }

        }

        /// <summary>
        /// Updates Notes here update the notes Title and the Content inside that Notes only
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns>IActionresult</returns>
        [HttpPut]
       // [Route("")]
        public async Task<IActionResult> UpdateNotes( NoteUpdateRequest noteUpdateRequest)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);

            var data = await this.notesBusinessLayer.UpdateNotes(noteUpdateRequest, UserId);

            
            if (data != null)
            {

                 message = "Note has been Updated";
                status = "True";
                return Ok(new { status, message, noteUpdateRequest });
            }
            else {
                 message = "Note Not Updated";
                status = "False";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// delete the Rows Id vise
        /// </summary>
        /// <param name="deleteModel">deleteModel</param>
        /// <returns>delete o rnot</returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteNotes(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);

            var status = await this.notesBusinessLayer.DeleteNotes(Id, UserId);
            if (status == true)
            {
                 message = "Notes is Deleted";
                return Ok(new { status, message });
            }
            else {
                 message = "Notes is Not Deleted";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Inserts the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>InsertImage</returns>
        [HttpPost("UploadImage")]
        public async Task<IActionResult> InsertImage([FromForm]  int Id, IFormFile file)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status = await this.notesBusinessLayer.UploadImage(Id, UserId, file);

            if (status == true)
            {

                 message = "Image uploaded Successfully";
                return Ok(new { status, message, Id, file.FileName});
            }
            else
            {
                 message = "Image not uploaded";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display
        /// </returns>
        [HttpGet("{Id}")]
        public IActionResult Display(int Id)
        {

            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var response = this.notesBusinessLayer.Display(Id, UserId);
             message = "data is displayed";
            return Ok(new { response, message, Id });
        }

        /// <summary>
        /// Displays all notes.
        /// </summary>
        /// <returns>display the all records</returns>
        [HttpGet]
        [Route("")]
        public IActionResult DisplayAllNotes()
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var message = this.notesBusinessLayer.DisplayAllNotes(UserId);
            return Ok(new { message });
        }

        /// <summary>
        /// Determines whether this instance is trashed.
        /// </summary>
        /// <returns>trashednotes</returns>
        [HttpGet("AllTrash")]
        public async Task<IActionResult> IsTrashed()
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status = this.notesBusinessLayer.GetTrashedNotes(UserId);
            return Ok(new { status });
        }

        /// <summary>
        /// Trasheds the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>trashe or untrash</returns>
        [HttpPost("Trash/{Id}")]
        public async Task<IActionResult> Trashed(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status = await this.notesBusinessLayer.Trashed(UserId, Id);

            if (status == true)
            {
                 message = "Trash Note";
                return Ok(new { status, message });
            }
            else
            {
                 message = "Untrash Note";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Archives this instance.
        /// </summary>
        /// <returns>Get Archive</returns>
        [HttpGet("GetArchive")]
        public IActionResult Archive()
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var message = this.notesBusinessLayer.GetArchiveNotes(UserId);
            return Ok(new { message });
        }

        /// <summary>
        /// Determines whether the specified user identifier is archive.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Archive or UnArchive</returns>
        [HttpPut("Archive/{Id}")]
        public async Task<IActionResult> IsArchive( int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var status = await this.notesBusinessLayer.IsArchive(UserId, Id);
            if (status == true)
            {
                 message = "Archive Note";
                return Ok(new { status,message });
            }
            else {
                 message = "UnArchive Note";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Determines whether the specified identifier is pin.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Pin and UnPin</returns>
        [HttpPut("Pin/{Id}")]
        public async Task<IActionResult> IsPin(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.IsPin(UserId, Id);
            if (data == true)
            {
                string message = "Pin Note";
                return Ok(new { status, message });
            }
            else
            {
                string message = "UnPin Note";
                return BadRequest(new { status, message });
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="Color">The color.</param>
        /// <returns>Change Color</returns>
        [HttpPut("SetColor")]
        public async Task<IActionResult> ChangeColor( ColorRequest colorRequest)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.ChangeColor(UserId, colorRequest);

         
            if (data != null)
            {
                message = "color Changed";
                status = "True";
                return Ok(new { message,status ,data });
            }
            else
            {
                message = "Id is Invalid";
                status = "False";
                return BadRequest(new {  message,status });
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>reminder</returns>
        [HttpPost("SetReminder")]
        public async Task<IActionResult> SetReminder(RequestReminder requestReminder)
        {
         
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.SetReminder(UserId, requestReminder);
            if (data != null)
            {
                 message = "Reminder is Set";
                 status = "True";
                return Ok(new {message,status,data});
            }
            else
            {
                 message = "Id is Invalid";
                 status = "false";
                return BadRequest(new { message,status });
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Delete the reminder</returns>
        [HttpDelete("Reminder/{Id}")]        
        public async Task<IActionResult> DeleteReminder( int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.DeleteReminder(UserId, Id);
            if (data == true)
            {
                 message = "Reminder has been Deleted";
                return Ok(new { status, message });
            }
            else
            {
                 message = "Id is Invalid";
                return BadRequest(new { status, message });
            }

        }

        [HttpPost("LabelsOnNote")]
        public IActionResult SetLabelsOnNote( NoteLabel noteLabel)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var message = this.notesBusinessLayer.LabelsOnNote(UserId, noteLabel);
            return Ok(new { message });
        }

        [HttpGet("Search")]
        public IActionResult Searching(string input)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var message = this.notesBusinessLayer.Search(input,UserId);
            return Ok(new { message });
        }

        [HttpPost("Collabrator")]
        public async Task<IActionResult> Collabrator( ShowCollabrateModel showCollabrateModel)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
       
            var data = await this.notesBusinessLayer.AddCollabrator(showCollabrateModel,UserId);
            if (data!=null)
            {
                 message = "Collabration is Done!";
                return Ok(new { status,message});
            }
            else {
                 message = "Invalid Email or Note Id";
                return BadRequest(new { status, message });
            }            
        }

        [HttpDelete("Collabrator/{Id}")]
        public async Task<IActionResult> RemoveCollabrators(int Id)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
       
            var data = await this.notesBusinessLayer.RemoveCollabrator(Id, UserId);
            if (data == true)
            {
                 message = "Remove successfully";
                return Ok(new { status, message });
            }
            else {
                 message = " Id Not Exists";
                return BadRequest(new { status, message });
            }
        }
        [HttpPost("Users")]
        public IActionResult DisplayUser()
        {
            var result = this.notesBusinessLayer.Users();
            return Ok(new { result });
        }

    }
}