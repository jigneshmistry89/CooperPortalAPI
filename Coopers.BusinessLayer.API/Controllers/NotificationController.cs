using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Notification Endpoint
    /// </summary>
    [RoutePrefix("api/Notification")]
    [AuthenticationFilter]
    public class NotificationController : ApiController
    {

        #region PRIVATE MEMBERS

        private INotificationApplicationService _notificationApplicationService;

        #endregion


        #region CONSTRUCTOR

        public NotificationController(INotificationApplicationService notificationApplicationService)
        {
            _notificationApplicationService = notificationApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 12 hour window)
        /// </summary>
        /// <param name="Minutes">Number of minutes past Notifications will be returned</param>
        /// <param name="LastSentNotificationID">Limits notification results to notifications sent after this ID</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors</param>
        /// <returns>list of notification info</returns>
        [HttpGet]
        [Route("RecentlySentNotifications")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetNotificationList([FromUri]short Minutes, [FromUri]long LastSentNotificationID = 0, [FromUri]long SensorID = 0)
        {
            return Ok(await _notificationApplicationService.GetRecentlySentNotifications(Minutes, LastSentNotificationID, SensorID));
        }

        /// <summary>
        /// Returns data points recorded in a range of time (limited to 7 days and 5000 records)
        /// </summary>
        /// <param name="From">starting point of the time period for your date range of notifications sent</param>
        /// <param name="To">ending point of the time period for your date range for notifications sent</param>
        /// <param name="SensorID">Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</param>
        /// <returns>list of notification info</returns>
        [HttpGet]
        [Route("SentNotifications")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetNotificationList([FromUri]string From = "", [FromUri]string To = "", [FromUri]long SensorID = 0)
        {
            return Ok(await _notificationApplicationService.GetSentNotifications(From, To, SensorID));
        }


        /// <summary>
        /// Returns the list of sensors that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>sensor list</returns>
        [HttpGet]
        [Route("SensorsAssignedTo/{NotificationID}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetSensorsAssignedToNotification(long NotificationID)
        {
            return Ok(await _notificationApplicationService.GetSensorsAssignedToNotification(NotificationID));
        }

        /// <summary>
        /// Returns the list of gateways that belongs to user based on the notification they are assigned to.
        /// </summary>
        /// <param name="NotificationID">Filters list to sensors that belong to this notification id</param>
        /// <returns>gateway list</returns>
        [HttpGet]
        [Route("GatewaysAssignedTo/{NotificationID}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetGatewaysAssignedToNotification(long NotificationID)
        {
            return Ok(await _notificationApplicationService.GetGatewaysAssignedToNotification(NotificationID));
        }

        /// <summary>
        /// returns all notificaiton on the specified account
        /// </summary>
        /// <param name="AccountID">Brings back a list of all notifications on a specific account</param>
        /// <returns>list of notifications</returns>
        [HttpGet]
        [Route("AccountNotifications/{AccountID}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetAccountNotifications(long AccountID)
        {
            return Ok(await _notificationApplicationService.GetAccountNotifications(AccountID));
        }

        /// <summary>
        /// Returns a list of Schedules for a specific notificaiton
        /// </summary>
        /// <param name="NotificationID">Brings back a list of all the daily schedules for the specific notification</param>
        /// <param name="Day">Brings back a single day's schedule</param>
        /// <returns>schedule list</returns>
        [HttpGet]
        [Route("ScheduleList")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetNotificationScheduleList([FromUri]long NotificationID, [FromUri]string Day = "")
        {
            return Ok(await _notificationApplicationService.GetNotificationScheduleList(NotificationID, Day));
        }

        /// <summary>
        /// Get the sent notification list for a sensor
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        [HttpGet]
        [Route("GatewayNotifications")]
        [ResponseType(typeof(List<NotificationInfo>))]
        public async Task<IHttpActionResult> GetNotificationListByGateway(int StartIndex, int Count)
        {
            return (Ok(await _notificationApplicationService.GetNotificationListByGateway(StartIndex, Count)));
        }

        [HttpGet]
        [Route("SensorNotifications")]
        [ResponseType(typeof(List<NotificationInfo>))]
        public async Task<IHttpActionResult> GetNotificationListBySensor(int StartIndex, int Count)
        {
            return (Ok(await _notificationApplicationService.GetNotificationListBySensor(StartIndex, Count)));
        }

        /// <summary>
        /// Get the Notification List for an Account
        /// </summary>
        /// <param name="StartIndex">Start index</param>
        /// <param name="Count">No of records to retrieve</param>
        /// <param name="AccountID">unique indentofoer for an account</param>
        /// <returns>List of notification info</returns>
        [HttpGet]
        [Route("AccountNotifications")]
        [ResponseType(typeof(List<NotificationInfo>))]
        public async Task<IHttpActionResult> GetNotificationListByAccountID(int StartIndex, int Count, long AccountID)
        {
            return Ok(await _notificationApplicationService.GetNotificationListByAccountID(StartIndex, Count, AccountID));
        }

        /// <summary>
        /// Get the sent notification list for a sensor
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="SensorID">unique identifier for the sensor</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        [HttpGet]
        [Route("SentToSensor")]
        [ResponseType(typeof(List<Notification>))]
        public async Task<IHttpActionResult> GetSentNotificationListBySensor(long SensorID, int StartIndex, int Count, string FromDate, string ToDate)
        {
            return (Ok(await _notificationApplicationService.GetSentNotificationListBySensor(SensorID,StartIndex, Count, FromDate, ToDate)));
        }

        /// <summary>
        /// Get the sent notification list for a gateway
        /// </summary>
        /// <param name="StartIndex">offset</param>
        /// <param name="Count">no of records to fetch</param>
        /// <param name="FromDate">start date</param>
        /// <param name="ToDate">end date</param>
        /// <returns>List of notifications</returns>
        [HttpGet]
        [Route("SentToGateway")]
        [ResponseType(typeof(List<Notification>))]
        public async Task<IHttpActionResult> GetSentNotificationListByGateWay(long GatewayID,int StartIndex, int Count, string FromDate, string ToDate)
        {
            return (Ok(await _notificationApplicationService.GetSentNotificationListByGateWay(GatewayID,StartIndex, Count, FromDate, ToDate)));
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
        [HttpGet]
        [Route("SentToAccount")]
        [ResponseType(typeof(List<Notification>))]
        public async Task<IHttpActionResult> GetSentNotificationByAccount(long AccountID, int StartIndex, int Count, string FromDate, string ToDate)
        {
            return (Ok(await _notificationApplicationService.GetSentNotificationByAccount(AccountID, StartIndex, Count, FromDate, ToDate)));
        }
        

        #endregion


        #region PUT | APIs

        /// <summary>
        /// sets the Notification active or inactive
        /// </summary>
        /// <param name="NotificationID">Unique identifier of the Notification</param>
        /// <param name="On">on flag that sets the notification on or off based off its value</param>
        /// <returns>Success if Notification toggeled successfully</returns>
        [HttpPut]
        [Route("ToggleNotification")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> ToggleNotification([FromUri]long NotificationID, [FromUri]bool On)
        {
            return Ok(await _notificationApplicationService.ToggleNotification(NotificationID, On));
        }

        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Success/Failure</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> UpdateNotification(NotitifcationWithSchedules Notification)
        {
            return Ok(await _notificationApplicationService.UpdateNotification(Notification));
        }

        #endregion


        #region POST | APIs

        /// <summary>
        /// Create a Notification
        /// </summary>
        /// <param name="Notification">Notification Model</param>
        /// <returns>Id of the newly created notifcation</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> CreateNotification(NotitifcationWithSchedules Notification)
        {
            return Ok(await _notificationApplicationService.CreateNotification(Notification));
        }

        #endregion


        #region DELETE | APIs


        #endregion


        #endregion


    }
}
