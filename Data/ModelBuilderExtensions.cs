using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IsGodThere.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                new IdentityRole 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Organizer",
                    NormalizedName = "ORGANIZER"
                },
                new IdentityRole 
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            });
        }
    }
}