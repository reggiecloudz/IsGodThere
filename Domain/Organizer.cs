using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Organizer : Profile
    {
        public OrganizerRole Role { get; set; }
        public long OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }

        public virtual ICollection<MeetingOrganizer> Meetings { get; set; } = new List<MeetingOrganizer>();
        public virtual ICollection<TeamOrganizer> Teams { get; set; } = new List<TeamOrganizer>();
    }
}