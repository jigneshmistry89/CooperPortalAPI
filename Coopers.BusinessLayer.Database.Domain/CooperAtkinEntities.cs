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

        public virtual DbSet<TaxableStates> TaxableStates { get; set; }

        public virtual DbSet<AccountLocation> AccountLocation { get; set; }

        public virtual DbSet<NetworkLocation> NetworkLocation { get; set; }

        public virtual DbSet<PaymentHistory> PaymentHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
