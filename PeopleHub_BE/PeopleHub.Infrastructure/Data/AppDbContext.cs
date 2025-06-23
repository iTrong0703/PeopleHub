using PeopleHub.Domain.Entities;

namespace PeopleHub.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AppUser
            modelBuilder.Entity<AppUser>()
               .HasIndex(u => u.UserName)
               .IsUnique();

            // AppUser - UserProfile 1-1
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            // UserProfile - Photo 1-n
            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.Photos)
                .WithOne(p => p.UserProfile)
                .HasForeignKey(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
