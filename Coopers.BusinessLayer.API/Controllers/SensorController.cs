using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Sensor Endpoint
    /// </summary>
    [RoutePrefix("api/Sensor")]
    [AuthenticationFilter]
    public class SensorController : ApiController
    {

        #region PRIVATE MEMBERS

        private ISensorApplicationService _sensorApplicationService;

        #endregion


        #region CONSTRUCTOR

        public SensorController(ISensorApplicationService sensorApplicationService)
        {
            _sensorApplicationService = sensorApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs


        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <param name="ApplicationID">Integer (optional)	Filters list to sensor that are this application type</param>
        /// <param name="Status">Integer (optional)	Filters list to sensor that match this status</param>
        /// <param name="Name">String (optional)	Filters list to sensors with names containing this string. (case-insensitive)</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        [HttpGet]
        [Route("SensorList")]
        [ResponseType(typeof(List<SensorDetail>))]
        public async Task<IHttpActionResult> GetSenserList([FromUri]string Name = "", [FromUri]long NetworkID = 0, [FromUri]short ApplicationID = 0, [FromUri]short Status = 0)
        {
            return Ok(await _sensorApplicationService.GetSensorList(Name, NetworkID, ApplicationID, Status));
        }

        [AuthenticationFilter]
        /// <summary>
        /// Returns the sensor detials.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorDetailDTO</returns>
        [HttpGet]
        [Route("Details/{SensorID}")]
        [ResponseType(typeof(SensorDetail))]
        public async Task<IHttpActionResult> GetSensorDetailsByID(int SensorID)
        {
            return Ok(await _sensorApplicationService.GetSensorDetailsByID(SensorID));
        }

        [AuthenticationFilter]
        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 7 day window)
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="FromDate">	Start of range from which messages will be returned</param>
        /// <param name="ToDate">End of range from which messages will be returned</param>
        /// <returns>List of DataMessagesDTO</returns>
        [HttpGet]
        [Route("DataMessages")]
        [ResponseType(typeof(List<DataMessages>))]
        public async Task<IHttpActionResult> GetSensorDataMessagesByID([FromUri]int SensorID, [FromUri]string FromDate = "", [FromUri]string ToDate = "")
        {
            return Ok(await _sensorApplicationService.GetSensorDataMessagesByID(SensorID, FromDate, ToDate));
        }

        #endregion

        #region PUT | APIs


        /// <summary>
        /// Assigns sensor to the specified network
        /// </summary>
        /// <param name="SensorID">Identifier of sensor to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>Success if Sensor assigned successfully</returns>
        [HttpPut]
        [Route("{SensorID}/AssignTo")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> AssignSensor(long SensorID, long NetworkID)
        {
            return Ok(await _sensorApplicationService.AssignSensor(SensorID, NetworkID));
        }

        #endregion

        #region DELETE | APIs

        /// <summary>
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>Success if Sensor removed successfully</returns>
        [HttpDelete]
        [Route("Remove/{SensorID}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> RemoveSensor(long SensorID)
        {
            return Ok(await _sensorApplicationService.RemoveSensor(SensorID));
        }

        #endregion

        #endregion


    }
}
