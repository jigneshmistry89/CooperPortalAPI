namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        CustomerID = c.Long(nullable: false, identity: true),
                        AccountID = c.Long(nullable: false),
                        UserName = c.String(maxLength: 1000),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        NotificationEmail = c.String(),
                        NotificationPhone = c.String(),
                        NotificationPhone2 = c.String(),
                        SMSCarrierID = c.Int(),
                        PasswordExpired = c.Boolean(),
                        IsAdmin = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreateDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        SendMaintenanceNotificationToEmail = c.Short(),
                        SendMaintenanceNotificationToPhone = c.Short(),
                        DirectSMS = c.Short(),
                        SendSensorNotificationToText = c.Short(),
                        SendSensorNotificationToVoice = c.Short(),
                        PasswordChangeDate = c.DateTime(),
                        FailedLoginCount = c.Short(),
                        GUID = c.Guid(),
                        MondayScheduleID = c.Short(),
                        TuesdayScheduleID = c.Short(),
                        WednesdayScheduleID = c.Short(),
                        ThursdayScheduleID = c.Short(),
                        FridayScheduleID = c.Short(),
                        SaturdayScheduleID = c.Short(),
                        SundayScheduleID = c.Short(),
                        AlwaysSend = c.Short(),
                        Image = c.String(),
                        ImageName = c.String(),
                        ImageWidth = c.Double(),
                        ImageHeight = c.Double(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.LocationNetwork",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        LocationID = c.Long(nullable: false),
                        NetworkID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProfileName = c.String(),
                        AccountID = c.Long(nullable: false),
                        Title = c.String(nullable: false, maxLength: 1000),
                        Address = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.AccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocationNetwork", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Location", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "AccountID", "dbo.Account");
            DropIndex("dbo.Location", new[] { "AccountID" });
            DropIndex("dbo.LocationNetwork", new[] { "LocationID" });
            DropIndex("dbo.User", new[] { "AccountID" });
            DropTable("dbo.Location");
            DropTable("dbo.LocationNetwork");
            DropTable("dbo.User");
            DropTable("dbo.Account");
        }
    }
}
