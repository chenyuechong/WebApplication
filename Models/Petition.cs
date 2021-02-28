using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Petition
    {
        public int title { get; set; }

        public string description { get; set; }

        public string closingDate { get; set; }

        public string categoryId { get; set; }


    }
}
