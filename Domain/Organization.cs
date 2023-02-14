using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Organization : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        public virtual ICollection<Organizer> Organizers { get; set; } = new List<Organizer>();
        public virtual ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
        public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
        public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    }
}