namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPaymentHistoryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentHistory", "CustomerID", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentHistory", "CustomerID", c => c.Int(nullable: false));
        }
    }
}
