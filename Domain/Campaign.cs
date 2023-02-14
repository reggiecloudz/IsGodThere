using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Campaign : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public long CauseId { get; set; }
        public virtual Cause? Cause { get; set; }

        public long OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }

        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}