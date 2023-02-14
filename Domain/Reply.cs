using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Reply : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public long PostId { get; set; }

        public virtual Post? Post { get; set; }

        public long? ParentId { get; set; }

        public virtual Reply? Parent { get; set; }

        public string UserId { get; set; } = string.Empty;

        public virtual ApplicationUser? User { get; set; }
    }
}