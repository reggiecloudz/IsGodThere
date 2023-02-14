using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Team : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string About { get; set; } = string.Empty;

        public long OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }

        public virtual ICollection<TeamOrganizer> Organizers { get; set; } = new List<TeamOrganizer>();
        public virtual ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}