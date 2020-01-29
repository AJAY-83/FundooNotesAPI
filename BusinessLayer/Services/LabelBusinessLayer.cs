// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelBusinessLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using BusinessLayer.Interface;
    using CommonLayer.Constance;
    using CommonLayer.Model;
    using RepositoryLayer.Interface;
    using ServiceStack.Redis;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
  
    /// <summary>
    /// this is Business layer class it is used to  CRUD Operations on the Label
    /// </summary>
    public class AdminBusinessService : ILabelBusinessLayer
    {
        /// <summary>
        /// The labelrepositorylayer is the reference of the repository layer of Label
        /// </summary>
        private ILabelRepositoryLayer labelRepositoryLayer;

        public AdminBusinessService(ILabelRepositoryLayer labelRepositoryLayer)
        {
            this.labelRepositoryLayer = labelRepositoryLayer;
        }

        /// <summary>
        /// Addlabels the specified addlabel.
        /// </summary>
        /// <param name="addlabel">The addlabel.</param>
        /// <returns>Add Labels</returns>        
        public async Task<bool> AddLabel(LabelModel addLabel,int UserId)
        {
            if (addLabel != null)
            {
                var result = await this.labelRepositoryLayer.AddLabel(addLabel,UserId);
                return result;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>model empty</returns>
        public async Task<bool> UpdateLabel(LabelModel labelModel,int UserId)
        {
            if (labelModel != null)
            {
                var result = await this.labelRepositoryLayer.UpdateLabel(labelModel,UserId);
                return result;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Deletelabels the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        /// deleted or not
        /// </returns>
        public async Task<bool> DeleteLabel(int Id,int UserId)
        {

            if (Id > 0)
            {
                return await this.labelRepositoryLayer.DeleteLabel(Id, UserId);
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
        /// <returns></returns>
        public IList<LabelModel> Display(int Id)
        {

            var result = this.labelRepositoryLayer.Display(Id);
            return result;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns></returns>
        public async Task<bool> GetLabel(LabelModel labelModel,int UserId)
        {
            using (var client = new RedisClient())
            {
                string key = labelModel.Id.ToString();
                if (client != null)
                {
                    var getNotes = client.Get(key);
                    return await this.labelRepositoryLayer.AddLabel(labelModel,UserId);
                }
                else
                {
                    client.Set(key,labelModel);
                    return false;                    
                }
            }

          
        }
        public IList<LabelModel> IsSearched(string input, int UserId)
        {
            var result = this.labelRepositoryLayer.IsSearched(input, UserId);
            return result;
        }

        public async Task<bool> InsertListOFLabels(List<string> labels, int UserId, int NoteId)
        {
            try
            {
                var result = await this.labelRepositoryLayer.IsInsertListOFLabels(labels, UserId, NoteId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
