using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Member : Profile
    {
        
        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<TeamMember> Teams { get; set; } = new List<TeamMember>();
        public virtual ICollection<Applicant> Applications { get; set; } = new List<Applicant>();
        public virtual ICollection<Volunteer> VolunteerWork { get; set; } = new List<Volunteer>();
        public virtual ICollection<MeetingMember> Meetings { get; set; } = new List<MeetingMember>();
        
    }
}