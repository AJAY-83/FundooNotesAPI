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
        [HttpPost("Image")]
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
        [HttpGet("Trash")]
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
        [HttpPost("{Id}/Trash")]
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
        [HttpGet("Archive")]
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
        [HttpPut("{Id}/Archive")]
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
        [HttpPut("{Id}/Pin")]
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
        [HttpPut("Color")]
        public async Task<IActionResult> SetColor( ColorRequest colorRequest)
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
        [HttpPost("Reminder")]
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
        [HttpDelete("{Id}/Reminder")]        
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
        public async  Task<IActionResult> SetLabelsOnNote(NoteLabel noteLabel)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data =await this.notesBusinessLayer.LabelOnNotes( noteLabel, UserId);
            if (data != null)
            {
                status = "True";
                message = "Label add on Note";
                return Ok(new { status,message,data });
            }
            else {
                status = "False";
                message = "Not Added label on Note";
                return Ok(new { status,message });
            }           
        }
        [HttpDelete("LabelsOnNote/{NoteId}")]
        public async Task<IActionResult> RemoveLabelsOnNote(int NoteId)
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var data = await this.notesBusinessLayer.RemoveLabelfromNote( UserId, NoteId);
            if (data == true)
            {
                status = "True";
                message = "Label Removed from Note";
                return Ok(new { status, message, data });
            }
            else
            {
                status = "False";
                message = "Not Remove label from Note";
                return Ok(new { status, message });
            }

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
                status = "true";
                 message = "Collabration is Done!";
                return Ok(new { status,message,data});
            }
            else {
                status = "false";
                message = "Invalid Email or Note Id";
                return BadRequest(new { status, message });
            }            
        }

        [HttpDelete("{Id}/Collabrator")]
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
        

        [HttpPut("BulkTrash")]
        public IActionResult Bulktrash(List<int> Id)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
                var status = this.notesBusinessLayer.BulkTrash(Id, UserId);

                if (status == true)
                {
                    message = "Bulk Trashed";
                    return Ok(new { message, status });
                }
                else
                {
                    message = "Not  Trashed";
                    return BadRequest(new {message,status });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPut("BulkUnTrash")]
        public IActionResult BulkUntrash(List<int> Id)
        {
            try
            {
                int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
                var status = this.notesBusinessLayer.BulkUnTrash(Id ,UserId);

                if (status == true)
                {
                    message = "Bulk UnTrashed";
                    return Ok(new { message, status });
                }
                else
                {
                    message = "Not Trashed";
                    return BadRequest(new { message, status });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Sorting")]
        public IActionResult Sorting()
        {
            int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
            var message = this.notesBusinessLayer.Sorting(UserId);
            return Ok(new { message });
        }


        //[HttpGet("Note")]
        //public IActionResult LabelsWithNotes()
        //{
        //    int UserId = Convert.ToInt32(User.FindFirst("Id")?.Value);
        //    var data = this.notesBusinessLayer.LabelsOnNote(UserId);
        //    if (data != null)
        //    {
        //        status = "True";
        //        message = "Data Found";
        //        return Ok(new {status,message,data });
        //    }
        //    else {
        //        message = "data not found";
        //        status = "False";
        //        return BadRequest(new { status,message });
        //    }
        //}
    }
}