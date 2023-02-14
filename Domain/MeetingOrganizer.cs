using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class MeetingOrganizer : Entity
    {
        public bool IsLead { get; set; } = false;
        
        public string OrganizerId { get; set; } = string.Empty;
        public virtual Organizer? Organizer { get; set; }

        public long MeetingId { get; set; }
        public virtual Meeting? Meeting { get; set; }
    }
}