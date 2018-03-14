namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPaymentHistoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentHistory",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        StripeChargeID = c.String(),
                        HistoryDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        ProductName = c.String(),
                        AccountID = c.Long(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentHistory");
        }
    }
}
