using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Request
{
   public class AdminLoginRequest
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }      

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
