using System;
using System.Threading.Tasks;
using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Collections.Generic;
using Coopers.BusinessLayer.Model.DTO;

namespace Coopers.BusinessLayer.Services.Services
{
    public class NotificationApplicationService : INotificationApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly INotificationClient _notificationClient;

        #endregion


        #region CONSTRUCTOR

        public NotificationApplicationService(INotificationClient notificationClient)
        {
            _notificationClient = notificationClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 12 hour window)
        /// </summary>
        /// <param name="Minutes">Number of minutes past Notifications will be returned</param>
        /// <param name="LastSentNotificationID">Limits notification results to notifications sent after this ID</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        public async Task<object> GetRecentlySentNotifications(short Minutes, long LastSentNotificationID = 0, long SensorID = 0)
        {
            return await _notificationClient.GetRecentlySentNotifications(Minutes, LastSentNotificationID, SensorID);
        }

        /// <summary>
        /// Returns data points recorded in a range of time (limited to 7 days and 5000 records)
        /// </summary>
        /// <param name="From">starting point of the time period for your date range of notifications sent</param>
        /// <param name="To">ending point of the time period for your date range for notifications sent</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        public async Task<object> GetSentNotifications(string From, string To, long SensorID = 0)
        {
            return await _notificationClient.GetSentNotifications(From, To, SensorID);
        }

        /// <summary>
        /// Returns the list of sensors that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>sensor list</returns>
        public async Task<object> GetSensorsAssignedToNotification(long NotificationID)
        {
            return await _notificationClient.GetSensorsAssignedToNotification(NotificationID);
        }

        /// <summary>
        /// Returns the list of gateways that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>gateway list</returns>
        public async Task<object> GetGatewaysAssignedToNotification(long NotificationID)
        {
            return await _notificationClient.GetGatewaysAssignedToNotification(NotificationID);
        }

        /// <summary>
        /// returns all notificaiton on the specified account
        /// </summary>
        /// <param name="AccountID">Brings back a list of all notifications on a specific account</param>
        /// <returns>list of notifications</returns>
        public async Task<object> GetAccountNotifications(long AccountID)
        {
            return await _notificationClient.GetAccountNotifications(AccountID);
        }

        /// <summary>
        /// Returns a list of Schedules for a specific notificaiton
        /// </summary>
        /// <param name="NotificationID">Brings back a list of all the daily schedules for the specific notification</param>
        /// <param name="Day">Brings back a single day's schedule</param>
        /// <returns>schedule list</returns>
        public async Task<object> GetNotificationScheduleList(long NotificationID, string Day = "")
        {
            return await _notificationClient.GetNotificationScheduleList(NotificationID, Day);
        }

        public async Task<NotificationInfo> GetNotificationListByGateway(int StartIndex, int Count)
        {
            var res = await _notificationClient.GetNotificationListBySensor(StartIndex, Count);
            return await _notificationClient.GetNotificationListByGateway(StartIndex, Count);
        }

        public async Task<NotificationInfo> GetNotificationListBySensor(int StartIndex, int Count)
        {
            return await _notificationClient.GetNotificationListBySensor(StartIndex, Count);
        }

        /// <summary>
        /// Get the Notification List for an Account
        /// </summary>
        /// <param name="StartIndex">Start index</param>
        /// <param name="Count">No of records to retrieve</param>
        /// <param name="AccountID">unique indentofoer for an account</param>
        /// <returns>List of notification info</returns>
        public async Task<List<NotificationInfo>> GetNotificationListByAccountID(int StartIndex, int Count, long AccountID)
        {
            return await _notificationClient.GetNotificationListByAccountID(StartIndex, Count, AccountID);
        }

        /// <summary>
        /// Get the sent notification list for a sensor
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List if notifications</returns>
        public async Task<List<Notification>> GetSentNotificationListBySensor(long SensorID,int StartIndex, int Count, string FromDate, string ToDate)
        {
            return await _notificationClient.GetSentNotificationListBySensor(SensorID,StartIndex, Count, FromDate, ToDate);
        }

        /// <summary>
        /// Get the sent notification list for a gateway
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List if notifications</returns>
        public async Task<List<Notification>> GetSentNotificationListByGateWay(long GatewayID,int StartIndex, int Count, string FromDate, string ToDate)
        {
            return await _notificationClient.GetSentNotificationListByGateWay(GatewayID,StartIndex, Count, FromDate, ToDate);
        }

        /// <summary>
        /// Get the sent notification list for an Account
        /// </summary>
        /// <param name="AccountID">unique indentofier for the account</param>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        public async Task<List<Notification>> GetSentNotificationByAccount(long AccountID, int StartIndex, int Count, string FromDate, string ToDate)
        {
            return await _notificationClient.GetSentNotificationByAccount(AccountID, StartIndex, Count, FromDate, ToDate);
        }

        /// <summary>
        /// sets the Notification active or inactive
        /// </summary>
        /// <param name="NotificationID">Unique identifier of the Notification</param>
        /// <param name="On">on flag that sets the notification on or off based off its value</param>
        /// <returns>true if success else false</returns>
        public async Task<string> ToggleNotification(long NotificationID, bool On)
        {
            return await _notificationClient.ToggleNotification(NotificationID, On);
        }

        /// <summary>
        /// Create a Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Id of the newly created notifcation</returns>
        public async Task<long> CreateNotification(NotitifcationWithSchedules Notification)
        {
            return await _notificationClient.CreateNotification(Notification);
        }

        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>No of records updated</returns>
        public async Task<long> UpdateNotification(NotitifcationWithSchedules Notification)
        {
            return await _notificationClient.UpdateNotification(Notification);
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
