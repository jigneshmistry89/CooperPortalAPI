namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNetworkLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NetworkLocation",
                c => new
                    {
                        CSNetID = c.Long(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CSNetID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NetworkLocation");
        }
    }
}
