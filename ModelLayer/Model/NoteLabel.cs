using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
   public class NoteLabel
    {
       
        public int Id { get; set; }
        public int  LabelId { get; set; }       
        public int NoteId { get; set; }
       // public string Title { get; set; }
       // public string Content { get; set; }
      //  public string Label { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserId { get; set; }
    }
}
