// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using CloudinaryDotNet;
    using CommonLayer.Constance;
    using CommonLayer.Model;
    using CommonLayer.Request;
    using CommonLayer.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class NotesRepositoryLayer : INotesRepositoryLayer
    {
        private readonly AuthenticationContext authenticationContext;

        private readonly IConfiguration configuration;
       
        public NotesRepositoryLayer(AuthenticationContext authenticationContext, IConfiguration configuration)
        {
            this.authenticationContext = authenticationContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="UserId"></param>
        /// <returns>
        /// Note add or not
        /// </returns>
        /// <exception cref="Exception">AddNotes</exception>
        public async Task<NoteAddRequest>  AddNotes(NoteAddRequest noteAddRequest,int UserId)
        {
            try
            {
                var users = new NotesModel()
                {
                    Title = noteAddRequest.Title,
                    Content = noteAddRequest.Content,
                    UserId= UserId,
                    CreatedDate =DateTime.Now,
                    ModifiedDate =DateTime.Now,
                    Color= noteAddRequest.Color,
                    Reminder = null,                  
                    IsActive = false,
                    IsTrash = false,
                    IsPin = false,
                    IsArchive = false,
                    IsNotes = false
            };

                this.authenticationContext.Notes.Add(users);
                var result = await authenticationContext.SaveChangesAsync();
                var response = new NoteAddRequest()
                {                    
                    Title = noteAddRequest.Title,
                    Content = noteAddRequest.Content,
                    Color = noteAddRequest.Color
                };

                return response;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="updateModel">The update model.</param>
        /// <returns>
        /// update or not
        /// </returns>
        /// <exception cref="Exception">exception</exception>
        public async Task<NoteUpdateRequest> UpdateNotes(NoteUpdateRequest noteUpdateRequest,int UserId)
        {
            try
            {
                var data = authenticationContext.Notes.Where(a => a.UserId == UserId && a.Id == noteUpdateRequest.Id).SingleOrDefault();

                if (data != null)
                {
                    if (noteUpdateRequest.Title != null)

                    {                       
                        data.Title = noteUpdateRequest.Title;
                        data.ModifiedDate = noteUpdateRequest.ModifiedDate;
                        await authenticationContext.SaveChangesAsync();
                        var response = new NoteUpdateRequest()
                        {
                            Title = noteUpdateRequest.Title,
                            Content = noteUpdateRequest.Content,
                            Color = noteUpdateRequest.Color,
                            ModifiedDate=noteUpdateRequest.ModifiedDate,
                            Reminder= noteUpdateRequest.Reminder
                        };

                        return response;
                    }
                    else if (noteUpdateRequest.Content != null)
                    {                       
                        data.Content = noteUpdateRequest.Content;
                        data.ModifiedDate = noteUpdateRequest.ModifiedDate;
                        await authenticationContext.SaveChangesAsync();
                        var response = new NoteUpdateRequest()
                        {
                            Title = noteUpdateRequest.Title,
                            Content = noteUpdateRequest.Content,
                            Color = noteUpdateRequest.Color,
                            ModifiedDate = noteUpdateRequest.ModifiedDate,
                            Reminder = noteUpdateRequest.Reminder
                        };

                        return response;
                    }
                }
                else {
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="deleteModel">The delete model.</param>
        /// <returns>delete or not</returns>
        public async Task<bool> DeleteNotes(int Id,int UserId)
        {
        
            var data = this.authenticationContext.Notes.Where(u => u.Id == Id && u.UserId==UserId).FirstOrDefault();

            //var result = await authenticationContext.SaveChangesAsync();
            if (data != null)
            {
                var result = authenticationContext.Notes.Remove(data);
                await this.authenticationContext.SaveChangesAsync();
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Image uploading here it will upload image on the cloudinary(there have the free space to upload the images and videos)
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> UploadImage(int Id,int UserId, IFormFile file)
        {
            //// ImageUpload is the class where we are writtens all creadintial details about the user of cloudinary
            ImageUpload cloudiNary = new ImageUpload();
            //// Account is the CloundinaryDotNet class 
            var cloudName = configuration["Config:CLOUD_NAME"];
            var APIKey = configuration["Config:API_KEY"];
            var SecretKey = configuration["Config:API_SECCRET_KEY"];
           
            Account account = new Account()
            {
                Cloud = cloudName,
                ApiKey = APIKey,
                ApiSecret = SecretKey
            }; 
            cloudiNary.cloudinary = new Cloudinary(account);

            ////getting the Notes Id to store the image on particular Notes only
            var image =  (from Notes in authenticationContext.Notes select Notes.Id).FirstOrDefault();

            ////checking the email id is Exist or not if exist then upload image 
            var idexist =  authenticationContext.Notes.Where(x => x.Id == Id && x.UserId==UserId).Any();

            //// here checking the Services 
            var checksServices = this.authenticationContext.UserAccountTable.Where(user => user.Id == UserId && user.Services == "Advance").FirstOrDefault();
            if (checksServices != null)
            {
                if (idexist == true)
                {
                    //// fetching all Records of Notes and just checking that notes Id
                    var data = this.authenticationContext.Notes.SingleOrDefault(u => u.Id == Id);

                    //// ImageUpload class have the UploadImage method inside that have the file related details
                    data.Image = cloudiNary.UploadImage(file);

                    //// lastyly save into the databse successfully
                    var result = await authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
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
        /// <returns>Display data via Id</returns>
        public IList<NotesModel> Display(int Id,int UserId)
        {
            //// stores the all notes into the List and display 
            List<NotesModel> note = new List<NotesModel>();
            foreach (var line in this.authenticationContext.Notes)
            {
                if (Id == line.Id && UserId==line.UserId)
                {
                    note.Add(line);
                    return note;
                }
            }

            return note;
        }

        /// <summary>
        /// Displays all notes.
        /// </summary>
        /// <returns>
        /// Display the All Notes
        /// </returns>
        public IList<NotesModel> DisplayAllNotes(int UserId)
        {
            //// Creates the List To store the all Notes and display the All Untrashed Notes
            List<NotesModel> note = new List<NotesModel>();
            //// foreach loop to gets the Trashed Fields
            foreach (var line in this.authenticationContext.Notes)
            {
                ////checking if trash is false then store it into the node 
                if (line.IsTrash == false && line.UserId==UserId)
                {
                    note.Add(line);                              
                }
            }

            //// Returns all notes which are store into the note object
            return note;
        }

        /// <summary>
        /// Gets the trashed notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>trashed</returns>
        public IList<NotesModel> GetTrashedNotes(int UserId)
        {
            //// creates List to store the all trashed Notes
            List<NotesModel> trashednote = new List<NotesModel>();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(user => user.Id == UserId && user.Services == "Advance").FirstOrDefault();
            //// getting all notes and store into the trashednote 

            if (checksServices != null)
            {
                foreach (var line in this.authenticationContext.Notes)
                {
                    if (line.UserId == UserId)
                    {
                        if (line.IsTrash == true)
                        {
                            trashednote.Add(line);
                        }
                    }
                }

                //// return all trashed notes
                return trashednote;

            }
            else {
                return null;
            }
           
        }

        /// <summary>
        /// Trasheds the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>trashed</returns>
        public async Task<bool> Trashed(int UserId, int Id)
        {
            //// checking the notes Id and UserId is Availabel or not into the databse
            var data =   this.authenticationContext.Notes.Where(u => u.Id == Id && u.UserId==UserId).FirstOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(user => user.Id == UserId && user.Services == "Advance").FirstOrDefault();
           
            if (checksServices != null)
            {
                //// checks that is null or  not null
                if (data != null)
                {
                    //// if IsTRash is true make it  false 
                    /// it means untrashed the Notes and save it into the database
                    if (data.IsTrash == true)
                    {
                        data.IsTrash = false;
                        data.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        //// if IsTrash is false then make it true
                        //// means trash the Note and save into Database
                        data.IsTrash = true;
                        data.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return true;
                    }
                }
                
            }
                       
            //// userId and Notes Id must be same then and then it will perform the operation
            ////otherwise it will throw the error
            return false;                
            }

        /// <summary>
        /// Gets the archive notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>get archive notes</returns>
        public IList<NotesModel> GetArchiveNotes(int UserId)
        {
            List<NotesModel> archive = new List<NotesModel>();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(user => user.Id == UserId && user.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                foreach (var line in authenticationContext.Notes)
                {
                    if (UserId == line.UserId)
                    {
                        if (line.IsArchive == true)
                        {
                            archive.Add(line);
                        }
                    }
                }
                return archive;
            }
            else
            {
                return null;
            }
           
        }

        /// <summary>
        /// Determines whether the specified user identifier is archive.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Archive or UnArchive</returns>
        public async Task<bool> IsArchive(int UserId, int Id)
        {
            var user = this.authenticationContext.Notes.Where(u => u.UserId == UserId && u.Id == Id).FirstOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(userArchive => userArchive.Id == UserId && userArchive.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (user != null)
                {
                    if (user.IsArchive == true)
                    {
                        user.IsArchive = false;
                        user.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        user.IsArchive = true;
                        user.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return true;
                    }
                }
            }
            else {
                return false;
            }
            return false;
           
        }

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>pin or Unpin string</returns>
        public async Task<bool> IsPin(int UserId, int Id)
        {
            var user =  this.authenticationContext.Notes.Where(u => u.UserId == UserId && u.Id == Id).FirstOrDefault();


            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (user != null)
                {
                    if (user.IsPin == true)
                    {
                        user.IsPin = false;
                        user.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return false;
                    }
                    else
                    {
                        user.IsPin = true;
                        user.ModifiedDate = DateTime.Now;
                        this.authenticationContext.SaveChanges();
                        return true;
                    }
                }
            }
            else {
                return false;
            }
            return false;         
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Color">The color.</param>
        /// <returns>color change</returns>
        public async Task<ColorRequest> ChangeColor(int UserId,ColorRequest colorRequest)
        {
            var user = this.authenticationContext.Notes.Where(u => u.UserId == UserId && u.Id ==colorRequest.Id).SingleOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (user != null)
                {
                    var model = new NotesModel()
                    {
                        Color = colorRequest.Color,
                        ModifiedDate = colorRequest.ModifiedDate
                    };
                    this.authenticationContext.SaveChanges();

                    var response = new ColorRequest()
                    {
                        Id = colorRequest.Id,
                        Color = colorRequest.Color,
                        ModifiedDate = colorRequest.ModifiedDate
                    };
                    return response;
                }
            }          
            else
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <param name="Reminder">The reminder.</param>
        /// <returns>set reminder or not string</returns>
        public async Task<RequestReminder> SetReminder(int UserId, RequestReminder requestReminder)
        {
            var user= this.authenticationContext.Notes.Where(u => u.UserId == UserId && u.Id ==requestReminder.Id).SingleOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (user != null)
                {
                    var model = new NotesModel()
                    {
                        Reminder = requestReminder.Reminder,
                        ModifiedDate = DateTime.Now
                    };
                    this.authenticationContext.SaveChanges();

                    var response = new RequestReminder()
                    {
                        Id = requestReminder.Id,
                        Reminder = requestReminder.Reminder
                    };

                    return response;
                }
                else
                {
                    return null;
                }
            }
            else {
                return null;
            }
            
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>delete reminder string</returns>
        public async Task<bool> DeleteReminder(int UserId, int Id)
        {
            var user = this.authenticationContext.Notes.Where(u => u.UserId == UserId && u.Id == Id).SingleOrDefault();
            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (user != null)
                {
                    user.Reminder = null;
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }


        /// <summary>
        /// Determines whether the specified input is searched.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>search note</returns>
        public IList<NotesModel> IsSearched(string input,int UserId)
        {          
            //// Creates the List To store the all Notes and display the All Untrashed Notes
            List<NotesModel> note = new List<NotesModel>();
            //// foreach loop to gets the Trashed Fields
            foreach (var line in this.authenticationContext.Notes)
            {
                //// var data=this.authenticationContext.Notes.Where(s =>  s.Content.Contains(input));
                ////checking if trash is false then store it into the node 
               if(line.UserId==UserId)
                {
                    if(line.Title.Contains(input) || line.Content.Contains(input))
                    {
                        note.Add(line);
                    }                    
                }                               
            }
            //// Returns all notes which are store into the note object
            return note;
        }

        /// <summary>
        /// Determines whether the specified user identifier is collabrate.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="collabratorModel">The collabrator model.</param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<CollabratorModel> IsCollabrate(ShowCollabrateModel showCollabrateModel, int UserId)
        {


            try
            {
                //// checks the NoteId exist or not            
                //var idChecking = this.authenticationContext.UserAccountTable.FirstOrDefault(u => u.Id.Equals(showCollabrateModel.ReceiverId));
                ////checking the Note Id is available in database or  not
                var notesIdCheck = this.authenticationContext.Notes.FirstOrDefault(u => u.Id == showCollabrateModel.NoteId);

                //// checks the service is Advance or not if service is Advance so it have permission 
                var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();
                 
                //// ifn ot present into the database i will return the null 
                if (notesIdCheck == null)
                {
                    return null;
                }

                if (checksServices != null)
                {
                    //// here   checking  the Collabrated user is present into database or not
                    if (notesIdCheck != null)
                    {

                        var collabratorModel = new CollabratorModel()
                        {
                            UserId = UserId,
                            NotesId = showCollabrateModel.NoteId,
                            ReceiverId = showCollabrateModel.ReceiverId
                        };

                        //// inserting the Collabrator records inside the table 
                        this.authenticationContext.Collabrator.Add(collabratorModel);
                        await this.authenticationContext.SaveChangesAsync();
                        //// cerating  the response object here display the only needed  information of the user
                        var response = new CollabratorModel()
                        {
                            UserId = collabratorModel.UserId,
                            NotesId = collabratorModel.NotesId,
                            ReceiverId = collabratorModel.ReceiverId,
                            Id = collabratorModel.Id

                        };
                        //// return the expecterd output 
                        return response;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// Determines whether [is remove collabrator] [the specified identifier].
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> IsRemoveCollabrator(int Id, int UserId)
        {
            var data = this.authenticationContext.Collabrator.Where(u => u.Id == Id && u.UserId == UserId).FirstOrDefault();


            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                //var result = await authenticationContext.SaveChangesAsync();
                if (data != null)
                {
                    var result = authenticationContext.Collabrator.Remove(data);
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Bulks the trash.
        /// </summary>
        /// <param name="NotesId">The notes identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool BulkTrash(List<int> NotesId, int UserId)
        {

            // List<NotesModel> note = new List<NotesModel>();
            //// foreach loop to gets the Trashed Fields

            try
            {
                //// checks the service is Advance or not if service is Advance so it have permission 
                var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

                if (checksServices != null)
                {
                    foreach (var Id in NotesId)
                    {
                        foreach (var note in this.authenticationContext.Notes)
                        {
                            //// checking the notes Id and UserId is Availabel or not into the databse
                            var data = this.authenticationContext.Notes.Where(u => u.Id == Id && u.UserId == UserId).FirstOrDefault();

                            //// checks that is null or  not null
                            if (data != null)
                            {
                                //// if IsTRash is true make it  false 
                                //// it means untrashed the Notes and save it into the database
                                if (data.IsTrash == false)
                                {
                                    data.IsTrash = true;
                                    data.ModifiedDate = DateTime.Now;
                                }
                                else
                                {
                                    data.IsTrash = false;
                                    data.ModifiedDate = DateTime.Now;
                                }


                            }

                        }
                        this.authenticationContext.SaveChanges();
                    }
                    return true;
                }
                else {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        /// <summary>
        /// Bulks the un trash.
        /// </summary>
        /// <param name="NotesId">The notes identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool BulkUnTrash( List<int> NotesId,int UserId)
        {

            // List<NotesModel> note = new List<NotesModel>();
            //// foreach loop to gets the Trashed Fields

            try
            {
                //// checks the service is Advance or not if service is Advance so it have permission 
                var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

                if (checksServices != null)
                {
                    foreach (var Id in NotesId)
                    {
                        foreach (var note in this.authenticationContext.Notes)
                        {
                            //// checking the notes Id and UserId is Availabel or not into the databse
                            var data = this.authenticationContext.Notes.Where(u => u.Id == Id && u.UserId == UserId).FirstOrDefault();

                            //// checks that is null or  not null
                            if (data != null)
                            {
                                //// if IsTRash is true make it  false 
                                /// it means untrashed the Notes and save it into the database
                                if (data.IsTrash == true)
                                {
                                    data.IsTrash = false;
                                    data.ModifiedDate = DateTime.Now;
                                }

                            }
                        }

                        this.authenticationContext.SaveChanges();
                    }
                    return true;
                }
                else {
                    return false;
                }
            }            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sortings the specified user identifier.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public IList<NotesModel> Sorting( int UserId)
        {
            //// Creates the List To store the all Notes and display the All Untrashed Notes
            List<NotesModel> note = new List<NotesModel>();
            var data = this.authenticationContext.UserAccountTable.Where(id => id.Id == UserId).FirstOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                if (data != null)
                {
                    List<NotesModel> sortedUsers = this.authenticationContext.Notes.OrderBy(user => user.Title).ToList();

                    //// this.authenticationContext.Notes.OrderBy(user => user.Content) .ThenByDescending(user => user.Title).ToList()
                    return sortedUsers;
                }
                else
                {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Labels the on notes.
        /// </summary>
        /// <param name="noteLabel">The note label.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        public async Task<NoteLabel> LabelOnNotes(NoteLabel noteLabel,int UserId)
        {
            var data = this.authenticationContext.UserAccountTable.Select(user => user.Id == UserId);
     
            var alreadyasing = this.authenticationContext.NoteLabel.SingleOrDefault(label => label.LabelId == noteLabel.LabelId && label.NoteId == noteLabel.NoteId);

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId);//&& pinuser.Services == "Advance").FirstOrDefault();
          
            
            if (alreadyasing != null)
            {
                return null;
            }
            if (checksServices != null)
            {
                if (data != null)
                {
                    var labels = new NoteLabel()
                    {
                        UserId = UserId,
                        NoteId = noteLabel.NoteId,
                        LabelId = noteLabel.LabelId,
                        CreatedDate = noteLabel.CreatedDate,
                        ModifiedDate = noteLabel.ModifiedDate
                    };

                    this.authenticationContext.NoteLabel.Add(labels);
                    var result = await this.authenticationContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        return labels;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// Removes the labelfrom note.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <returns></returns>
        public async Task<bool> RemoveLabelfromNote(int UserId, int NoteId)
        {

            var data = this.authenticationContext.NoteLabel.Where(u => u.UserId == UserId && u.NoteId == NoteId).FirstOrDefault();

            //// checks the service is Advance or not if service is Advance so it have permission 
            var checksServices = this.authenticationContext.UserAccountTable.Where(pinuser => pinuser.Id == UserId && pinuser.Services == "Advance").FirstOrDefault();

            if (checksServices != null)
            {
                //var result = await authenticationContext.SaveChangesAsync();
                if (data != null)
                {
                    this.authenticationContext.NoteLabel.Remove(data);
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        public IList<NoteLabel> DisplayNoteLabels(int UserId)
        {

            var data = this.authenticationContext.NoteLabel.Where(x => x.UserId == UserId).FirstOrDefault();
            List<NoteLabel> note = new List<NoteLabel>();
            if (data != null)
            {

                foreach (var line in this.authenticationContext.NoteLabel)
                {
                    note.Add(line);
                }
            }
            return note;
        }

        public  IList<NoteLabelsRequest> DisplayLabelsOnNote(int UserId)
        {
   
            try
            {
              List<NoteLabelsRequest> noteLabelsRequests = new List<NoteLabelsRequest>();
                LabelRepositoryLayer labelRepositoryLayer = new LabelRepositoryLayer(authenticationContext);
                List<NotesModel> notesdata = DisplayAllNotes(UserId).ToList();
                List<LabelModel> labeldata = labelRepositoryLayer.Display(UserId).ToList();
                List<NoteLabel> noteLabels = DisplayNoteLabels(UserId).ToList();

                var data = (from label in labeldata
                            join labelN in noteLabels
                            on label.Id equals labelN.LabelId
                            join notes in notesdata
                            on labelN.NoteId equals notes.Id
                            select new NoteLabelsRequest()
                            {
                               // LabelId = label.Id,
                                NoteId = labelN.NoteId,
                                Label = labelist(UserId).ToList(),
                                Title = notes.Title,
                                Content = notes.Content,
                                Reminder=notes.Reminder
                                

                            }).ToList();
                 return data;
                //foreach (var notelabels in query)
                //{
                //    noteLabels.Add(notelabels);
                //}

               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IList<LabelsWithNotesResponse> labelist(int UserId)
        {
            LabelRepositoryLayer labelRepositoryLayer = new LabelRepositoryLayer(authenticationContext);
            List<LabelModel> labeldata = labelRepositoryLayer.Display(UserId).ToList();
            List<NoteLabel> noteLabels = DisplayNoteLabels(UserId).ToList();

            var DBData = (from notesL in noteLabels
                          join labels in labeldata
                          on notesL.LabelId equals labels.Id                          
                          select new LabelsWithNotesResponse()
                          {
                              Id = labels.Id,                                
                              Label = labels.Label

                          });
            var data = DBData.ToList();
            return data;
        }



        //public IList<NoteLabel> LabelsOnNote(int UserId)
        //{
        //    try
        //    {
        //        LabelRepositoryLayer labelRepositoryLayer = new LabelRepositoryLayer(authenticationContext);
        //        List<NotesModel> notesdata = DisplayAllNotes(UserId).ToList();
        //        List<LabelModel> labeldata = labelRepositoryLayer.Display(UserId).ToList();
                

        //        var DBData = (from notes in notesdata
        //                      join labels in labeldata
        //                      on notes.Id equals labels.NoteId
        //                      //join s in supplData on notes.SupplierID equals s.SupplierID
        //                      select new NoteLabel()
        //                      {
        //                          Id = notes.Id,
        //                          Title = notes.Title,
        //                          Content = notes.Content,
        //                          Label = labels.Label,
        //                          NoteId = labels.NoteId,
        //                          UserId = labels.UserId
        //                      });
        //        var data = DBData.ToList();
        //        return data;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}
    }
}
