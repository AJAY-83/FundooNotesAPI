using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionModelLayer.ElectionModel
{
   public class CandidateModel
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey("PartyModel")]
        public int PartyId { get; set; }

        [ForeignKey("ConsituencyModel")]
        public int ConsituencyId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
