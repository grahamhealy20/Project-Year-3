namespace TrackingRESTService.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TrackingContext : DbContext
    {
        public TrackingContext()
            : base("name=TrackingContext")
        {
        }

        public virtual DbSet<TrackingState> TrackingStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
