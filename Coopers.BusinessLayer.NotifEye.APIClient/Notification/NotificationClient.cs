using System.Threading.Tasks;
using System.Collections.Generic;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Linq;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class NotificationClient : INotificationClient
    {

        #region PRIVATE MEMBERS

        private readonly IHttpService _httpService;

        #endregion
  

        #region CONSTRUCTOR

        public NotificationClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region PUBLIC MEMBERS     


        #region NOTIFEYEAPI

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 12 hour window)
        /// </summary>
        /// <param name="Minutes">Number of minutes past Notifications will be returned</param>
        /// <param name="LastSentNotificationID">Limits notification results to notifications sent after this ID</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        public async Task<object> GetRecentlySentNotifications(short Minutes, long LastSentNotificationID = 0, long SensorID = 0)
        {
            string filter = string.Format("Minutes={0}&", Minutes);

            if (LastSentNotificationID != 0)
            {
                filter += string.Format("LastSentNotificationID={0}&", LastSentNotificationID);
            }
            if (SensorID != 0)
            {
                filter += string.Format("SensorID={0}&", SensorID);
            }

            filter = filter.EndsWith("&") ? filter.Remove(filter.Length - 1) : filter;


            return await _httpService.GetAsAsync<object>("RecentlySentNotifications", filter, false, false);
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
            string filter = string.Format("From={0}&To={1}&", From, To);

            if (SensorID != 0)
            {
                filter += string.Format("SensorID={0}&", SensorID);
            }

            filter = filter.EndsWith("&") ? filter.Remove(filter.Length - 1) : filter;

            return await _httpService.GetAsAsync<object>("SentNotifications", filter, false, false);
        }

        /// <summary>
        /// Returns the list of sensors that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>sensor list</returns>
        public async Task<object> GetSensorsAssignedToNotification(long NotificationID)
        {
            return await _httpService.GetAsAsync<object>("SensorAssignedToNotificaiton", string.Format("NotificationID={0}", NotificationID), false, false);
        }

        /// <summary>
        /// Returns the list of gateways that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>gateway list</returns>
        public async Task<object> GetGatewaysAssignedToNotification(long NotificationID)
        {
            return await _httpService.GetAsAsync<object>("GatewayAssignedToNotificaiton", string.Format("NotificationID={0}", NotificationID), false, false);
        }

        /// <summary>
        /// returns all notificaiton on the specified account
        /// </summary>
        /// <param name="AccountID">Brings back a list of all notifications on a specific account</param>
        /// <returns>list of notifications</returns>
        public async Task<object> GetAccountNotifications(long AccountID)
        {
            return await _httpService.GetAsAsync<object>("AccountNotificationList", string.Format("AccountID={0}", AccountID), false, false);
        }

        /// <summary>
        /// Returns a list of Schedules for a specific notificaiton
        /// </summary>
        /// <param name="NotificationID">Brings back a list of all the daily schedules for the specific notification</param>
        /// <param name="Day">Brings back a single day's schedule</param>
        /// <returns>schedule list</returns>
        public async Task<object> GetNotificationScheduleList(long NotificationID, string Day = "")
        {
            string queryParam = string.Format("NotificationID={0}", NotificationID);

            if (!string.IsNullOrEmpty(Day))
            {
                queryParam += string.Format("&Day={0}", Day);
            }

            return await _httpService.GetAsAsync<object>("NotificationScheduleList", queryParam, false, false);
        }

        /// <summary>
        /// sets the Notification active or inactive
        /// </summary>
        /// <param name="NotificationID">Unique identifier of the Notification</param>
        /// <param name="On">on flag that sets the notification on or off based off its value</param>
        /// <returns>true if success else false</returns>
        public async Task<string> ToggleNotification(long NotificationID, bool On)
        {
            var res = (await _httpService.GetAsAsync<string>("ToggleNotification", string.Format("NotificationID={0}&On={1}", NotificationID, On), false, false));

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        #endregion


        #region INTEGRATED API

        public async Task<NotificationInfo> GetNotificationListByGateway(int StartIndex, int Count)
        {
            string queryParam = string.Format("StartIndex={0}&Count={1}", StartIndex, Count);

            return await _httpService.GetAsAsync<NotificationInfo>("notification/GetNotificationListByGateway", queryParam, true, false);
        }

        public async Task<NotificationInfo> GetNotificationListBySensor(int StartIndex, int Count)
        {
            string queryParam = string.Format("StartIndex={0}&Count={1}", StartIndex, Count);

            return await _httpService.GetAsAsync<NotificationInfo>("notification/GetNotificationListBySensor", queryParam, true, false);
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
            string queryParam = string.Format("StartIndex={0}&Count={1}&AccountID={2}", StartIndex, Count, AccountID);

            return await _httpService.GetAsAsync<List<NotificationInfo>>("notification/GetNotificationListByAccountID", queryParam, true, false);
        }

        /// <summary>
        /// Get the sent notification list for a sensor
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        public async Task<List<Notification>> GetSentNotificationListBySensor(long SensorID,int StartIndex, int Count, string FromDate, string ToDate)
        {
            string queryParam = string.Format("StartIndex={0}&Count={1}&FromDate={2}&ToDate={3}&SensorID={4}", StartIndex, Count, FromDate, ToDate, SensorID);

            return await _httpService.GetAsAsync<List<Notification>>("notification/GetSentNotificationListBySensor", queryParam, true, false);
        }


        /// <summary>
        /// Get the sent notification list for a gateway
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        public async Task<List<Notification>> GetSentNotificationListByGateWay(long GatewayID, int StartIndex, int Count, string FromDate, string ToDate)
        {
            //prepare the api path
            string queryParam = string.Format("StartIndex={0}&Count={1}&FromDate={2}&ToDate={3}&GatewayID={4}", StartIndex, Count, FromDate, ToDate, GatewayID);

            return await _httpService.GetAsAsync<List<Notification>>("notification/GetSentNotificationListByGateWay", queryParam, true, false);
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
            List<Notification> notList = new List<Notification>();

            //prepare the api path
            string queryParam = string.Format("StartIndex={0}&Count={1}&FromDate={2}&ToDate={3}&AccountID={4}", StartIndex, Count, FromDate, ToDate, AccountID);

            var res = await _httpService.GetAsAsync<List<SentNotificationResponse>>("notification/GetSentNotificationListByAccountID", queryParam, true, false);

            if (res != null && res.Count > 0 && res[0].Notification != null)
            {
                notList = res[0].Notification;
            }

            return notList;
        }

        /// <summary>
        /// Create a Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Id of the newly created notifcation</returns>
        public async Task<long> CreateNotification(NotitifcationWithSchedules Notification)
        {
            return await _httpService.PostAsAsync<long>("notification/createnotification", Notification);
        }

        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> UpdateNotification(NotitifcationWithSchedules Notification)
        {
            return await _httpService.PutAsAsync<string>("notification/editnotification", Notification);
        }

        #endregion


        #endregion


        #region PRIVATE MEMBERS    

        #endregion


    }
}
