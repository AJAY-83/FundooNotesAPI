// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using CommonLayer.Constance;
    using CommonLayer.Model;
    using RepositoryLayer.Context;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LabelRepositoryLayer:ILabelRepositoryLayer
    {
        private readonly AuthenticationContext authenticationContext;

        public LabelRepositoryLayer(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="addlabel">The addlabel.</param>
        /// <returns>
        /// Added or not
        /// </returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AddLabel(LabelModel addLabel,int UserId)
        {
            try
            {
                var label = new LabelModel()
                {
                    Label = addLabel.Label,                   
                    Id= addLabel.Id,
                    UserId = UserId,
                    NoteId=addLabel.NoteId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now

                };

                var adddata = this.authenticationContext.Label.Add(label);
              
                var result = await authenticationContext.SaveChangesAsync();
                return true;               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Updated or Not
        /// </returns>
        public async Task<bool> UpdateLabel(LabelModel labelModel)
        {
            try
            {
                bool idexist = authenticationContext.Label.Any(x => x.Id == labelModel.Id);
                //// this.authenticationContext.Notes.SingleOrDefault(u => u.Id == updateModel.Id);
                if (idexist)
                {
                    var data = this.authenticationContext.Label.SingleOrDefault(u => u.Id == labelModel.Id && u.UserId==labelModel.UserId);
                    data.Label = labelModel.Label;
                    data.ModifiedDate = labelModel.ModifiedDate;

                    var result = await authenticationContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// Deleted or Not
        /// </returns>
        public async Task<bool> DeleteLabel(int Id,int UserId)
        {
            var data = this.authenticationContext.Label.Where(u => u.Id == Id && u.UserId==UserId).FirstOrDefault();                   
            if (data != null)
            {
                var result = authenticationContext.Label.Remove(data);
                await this.authenticationContext.SaveChangesAsync();
                return true;
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
        /// Display the data
        /// </returns>
        public  IList<LabelModel> Display(int UserId)
        {
            List<LabelModel> label = new List<LabelModel>();
         
            foreach (var line in this.authenticationContext.Label)
            {
                if (UserId == line.UserId)
                {
                    label.Add(line);                   
                }
            }

            return label;
        }

        public IList<LabelModel> IsSearched(string input, int UserId)
        {
            //// Creates the List To store the all Notes and display the All Untrashed Notes
            List<LabelModel> note = new List<LabelModel>();
            //// foreach loop to gets the Trashed Fields
            foreach (var line in this.authenticationContext.Label)
            {
                //// var data=this.authenticationContext.Notes.Where(s =>  s.Content.Contains(input));
                ////checking if trash is false then store it into the node 
                if (line.UserId == UserId)
                {
                    if (line.Label.Contains(input) )
                    {
                        note.Add(line);
                    }
                }
            }
            return note;
           }


      public async Task<bool> IsInsertListOFLabels(List<string> labels, int UserId,int NoteId)
        {
            try
            {
               string labellist = Convert.ToString(labels);
             
                foreach (var Id in labels.ToList())
                {
                    foreach (var note in this.authenticationContext.Label)
                    {
                        
                        //// checking the notes Id and UserId is Availabel or not into the databse
                        // var data = this.authenticationContext.Notes.Where(u => u.Id == Id && u.UserId == UserId).FirstOrDefault();
 

                        var label = new LabelModel()
                        {
                            Label = labellist,
                            UserId = UserId,
                            NoteId = NoteId,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now

                        };

                        var adddata = this.authenticationContext.Label.Add(label);

                        if (adddata != null)
                        {
                            var result = await authenticationContext.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return false;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        

        }
    }
}
