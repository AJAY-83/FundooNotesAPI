// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelRepositoryLayer.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
    using CommonLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILabelRepositoryLayer
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="addlabel">The addlabel.</param>
        /// <returns>Added or not </returns>
        Task<bool> AddLabel(LabelModel addLabel, int UserId);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Updated or Not</returns>
        Task<bool> UpdateLabel(LabelModel labelModel);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Deleted or Not</returns>
        Task<bool> DeleteLabel(int Id,int UserId);

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display the data</returns>
        IList<LabelModel> Display(int UserId);

        IList<LabelModel> IsSearched(string input, int UserId);

        Task<bool> IsInsertListOFLabels(List<string> labels, int UserId, int NoteId);
       
    }
}
