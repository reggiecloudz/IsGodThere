using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public class ChatMessage : Entity
    {
        public long Id { get; set; }
        
        public string Nick { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public long ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
