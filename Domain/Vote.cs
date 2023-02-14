using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class Vote : Entity
    {
        public long Id { get; set; }

        public VoteType Type { get; set; }

        public long PostId { get; set; }

        public virtual Post? Post { get; set; }

        public string UserId { get; set; } = string.Empty;

        public virtual ApplicationUser? User { get; set; }
    }
}