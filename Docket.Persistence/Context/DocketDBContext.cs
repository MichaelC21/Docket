using Docket.Core.Entities;
using Docket.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Docket.Persistence.Context
{
    public class DocketDBContext : DbContext
    {
        public DocketDBContext(DbContextOptions<DocketDBContext> options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(builder =>
            {
                builder.HasKey(j => j.JobID);
                builder.Property(j => j.JobID)
                    .IsRequired();

                builder.Property(j => j.Payload)
                    .HasMaxLength(500);

                    
                builder.Property(j => j.Handler)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(j => j.Status)
                    .IsRequired()
                    .HasDefaultValue(JobStatus.Queued);
                
                builder.Property(j => j.Attempted)
                    .IsRequired()
                    .HasDefaultValue(0);

                builder.Property(j => j.MaxAttempts)
                    .IsRequired()
                    .HasDefaultValue(3);

                builder.Property(j =>  j.ClaimedBy)
                    .HasMaxLength(250);
                
            });
                

        }
    }
}
