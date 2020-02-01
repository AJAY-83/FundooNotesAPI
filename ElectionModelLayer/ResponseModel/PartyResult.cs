using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionModelLayer.ResponseModel
{
   public class PartyResult
    {
        public int Id { get; set; }

        public string PartyName { get; set; }
        public int Total { get; set; }
        public int Won { get; set; }
    }
}
