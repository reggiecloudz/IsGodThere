#nullable disable
using IsGodThere.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IsGodThere.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MeetingMember> MeetingMembers { get; set; }
        public DbSet<MeetingOrganizer> MeetingOrganizers { get; set; }
        public DbSet<TeamOrganizer> TeamOrganizers { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Article> Articles { get; set; } 
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            builder.Entity<ChatUser>(cuser =>
            {
                cuser.HasKey(cu => new { cu.UserId, cu.ChatId });

                cuser.HasOne(cu => cu.Chat)
                    .WithMany(c => c.Users)
                    .HasForeignKey(cu => cu.ChatId)
                    .IsRequired();

                cuser.HasOne(cu => cu.User)
                    .WithMany(u => u.Chats)
                    .HasForeignKey(cu => cu.UserId)
                    .IsRequired();
            });

            builder.Entity<UserNotification>(notif =>
            {
                notif.HasKey(un => new { un.UserId, un.NotificationId });

                notif.HasOne(un => un.User)
                    .WithMany(m => m.Notifications)
                    .HasForeignKey(un => un.UserId)
                    .IsRequired();

                notif.HasOne(un => un.Notification)
                    .WithMany(n => n.Users)
                    .HasForeignKey(un => un.NotificationId)
                    .IsRequired();
            });

            builder.Entity<MeetingMember>(obj =>
            {
                obj.HasKey(ma => new { ma.MemberId, ma.MeetingId });

                obj.HasOne(ma => ma.Meeting)
                    .WithMany(m => m.Members)
                    .HasForeignKey(ma => ma.MeetingId)
                    .IsRequired();

                obj.HasOne(ma => ma.Member)
                    .WithMany(a => a.Meetings)
                    .HasForeignKey(ma => ma.MemberId)
                    .IsRequired();
            });

            builder.Entity<MeetingOrganizer>(obj =>
            {
                obj.HasKey(mo => new { mo.MeetingId, mo.OrganizerId });

                obj.HasOne(mo => mo.Meeting)
                    .WithMany(m => m.Organizers)
                    .HasForeignKey(mo => mo.MeetingId)
                    .IsRequired();

                obj.HasOne(mo => mo.Organizer)
                    .WithMany(o => o.Meetings)
                    .HasForeignKey(mo => mo.OrganizerId)
                    .IsRequired();
            });

            builder.Entity<TeamOrganizer>(obj =>
            {
                obj.HasKey(to => new { to.TeamId, to.OrganizerId });

                obj.HasOne(to => to.Team)
                    .WithMany(t => t.Organizers)
                    .HasForeignKey(to => to.TeamId)
                    .IsRequired();

                obj.HasOne(to => to.Organizer)
                    .WithMany(o => o.Teams)
                    .HasForeignKey(to => to.OrganizerId)
                    .IsRequired();
            });

            builder.Entity<TeamMember>(obj =>
            {
                obj.HasKey(tv => new { tv.TeamId, tv.MemberId });

                obj.HasOne(tv => tv.Team)
                    .WithMany(t => t.Members)
                    .HasForeignKey(tv => tv.TeamId)
                    .IsRequired();

                obj.HasOne(tv => tv.Member)
                    .WithMany(v => v.Teams)
                    .HasForeignKey(tv => tv.MemberId)
                    .IsRequired();
            });

            builder.Entity<Volunteer>(obj =>
            {
                obj.HasKey(v => new { v.OpportunityId, v.MemberId });

                obj.HasOne(v => v.Opportunity)
                    .WithMany(o => o.Volunteers)
                    .HasForeignKey(v => v.OpportunityId)
                    .IsRequired();

                obj.HasOne(v => v.Member)
                    .WithMany(m => m.VolunteerWork)
                    .HasForeignKey(v => v.MemberId)
                    .IsRequired();
            });

            // builder.Entity<Auction>()
            //     .HasMany<Bid>(a => a.Bids)
            //     .WithOne(b => b.Auction)
            //     .HasForeignKey(b => b.AuctionId)
            //     .OnDelete(DeleteBehavior.Cascade);


            // builder.SeedRoles();
        }

        public override int SaveChanges()
            {
                var changedEntities = ChangeTracker.Entries();

                foreach (var changedEntity in changedEntities)
                {
                    if (changedEntity.Entity is Entity)
                    {
                        var entity = changedEntity.Entity as Entity;
                        if (changedEntity.State == EntityState.Added)
                        {
                            entity.Created = DateTime.Now;
                            entity.Updated = DateTime.Now;

                        }
                        else if (changedEntity.State == EntityState.Modified)
                        {
                            entity.Updated = DateTime.Now;
                        }
                    }

                }
                return base.SaveChanges();
            }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (var changedEntity in changedEntities)
            {
                if (changedEntity.Entity is Entity)
                {
                    var entity = changedEntity.Entity as Entity;
                    if (changedEntity.State == EntityState.Added)
                    {
                        entity.Created = DateTime.Now;
                        entity.Updated = DateTime.Now;

                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        entity.Updated = DateTime.Now;
                    }
                }
            }
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
    
}