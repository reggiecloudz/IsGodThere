using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Topic : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Description { get; set; } = string.Empty;

        public long ForumId { get; set; }
        public virtual Forum? Forum { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}