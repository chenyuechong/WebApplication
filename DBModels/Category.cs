using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.DBModels
{
    public partial class Category
    {
        public Category()
        {
            Petitions = new HashSet<Petition>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Petition> Petitions { get; set; }
    }
}
