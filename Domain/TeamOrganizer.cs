using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class TeamOrganizer : Entity
    {
        public bool IsLead { get; set; } = false;

        public string OrganizerId { get; set; } = string.Empty;
        public virtual Organizer? Organizer { get; set; }

        public long TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}