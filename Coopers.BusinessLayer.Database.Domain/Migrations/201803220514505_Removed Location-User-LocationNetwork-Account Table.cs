namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLocationUserLocationNetworkAccountTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Location", "AccountID", "dbo.Account");
            DropForeignKey("dbo.LocationNetwork", "LocationID", "dbo.Location");
            DropIndex("dbo.User", new[] { "AccountID" });
            DropIndex("dbo.LocationNetwork", new[] { "LocationID" });
            DropIndex("dbo.Location", new[] { "AccountID" });
            DropTable("dbo.Account");
            DropTable("dbo.User");
            DropTable("dbo.LocationNetwork");
            DropTable("dbo.Location");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationNetwork",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        LocationID = c.Long(nullable: false),
                        NetworkID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateIndex("dbo.Location", "AccountID");
            CreateIndex("dbo.LocationNetwork", "LocationID");
            CreateIndex("dbo.User", "AccountID");
            AddForeignKey("dbo.LocationNetwork", "LocationID", "dbo.Location", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Location", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
            AddForeignKey("dbo.User", "AccountID", "dbo.Account", "AccountID", cascadeDelete: true);
        }
    }
}
