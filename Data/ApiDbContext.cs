using TuksSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TuksSyncAPI.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<EventInfo> EventInfos { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the EventInfo entity
            modelBuilder.Entity<EventInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.TicketPrice).HasColumnType("decimal(18,2)");
            });
        }
    }
}