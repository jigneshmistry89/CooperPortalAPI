using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface INotificationClient
    {

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 12 hour window)
        /// </summary>
        /// <param name="Minutes">Number of minutes past Notifications will be returned</param>
        /// <param name="LastSentNotificationID">Limits notification results to notifications sent after this ID</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        Task<object> GetRecentlySentNotifications(short Minutes, long LastSentNotificationID = 0, long SensorID = 0);

        /// <summary>
        /// Returns data points recorded in a range of time (limited to 7 days and 5000 records)
        /// </summary>
        /// <param name="From">starting point of the time period for your date range of notifications sent</param>
        /// <param name="To">ending point of the time period for your date range for notifications sent</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        Task<object> GetSentNotifications(string From, string To, long SensorID = 0);

        /// <summary>
        /// Returns the list of sensors that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>sensor list</returns>
        Task<object> GetSensorsAssignedToNotification(long NotificationID);

        /// <summary>
        /// Returns the list of gateways that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>gateway list</returns>
        Task<object> GetGatewaysAssignedToNotification(long NotificationID);

        /// <summary>
        /// sets the Notification active or inactive
        /// </summary>
        /// <param name="NotificationID">Unique identifier of the Notification</param>
        /// <param name="On">on flag that sets the notification on or off based off its value</param>
        /// <returns>true if success else false</returns>
        Task<string> ToggleNotification(long NotificationID, bool On);

        /// <summary>
        /// returns all notificaiton on the specified account
        /// </summary>
        /// <param name="AccountID">Brings back a list of all notifications on a specific account</param>
        /// <returns>list of notifications</returns>
        Task<object> GetAccountNotifications(long AccountID);

        /// <summary>
        /// Returns a list of Schedules for a specific notificaiton
        /// </summary>
        /// <param name="NotificationID">Brings back a list of all the daily schedules for the specific notification</param>
        /// <param name="Day">Brings back a single day's schedule</param>
        /// <returns>schedule list</returns>
        Task<object> GetNotificationScheduleList(long NotificationID,string Day="");

        Task<NotificationInfo> GetNotificationListByGateway(int StartIndex, int Count);

        Task<NotificationInfo> GetNotificationListBySensor(int StartIndex, int Count);

        /// <summary>
        /// Get the Notification List for an Account
        /// </summary>
        /// <param name="StartIndex">Start index</param>
        /// <param name="Count">No of records to retrieve</param>
        /// <param name="AccountID">unique indentofoer for an account</param>
        /// <returns>List of notification info</returns>
        Task<List<NotificationInfo>> GetNotificationListByAccountID(int StartIndex, int Count, long AccountID);


        /// <summary>
        /// Get the sent notification list for a sensor
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        Task<List<Notification>> GetSentNotificationListBySensor(long SensorID,int StartIndex, int Count, string FromDate, string ToDate);

        /// <summary>
        /// Get the sent notification list for a gateway
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        Task<List<Notification>> GetSentNotificationListByGateWay(long GatewayID,int StartIndex, int Count, string FromDate, string ToDate);

        /// <summary>
        /// Get the sent notification list for an Account
        /// </summary>
        /// <param name="AccountID">unique indentofier for the account</param>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        Task<List<Notification>> GetSentNotificationByAccount(long AccountID, int StartIndex, int Count, string FromDate, string ToDate);

        /// <summary>
        /// Create a Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Id of the newly created notifcation</returns>
        Task<long> CreateNotification(NotitifcationWithSchedules Notification);

        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Success/Failure</returns>
        Task<string> UpdateNotification(NotitifcationWithSchedules Notification);
    }
}
