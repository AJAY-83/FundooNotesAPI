using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
   public class CollabratorModel
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("AccountModel")]
        public int UserId { get; set; }

        [ForeignKey("NotesModel")]
        public int NotesId { get; set; }

        [ForeignKey("AccountModel")]
        public int ReceiverId { get; set; }


    }
}
