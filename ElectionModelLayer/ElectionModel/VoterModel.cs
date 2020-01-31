using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionModelLayer.ElectionModel
{
    public class VoterModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MobileNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [ForeignKey("CandidateModel")]
        public int CandidateId { get; set; }
    }

}
