using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistences
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options) { }
        public DbSet<Notifications_> Notifications { get; set; }
        public DbSet<NotificationLogs_> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationLogs_>()
                        .HasOne(j => j.notification)
                        .WithMany()
                        .HasForeignKey(j => j.notification_id);
        }
    }
}
