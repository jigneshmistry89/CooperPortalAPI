using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{

    public class NotificationInfo
    {
        public NotificationDetail Notification { get; set; }

        public List<Device> Devices { get; set; }

        public List<UserNotificationInfo> Users { get; set; }

    }

    public class NotificationDetail
    {
        public long NotificationID { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string NotificationClass { get; set; }

        public bool Active { get; set; }

        public DateTime LastDateSent { get; set; }

        public double Threshold { get; set; }

        public string Comparer { get; set; }

        public float Snooze { get; set; }

        public long AdvancedNotificationID { get; set; }

        public string AdvanceNotificationName { get; set; }

        public string AdvancedNotificationType { get; set; }

    }

    public class Device
    {
        public long DeviceID { get; set; }

        public string DeviceName { get; set; }

        public string DeviceType { get; set; }

        public string DeviceCategory { get; set; }

    }

    public class UserNotificationInfo
    {
        public long UserID { get; set; }

        public string UserName { get; set; }

        public string SMSNumber { get; set; }

        public string Email { get; set; }

        public bool NotifyThroughEmail { get; set; }

        public bool NotifyThroughPhone { get; set; }
    }

}