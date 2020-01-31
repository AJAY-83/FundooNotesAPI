using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionModelLayer.ResponseModel
{
   public class PartyResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
