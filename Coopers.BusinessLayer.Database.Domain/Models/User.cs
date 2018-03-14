using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public long CustomerID { get; set; }

        public long AccountID { get; set; }

        [StringLength(1000)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NotificationEmail { get; set; }

        public string NotificationPhone { get; set; }

        public string NotificationPhone2 { get; set; }

        public int? SMSCarrierID { get; set; }

        public bool? PasswordExpired { get; set; }

        public bool? IsAdmin { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? IsActive { get; set; }

        public short? SendMaintenanceNotificationToEmail { get; set; }

        public short? SendMaintenanceNotificationToPhone { get; set; }

        public short? DirectSMS { get; set; }

        public short? SendSensorNotificationToText { get; set; }

        public short? SendSensorNotificationToVoice { get; set; }

        public DateTime? PasswordChangeDate { get; set; }

        public short? FailedLoginCount { get; set; }
        public Guid? GUID { get; set; }

        public short? MondayScheduleID { get; set; }
        public short? TuesdayScheduleID { get; set; }
        public short? WednesdayScheduleID { get; set; }
        public short? ThursdayScheduleID { get; set; }
        public short? FridayScheduleID { get; set; }
        public short? SaturdayScheduleID { get; set; }
        public short? SundayScheduleID { get; set; }
        public short? AlwaysSend { get; set; }
        public string Image { get; set; }
        public string ImageName { get; set; }
        public double? ImageWidth { get; set; }
        public double? ImageHeight { get; set; }
        public string Title { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
    }
}
