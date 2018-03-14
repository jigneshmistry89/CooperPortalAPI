namespace Coopers.BusinessLayer.Database.Domain
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public partial class CooperAtkinEntities : DbContext
    {
        public CooperAtkinEntities()
            : base("name=CooperAtkinEntities")
        {
            Database.SetInitializer(
            new MigrateDatabaseToLatestVersion<CooperAtkinEntities, Configuration>("CooperAtkinEntities"));
        }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<TaxableStates> TaxableStates { get; set; }

        public virtual DbSet<NetworkLocation> NetworkLocation { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<LocationNetwork> LocationNetworks { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<PaymentHistory> PaymentHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
