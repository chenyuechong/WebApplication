using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.DBModels
{
    public partial class Signature
    {
        public int SignatoryId { get; set; }
        public int PetitionId { get; set; }
        public DateTime SignedDate { get; set; }

        public virtual Petition Petition { get; set; }
        public virtual User Signatory { get; set; }
    }
}
