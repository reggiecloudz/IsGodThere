using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Comment : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public long ArticleId { get; set; }

        public virtual Article? Article { get; set; }

        public long? ParentId { get; set; }

        public virtual Comment? Parent { get; set; }

        public string UserId { get; set; } = string.Empty;

        public virtual ApplicationUser? User { get; set; }
    }
}