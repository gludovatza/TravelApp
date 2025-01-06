using Microsoft.EntityFrameworkCore;

namespace TravelApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTips)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTrip>()
                .HasOne(ut => ut.Trip)
                .WithMany(u => u.UserTips)
                .HasForeignKey(ut => ut.TripId);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice" },
                new User { Id = 2, Name = "Bob" }
            );

            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, Destination = "Paris", Description = "Romantic city" },
                new Trip { Id = 2, Destination = "Rome", Description = "Ancient ruins" }
            );

            modelBuilder.Entity<UserTrip>().HasData(
                new UserTrip { Id = 1, UserId = 1, TripId = 1, Date = new DateTime(1983, 10, 28) },
                new UserTrip { Id = 2, UserId = 1, TripId = 2, Date = new DateTime(1999, 5, 11) },
                new UserTrip { Id = 3, UserId = 2, TripId = 1, Date = new DateTime(2000, 1, 1) },
                new UserTrip { Id = 4, UserId = 2, TripId = 2, Date = new DateTime(2001, 2, 2) }
            );
        }
    }
}
