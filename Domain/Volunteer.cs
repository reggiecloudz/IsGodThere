using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Volunteer : Entity
    {
        public string MemberId { get; set; } = string.Empty;
        public virtual Member? Member { get; set; }
        
        public long OpportunityId { get; set; }
        public virtual Opportunity? Opportunity { get; set; }
    }
}