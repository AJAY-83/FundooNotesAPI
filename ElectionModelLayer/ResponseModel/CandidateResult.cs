using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionModelLayer.ResponseModel
{
   public class CandidateResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PartyName { get; set; }
        public int TotalVotes { get; set; }

    }
}
