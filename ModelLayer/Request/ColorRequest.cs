using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Request
{
   public class ColorRequest
    {
        public int Id { get; set; }

        public string Color { get; set; }

        public DateTime ModifiedDate{ get; set; }
    }
}
