// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Interface
{
    using CommonLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILabelBusinessLayer
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="addLabel">The add label.</param>
        /// <returns>Add or not</returns>
        Task<bool> AddLabel(LabelModel addLabel,int UserId);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Updated or not </returns>
        Task<bool> UpdateLabel(LabelModel labelModel,int UserId);

        /// <summary>
        /// Deletelabels the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>deleted or not</returns>
        Task<bool> DeleteLabel(int Id,int UserId);

        /// <summary>
        /// Displays the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>Display Data</returns>
        IList<LabelModel> Display(int UserId);

        IList<LabelModel> IsSearched(string input, int UserId);

        Task<bool> InsertListOFLabels(List<string> labels, int UserId,int NoteId);
    }
}
