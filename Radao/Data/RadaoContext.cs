using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radao.Models;


namespace Radao.Data
{
    /// <summary>
    /// Represents the database context for the Radao project.
    /// </summary>
    public class RadaoContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionContext"/> class.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public RadaoContext(DbContextOptions<RadaoContext> options) : base(options)
        { }


        /// <summary>
        /// Gets or sets the collection of ContinuousUseDevices in the database.
        /// </summary>
        public DbSet<ContinuousUseDevice> ContinuousUseDevices { get; set; }

        /// <summary>
        /// Gets or sets the collection of Devices in the database.
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the collection of Fountains in the database.
        /// </summary>
        public DbSet<Fountain> Fountains { get; set; }

        /// <summary>
        /// Gets or sets the collection of WaterAnalysis in the database.
        /// </summary>
        public DbSet<WaterAnalysis> WaterAnalysis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WaterAnalysis>()
                .HasOne(w => w.Fountain)
                .WithMany(f => f.WaterAnalysis) // Ensure this matches your navigation property
                .HasForeignKey(w => w.FountainId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete

            modelBuilder.Entity<WaterAnalysis>()
                .HasOne(w => w.Device)
                .WithMany() // If Device has a navigation property, update accordingly
                .HasForeignKey(w => w.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fountain>()
                .HasOne(f => f.ContinuousUseDevice)
                .WithMany() // If Device has a navigation property, update accordingly
                .HasForeignKey(f => f.ContinuousUseDeviceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
