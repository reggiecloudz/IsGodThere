using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Meeting : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public long OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }

        public ICollection<MeetingOrganizer> Organizers { get; set; } = new List<MeetingOrganizer>();
        public ICollection<MeetingMember> Members { get; set; } = new List<MeetingMember>();
    }
}