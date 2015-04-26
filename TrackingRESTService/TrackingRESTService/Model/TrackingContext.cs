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
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<TrackingState> TrackingStates { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
       // public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasMany(x => x.states).WithRequired().WillCascadeOnDelete();
        }
    }
}
