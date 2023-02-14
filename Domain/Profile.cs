using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public abstract class Profile : Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string IdentityId { get; set; } = string.Empty;
        public virtual ApplicationUser? Identity { get; set; }
    }
}