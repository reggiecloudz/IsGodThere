using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Article : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        // organization.Name.ToLower().Replace(" ", "-");
        [ScaffoldColumn(false)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Content { get; set; } = string.Empty;

        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}