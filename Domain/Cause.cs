using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Cause : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    }
}