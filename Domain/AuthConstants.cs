using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsGodThere.Domain
{
    public static class AuthConstants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Organizer = "Organizer";
            public const string Member = "Member";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireOrganizer = "RequireOrganizer";
            public const string RequireMember = "RequireMember";
        }
    }
}