// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INoteBusinessLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interface
{
    using CommonLayer.Model;
    using CommonLayer.Request;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotesBusinessLayer
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <returns>Add or Not</returns>
        Task<NoteAddRequest> AddNotes(NoteAddRequest noteAddRequest,int UserId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <returns>Update or not</returns>
        Task<NoteUpdateRequest> UpdateNotes(NoteUpdateRequest noteUpdateRequest,int UserId);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="deleteModel">The delete model.</param>
        /// <returns>Delete or Not</returns>
        Task<bool> DeleteNotes(int Id,int UserId);

        // string UploadImage(int id, IFormFile file);

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display the Record</returns>
        IList<NotesModel> Display(int Id,int UserId);

        /// <summary>
        /// Inserts the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>upload or not</returns>
        Task<bool> UploadImage(int Id, int UserId, IFormFile file);

        /// <summary>
        /// Displays all notes.
        /// </summary>
        /// <returns>Display the all Notes</returns>
        IList<NotesModel> DisplayAllNotes(int UserId);

        /// <summary>
        /// Gets the trashed notes.
        /// Display the All notes which are in Trashed Area
        /// </summary>
        /// <returns>Display</returns>
        IList<NotesModel> GetTrashedNotes(int UserId);

        /// <summary>
        /// Trasheds the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Trash or Untrash</returns>
        Task<bool> Trashed(int UserId, int Id);

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>gets Archive Notes</returns>
        IList<NotesModel> GetArchiveNotes(int UserId);

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Archive or UnArchive</returns>
        Task<bool> IsArchive(int UserId, int Id);

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Pin or Unpin</returns>
        Task<bool> IsPin(int UserId, int Id);

        /// <summary>
        /// Colors the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>change color</returns>
        Task<ColorRequest> ChangeColor(int UserId, ColorRequest colorRequest);

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>set reminder</returns>
        Task<RequestReminder> SetReminder(int UserId, RequestReminder requestReminder);

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Delete reminder</returns>
        Task<bool> DeleteReminder(int UserId, int Id);

        /// <summary>
        /// Labelses the on note.
        /// </summary>
        /// <param name="noteLabel">The note label.</param>
        /// <returns>Add Label on Note</returns>
       // string LabelsOnNote(int UserId,NoteLabel noteLabel);

        /// <summary>
        /// Searches the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>search data</returns>
        IList<NotesModel> Search(string input,int UserId);

        /// <summary>
        /// Adds the collabrator.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="collabratorModel">The collabrator model.</param>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
       Task<CollabratorModel> AddCollabrator(ShowCollabrateModel showCollabrateModel,int UserId);

        /// <summary>
        /// Removes the collabrator.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> RemoveCollabrator(int Id, int UserId);

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        bool BulkTrash(List<int> Id, int UserId);

        /// <summary>
        /// Bulks the un trash.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        bool BulkUnTrash(List<int> Id, int UserId);

        /// <summary>
        /// Sortings the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        IList<NotesModel> Sorting(int UserId);

        IList<NoteLabel> LabelsOnNote(int UserId);
    }
}
