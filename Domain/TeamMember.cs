using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class TeamMember : Entity
    {
        public string MemberId { get; set; } = string.Empty;
        public virtual Member? Member { get; set; }

        public long TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}