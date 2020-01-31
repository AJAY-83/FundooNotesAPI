using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ElectionModelLayer.ElectionModel
{
    public class AdminModel
    {
        [Key]
        public int Id { get; set; }

        public string  FirstName { get; set; }

        public string LastName { get; set; }

        public int MobileNumber { get; set; }

        public string Email { get; set; }

        public string Passwrod { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
