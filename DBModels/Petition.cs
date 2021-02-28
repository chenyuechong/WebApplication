using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.DBModels
{
    public partial class Petition
    {
        public Petition()
        {
            Signatures = new HashSet<Signature>();
        }

        public int PetitionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string PhotoFilename { get; set; }

        public virtual User Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Signature> Signatures { get; set; }
    }
}
