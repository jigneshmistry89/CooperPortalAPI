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
        /// Update the SensorName and the Heartbeat info
        /// </summary>
        /// <param name="UpdateSensor">Update sensor model</param>
        /// <returns>Success/Failure</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> UpdateSensor(Model.DTO.UpdateSensor UpdateSensor)
        {
            return Ok(await _sensorApplicationService.UpdateSensor(UpdateSensor));
        }

        /// <summary>
        /// Bulk Update sensor details 
        /// </summary>
        /// <param name="UpdateSensors">List of BulkUpdate model</param>
        /// <returns>List of SensorBulkResponse</returns>
        [HttpPut]
        [Route("BulkUpdate")]
        [ResponseType(typeof(List<Model.DTO.SensorBulkResponse>))]
        public async Task<IHttpActionResult> BulkUpdateSensor(List<Model.DTO.UpdateSensor> UpdateSensors)
        {
            return Ok(await _sensorApplicationService.BulkUpdateSensor(UpdateSensors));
        }

        /// <summary>
        /// Creates/Updates sensor attribute.
        /// </summary>
        /// <param name="SensorAttribute">Sensor attribute model</param>
        /// <returns>Created/Updated Sensor attribute model</returns>
        [HttpPut]
        [Route("UpdateAttribute")]
        [ResponseType(typeof(Model.DTO.SensorAttribute))]
        public async Task<IHttpActionResult> UpdateSensorAttribute(Model.DTO.SensorAttribute SensorAttribute)
        {
            return Ok(await _sensorApplicationService.UpdateSensorAttribute(SensorAttribute));
        }


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

        /// <summary>
        /// Assign the sensors to the specified network
        /// </summary>
        /// <param name="SensorBulkAssign">Sensor Bulk Assign Model</param>
        /// <returns>Success if Sensor assigned successfully</returns>
        [HttpPut]
        [Route("BulkAssignTo")]
        [ResponseType(typeof(List<Model.DTO.SensorBulkResponse>))]
        public async Task<IHttpActionResult> BulkAssignSensor([FromBody]Model.DTO.SensorBulkAssign SensorBulkAssign)
        {
            return Ok(await _sensorApplicationService.BulkAssignSensor(SensorBulkAssign));
        }

        #endregion

        #region POST | APIs

        /// <summary>
        /// Create a Sensor
        /// </summary>
        /// <param name="CreateSensor">Create sensor model</param>
        /// <returns>Success/Failure</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> CreateSensor(Model.DTO.CreateSensor CreateSensor)
        {
            return Ok(await _sensorApplicationService.CreateSensor(CreateSensor));
        }


        #endregion

        #region DELETE | APIs

        /// <summary>
        /// Removes the sensors object from the network.
        /// </summary>
        /// <param name="SensorID">unique identofier for the network</param>
        /// <returns>Success if Sensors removed successfully else failure</returns>
        [HttpDelete]
        [Route("{SensorID}/Remove")]
        [ResponseType(typeof(List<Model.DTO.SensorBulkResponse>))]
        public async Task<IHttpActionResult> RemoveSensor(long SensorID)
        {
            return Ok(await _sensorApplicationService.RemoveSensor(SensorID));
        }

        /// <summary>
        /// Removes the sensors from the network.
        /// </summary>
        /// <param name="SensorIDs">List of sensor ids</param>
        /// <returns>Success if Sensors removed successfully else failure</returns>
        [HttpDelete]
        [Route("BulkRemove")]
        [ResponseType(typeof(List<Model.DTO.SensorBulkResponse>))]
        public async Task<IHttpActionResult> RemoveSensor(List<long> SensorIDs)
        {
            return Ok(await _sensorApplicationService.BulkRemoveSensor(SensorIDs));
        }

        #endregion

        #endregion


    }
}
