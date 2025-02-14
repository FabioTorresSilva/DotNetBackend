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
        /// Gets or sets the collection of Addresses in the database.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

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
        /// Gets or sets the collection of WaterAnalysises in the database.
        /// </summary>
        public DbSet<WaterAnalysis> WaterAnalysises { get; set; }

    }
}
