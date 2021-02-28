using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.DBModels
{
    public partial class User
    {
        public User()
        {
            Petitions = new HashSet<Petition>();
            Signatures = new HashSet<Signature>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoFilename { get; set; }

        public virtual ICollection<Petition> Petitions { get; set; }
        public virtual ICollection<Signature> Signatures { get; set; }
    }
}
