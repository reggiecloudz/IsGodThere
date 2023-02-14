using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Opportunity : Entity
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool IsOpen { get; set; } = true;

        // organization.Title.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public int NumberNeeded { get; set; }

        [DataType(DataType.Text)]
        public string Requirements { get; set; } = string.Empty;

        public long OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
        public virtual ICollection<Volunteer> Volunteers { get; set; } = new List<Volunteer>();
    }
}