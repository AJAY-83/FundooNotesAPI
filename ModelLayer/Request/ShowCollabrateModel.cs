using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Request
{
   public class ShowCollabrateModel
    {
        public int NoteId { get; set; }

        public IList<int> ReceiverId { get; set; }
    }
}
