using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Applicant : Entity
    {
        public long Id { get; set; }

        public bool IsAccepted { get; set; }

        [DataType(DataType.Text)]
        public string Qualifications { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public long OpportunityId { get; set; }
        public virtual Opportunity? Opportunity { get; set; }
    }
}