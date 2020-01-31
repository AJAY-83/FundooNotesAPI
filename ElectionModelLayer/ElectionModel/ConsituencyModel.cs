using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ElectionModelLayer.ElectionModel
{
   public class ConsituencyModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
