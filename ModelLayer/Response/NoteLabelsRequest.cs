using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Response
{
   public class NoteLabelsRequest
    {
       // public int LabelId { get; set; }

        public int NoteId { get; set; }

        public IList<LabelsWithNotesResponse> Label { get; set; }

        public string  Content { get; set; }

        public string  Title { get; set; }

        public DateTime? Reminder { get; set; }

      
    }
}
