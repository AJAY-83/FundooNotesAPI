using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
   public class NoteLabel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// set the Primary key to Id in NotesLabel Table
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the label identifier.
        /// </summary>
        /// <value>
        /// The label identifier.
        /// </value>
        [ForeignKey("Lable")]
        [Required(ErrorMessage ="Lable is Important")]
        public int LabelId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// makes the foreign to the UserId it is the inside the UserAccountTabele Table in Database
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [ForeignKey("UserAccountTable")]
        [Required(ErrorMessage = "User is Important")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the notes identifier.
        /// makes the foreign key to NotesId it is inside the Notes Table in Databse
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [ForeignKey("Notes")]
        [Required(ErrorMessage = "Notes  is Important")]
        public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// is the store the label only
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string  LabelData { get; set; }
    }
}
