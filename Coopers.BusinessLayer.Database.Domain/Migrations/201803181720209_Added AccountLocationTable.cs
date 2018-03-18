namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAccountLocationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLocation",
                c => new
                    {
                        AccountID = c.Long(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountLocation");
        }
    }
}
