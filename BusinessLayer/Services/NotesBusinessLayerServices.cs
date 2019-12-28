// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoteBusinessLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Services
{
    using BusinessLayer.Interface;
    using CommonLayer.Constance;
    using CommonLayer.Model;
    using CommonLayer.Request;
    using Microsoft.AspNetCore.Http;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class NotesBusinessLayerServices : INotesBusinessLayer
    {
        /// <summary>
        /// The notes repository layer
        /// </summary>
        public INotesRepositoryLayer notesRepositoryLayer;

        public NotesBusinessLayerServices(INotesRepositoryLayer notesRepositoryLayer)
        {
            this.notesRepositoryLayer = notesRepositoryLayer;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel"></param>
        /// <returns>
        /// Add or Not
        /// </returns>
        public async Task<NoteAddRequest> AddNotes(NoteAddRequest noteAddRequest,int UserId)
        {
            
                if (noteAddRequest != null)
                {
                    var result = await this.notesRepositoryLayer.AddNotes(noteAddRequest, UserId);
                    return result;
                }
                else
                {
                    return null;
                }
          
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <returns>
        /// Update or not
        /// </returns>
        public async Task<NoteUpdateRequest> UpdateNotes(NoteUpdateRequest noteUpdateRequest,int UserId)
        {
            if (noteUpdateRequest != null)
            {
                var result = await this.notesRepositoryLayer.UpdateNotes(noteUpdateRequest,UserId);
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="deleteModel">The delete model.</param>
        /// <returns>
        /// Delete or Not
        /// </returns>
        public async Task<bool> DeleteNotes(int Id,int UserId)
        {
            if (Id > 0)
            {
                var result = await this.notesRepositoryLayer.DeleteNotes(Id,UserId);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Inserts the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>
        /// upload or not
        /// </returns>
        public async Task<bool> UploadImage(int Id, int UserId, IFormFile file)
        {
            if (Id > 0)
            {
                var result = await this.notesRepositoryLayer.UploadImage(Id,UserId, file);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Display the Record
        /// </returns>
        public IList<NotesModel> Display(int Id,int UserId)
        {
            var result = this.notesRepositoryLayer.Display(Id, UserId);
            return result;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns></returns>
        public  IList<NotesModel> GetNotes(int Id,int UserId)
        {
            IList<NotesModel> result=new List<NotesModel>();
            using (var client = new RedisClient())
            {
                string key = Id.ToString();
                if (client != null)
                {
                    var getNotes = client.Get(key);

                    result= this.notesRepositoryLayer.Display(Id,UserId);
                    return result;
                }
                else
                {
                    client.Set(key, result);
                    return result;
                }
            }
        }

        /// <summary>
        /// Displays all notes.
        /// </summary>
        /// <returns>
        /// Display the all Notes
        /// </returns>
        public IList<NotesModel> DisplayAllNotes(int UserId)
        {
            var result = this.notesRepositoryLayer.DisplayAllNotes(UserId);
            return result;
        }

        /// <summary>
        /// Gets the trashed notes.
        /// Display the All notes which are in Trashed Area
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="Id">Id</param>
        /// <returns>
        /// Display
        /// </returns>
        public IList<NotesModel> GetTrashedNotes(int UserId)
        {
            var result = this.notesRepositoryLayer.GetTrashedNotes(UserId);
            return result;
        }

        /// <summary>
        /// Trasheds the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>trash or untrash</returns>
        public async Task<bool> Trashed( int Id, int UserId)
        {
            if (Id > 0)
            {
                var result = await this.notesRepositoryLayer.Trashed(Id, UserId);
                return result;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>
        /// gets Archive Notes
        /// </returns>
        public IList<NotesModel> GetArchiveNotes(int UserId)
        {
            var result = notesRepositoryLayer.GetArchiveNotes(UserId);
            return result;
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Archive or UnArchive
        /// </returns>
        public async Task<bool> IsArchive(int UserId, int Id)
        {
            if (UserId > 0)
            {
                var result = await this.notesRepositoryLayer.IsArchive(UserId, Id);
                return result;
            }
            else {
                return false;
            }
         
        }

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Pin or Unpin
        /// </returns>
        public async Task<bool> IsPin(int UserId,int Id)
        {
            
            if (UserId > 0)
            {
                var result = await this.notesRepositoryLayer.IsPin(UserId, Id);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Colors the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Color"></param>
        /// <returns>
        /// change color
        /// </returns>
        public async Task<ColorRequest> ChangeColor(int UserId, ColorRequest colorRequest)
        {
            string strRegex = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$";
            Regex colorcode = new Regex(strRegex);
            if (colorcode.IsMatch(colorRequest.Color))
            {
                var result = await this.notesRepositoryLayer.ChangeColor(UserId,colorRequest);
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>
        /// set reminder
        /// </returns>
        public async Task<RequestReminder> SetReminder(int UserId, RequestReminder requestReminder)
        {
           
            if (UserId > 0)
            {
                var result = await  this.notesRepositoryLayer.SetReminder(UserId, requestReminder);
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Delete reminder
        /// </returns>
        public async Task<bool> DeleteReminder(int UserId, int Id)
        {
           
            if (UserId > 0)
            {
                var result = await this.notesRepositoryLayer.DeleteReminder(UserId, Id);
                return result;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Labelses the on note.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="noteLabel">The note label.</param>
        /// <returns>
        /// Add Label on Note
        /// </returns>
        public string LabelsOnNote(int UserId,NoteLabel noteLabel)
        {
            var result = this.notesRepositoryLayer.SetLabelsOnNote(UserId,noteLabel);
            return result;
        }

        public IList<NotesModel> Search(string input,int UserId)
        {
            var result = this.notesRepositoryLayer.IsSearched(input, UserId);
            return result;
        }

        /// <summary>
        /// Adds the collabrator.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="collabratorModel">The collabrator model.</param>
        /// <param name="SenderEmail">The sender email.</param>
        /// <returns></returns>
        public async Task<CollabratorModel> AddCollabrator(ShowCollabrateModel showCollabrateModel,int UserId)
        {
            var result = await this.notesRepositoryLayer.IsCollabrate(showCollabrateModel,UserId);
        
            return result;
                                  
        }

        /// <summary>
        /// Removes the collabrator.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> RemoveCollabrator(int Id, int UserId)
        {
            
            if (Id > 0)
            {
                var result = await this.notesRepositoryLayer.IsRemoveCollabrator(Id, UserId);
                return result;
            }
            else {
                return false;
            }
        }

        public IList<AccountModel> Users()
        {
            var result = this.notesRepositoryLayer.Users();
            return result;
        }
    }
}
