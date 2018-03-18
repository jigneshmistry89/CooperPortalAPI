using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class Notification
    {
        public long NotificationID { get; set; }

        public string Name { get; set; }

        public string NotificationClass { get; set; }

        public long DeviceID { get; set; }

        public string DeviceName { get; set; }

        public string DeviceType { get; set; }

        public string Reading { get; set; }

        public DateTime NotificationDate { get; set; }

        public string Text { get; set; }

        public long SentNotificationID { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; }

        public string SMSNumber { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

    }

    public class SentNotificationResponse
    {
        public List<Notification> Notification { get; set;  }

    }

    public class NotitifcationWithSchedules
    {
        public long NotificationID { get; set; }

        public string Text { get; set; }

        public string Name { get; set; }

        public string Scale { get; set; }

        public string NotificationClass { get; set; }

        public string CompareType { get; set; }

        public double ComparerValue { get; set; }

        public long AccountID { get; set; }

        public long AdvancedNotificationID { get; set; }

        public int MonnitApplicationID { get; set; }

        //public long GatewayID { get; set; }

        //public long SensorID { get; set; }

        public float Snooze { get; set; }

        //public string StartTime { get; set; }

        //public string EndTime { get; set; }

        public List<Schedule> Schedule { get; set; }

        public List<long> GatewayList { get; set; }

        public List<long> SensorList { get; set; }

        public List<UserNotification> UserList { get; set; }

    }

    public class UserNotification
    {
        public long UserID { get; set; }

        public int NotificationType { get; set; }
    }

    public class Schedule
    {
        public int DayOfWeek { get; set; }

        public int ScheduleDay { get; set; }

        public string FirstTime { get; set; }

        public string SecondTime { get; set; }

    }

}
