namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnLatitudeandLongitudetoNetworkLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NetworkLocation", "Latitude", c => c.Single(nullable: false));
            AddColumn("dbo.NetworkLocation", "Longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NetworkLocation", "Longitude");
            DropColumn("dbo.NetworkLocation", "Latitude");
        }
    }
}
