using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class updateUserInfo
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string currentPassword { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }
}
