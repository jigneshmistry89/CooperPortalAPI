namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTableTaxableStates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxableStates",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        StateCode = c.String(),
                        StateName = c.String(),
                        Tax = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaxableStates");
        }
    }
}
