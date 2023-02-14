using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IsGodThere.Domain
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string FullName { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string ZipCode { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public virtual Member? Member { get; set; }

        public virtual Organizer? Organizer { get; set; }
        
        public virtual ICollection<UserNotification> Notifications { get; set; } = new List<UserNotification>();
        public virtual ICollection<ChatUser> Chats { get; set; } = new List<ChatUser>();
    }
}