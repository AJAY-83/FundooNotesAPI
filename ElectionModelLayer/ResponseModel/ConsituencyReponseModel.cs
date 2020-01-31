using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionModelLayer.ResponseModel
{
    public class ConsituencyReponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
