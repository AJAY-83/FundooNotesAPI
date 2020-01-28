// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Interface
{
    using CommonLayer.Model;
    using CommonLayer.Request;
    using CommonLayer.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotesRepositoryLayer
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>Note add or not</returns>
        Task<NoteAddRequest> AddNotes(NoteAddRequest noteAddRequest,int UserId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <returns>update or not</returns>
        Task<NoteUpdateRequest> UpdateNotes(NoteUpdateRequest noteUpdateRequest,int UserId);

        /// <summary>
        /// Deletes the nodes.
        /// </summary>
        /// <param name="deleteNotes">The delete notes.</param>
        /// <returns>Delete or not</returns>
        Task<bool> DeleteNotes(int Id,int UserId);

        //string UploadImage(int id, IFormFile file);

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        IList<NotesModel> Display(int Id,int UserId);

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="file">The file.</param>
        /// <returns>uploaded or not</returns>
        Task<bool> UploadImage(int Id, int UserId, IFormFile file);

        /// <summary>
        /// Displays all notes.
        /// </summary>
        /// <returns>Display the All Notes</returns>
        IList<NotesModel> DisplayAllNotes(int UserId);

        /// <summary>
        /// Gets the trashed notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>gettrashed notes</returns>
        IList<NotesModel> GetTrashedNotes(int UserId);

        /// <summary>
        /// Trasheds the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Trash or untrash</returns>
        Task<bool> Trashed(int UserId, int Id);

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>GetArchive</returns>
        IList<NotesModel> GetArchiveNotes(int UserId);

        /// <summary>
        /// Determines whether the specified user identifier is archive.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>IsArchive</returns>
        Task<bool> IsArchive(int UserId, int Id);

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>pin or unpin</returns>
        Task<bool> IsPin(int UserId, int Id);

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Color">The color.</param>
        /// <returns>sel color on note</returns>
        Task<ColorRequest> ChangeColor(int UserId, ColorRequest colorRequest);

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>setreminder</returns>
        Task<RequestReminder> SetReminder(int UserId, RequestReminder requestReminder);

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Delete reminder</returns>
        Task<bool> DeleteReminder(int UserId, int Id);

        /// <summary>
        /// Sets the labels on note.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="noteLabel">The note label.</param>
        /// <returns></returns>
       // string SetLabelsOnNote(int UserId,NoteLabel noteLabel);

        /// <summary>
        /// Determines whether the specified input is searched.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        IList<NotesModel> IsSearched(string input,int UserId);

        /// <summary>
        /// Determines whether the specified user identifier is collabrate.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="collabratorModel">The collabrator model.</param>
        /// <returns></returns>
        Task<CollabratorModel> IsCollabrate(ShowCollabrateModel showCollabrateModel, int UserId);

        /// <summary>
        /// Determines whether [is remove collabrator] [the specified identifier].
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsRemoveCollabrator(int Id, int UserId);



        bool BulkTrash(List<int> Id, int UserId);
        bool BulkUnTrash(List<int> Id,int UserId);

        IList<NotesModel> Sorting( int UserId);

        //  IList<NoteLabel> LabelsOnNote(int UserId);

        Task<NoteLabel> LabelOnNotes(NoteLabel noteLabel, int UserId);
        Task<bool> RemoveLabelfromNote(int UserId, int NoteId);

        IList<NoteLabel> DisplayNoteLabels(int UserId);

        IList<NoteLabelsRequest> DisplayLabelsOnNote(int UserId);

       //// IList<LabelsWithNotesResponse> labelist(int UserId);
    }
}
