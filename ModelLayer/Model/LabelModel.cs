// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Ajay Lodale"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class LabelModel
    {       
        /// <summary>
        /// Gets or sets the label identifier.
        /// this is primary key of the Label  Table
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// this is comes from the Notes table
        /// Notes table Id is Foreign key in the label table
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(ErrorMessage = "Foreign is required")]
        [ForeignKey("NotesModel")]       
        public int NoteId { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// this key comes from the AccountTable
        /// there primary key work here as a foreign key
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required(ErrorMessage = "Foreign is required")]
        [ForeignKey("AccountModel")]       
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// it is store the labels of the particular Notes
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; set; }
    }
}
