using Coopers.BusinessLayer.Model.Interface;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class SensorClient : ISensorClient
    {


        #region PRIVATE MEMBERS
 
        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public SensorClient(IHttpContextProvider httpContextProvider, IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region PUBLIC MEMBERS     

        #region NOTIFEYEAPI API

        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <param name="ApplicationID">Integer (optional)	Filters list to sensor that are this application type</param>
        /// <param name="Status">Integer (optional)	Filters list to sensor that match this status</param>
        /// <param name="Name">String (optional)	Filters list to sensors with names containing this string. (case-insensitive)</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        public async Task<object> GetSensorList(string UserName,string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0)
        {
            string filter = "";
            if (!string.IsNullOrEmpty(Name))
            {
                filter = string.Format("Name={0}&", Name);
            }
            if (NetworkID != 0)
            {
                filter += string.Format("NetworkID={0}&", NetworkID);
            }
            if (ApplicationID != 0)
            {
                filter += string.Format("ApplicationID={0}&", ApplicationID);
            }
            if (Status != 0)
            {
                filter += string.Format("Status={0}&", Status);
            }

            filter = filter.EndsWith("&") ? filter.Remove(filter.Length - 1) : filter;

            if (string.IsNullOrEmpty(UserName))
            {
                return await _httpService.GetAsAsync<object>("SensorListExtended", filter, false, false);
            }
            else
            {
                return await _httpService.GetWithUserAsync<object>(UserName, "SensorListExtended", filter, false, false);
            }
        }

        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        public async Task<List<SensorDetail>> GetSensorListByNetworkID(long NetworkID, string UserName="")
        {
            return JsonConvert.DeserializeObject<List<SensorDetail>>((await GetSensorList(UserName,"", NetworkID)).ToString());
        }


        /// <summary>
        /// Returns the sensor detials.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorDetail</returns>
        public async Task<SensorDetail> GetSensorDetailsByID(long SensorID)
        {
            return await _httpService.GetAsAsync<SensorDetail>("SensorGet", string.Format("SensorID={0}", SensorID), false, false);
        }

        /// <summary>
        /// Returns the sensor object with extended Properties.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorExtendedDetail model</returns>
        public async Task<SensorExtendedDetail> GetSensorExtendedDetailsByID(long SensorID)
        {
            return await _httpService.GetAsAsync<SensorExtendedDetail>("SensorGetExtended", string.Format("SensorID={0}", SensorID), false, false);
        }

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 7 day window)
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="FromDate">	Start of range from which messages will be returned</param>
        /// <param name="ToDate">End of range from which messages will be returned</param>
        /// <returns>List of DataMessages</returns>
        public async Task<List<DataMessages>> GetSensorDataMessages(long SensorID, string FromDate, string ToDate)
        {
            string queryParams = string.Format("SensorID={0}&fromDate={1}&toDate={2}", SensorID, FromDate, ToDate);
            return await _httpService.GetAsAsync<List<DataMessages>>("SensorDataMessages", queryParams, false,false);
        }

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 1 day window)
        /// </summary>
        /// <param name="SensorID">	Unique identifier of the sensor</param>
        /// <param name="Minutes">Number of minutes past messages will be returned</param>
        /// <param name="LastMessageID">(optional)	Only return messages received after this message ID</param>
        /// <returns>List of datamessages</returns>
        public async Task<List<DataMessages>> GetSensorRecentDataMessages(long SensorID, int Minutes, long LastMessageID = 0)
        {
            return await _httpService.GetAsAsync<List<DataMessages>>("SensorRecentDataMessages", string.Format("SensorID={0}&Minutes={1}", SensorID, Minutes), false, false);
        }

       

        /// <summary>
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>true/false</returns>
        public async Task<string> RemoveSensor(long SensorID)
        {
            var res = await _httpService.GetAsAsync<string>("RemoveSensor", string.Format("SensorID={0}", SensorID), false, false);

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        /// <summary>
        /// Sets the display name of the sensor
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="SensorName">Name to give the sensor</param>
        /// <returns></returns>
        public async Task<string> SensorSetName(long SensorID,string SensorName)
        {
            var res = await _httpService.GetAsAsync<string>("SensorSetName", string.Format("SensorID={0}&SensorName={1}", SensorID, SensorName), false, false);

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        /// <summary>
        /// Sets the heartbeat intervals of the sensor
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="ReportInterval">Standard state heart beat</param>
        /// <param name="ActiveStateInterval">Aware state heart beat</param>
        /// <returns></returns>
        public async Task<string> SensorSetHeartbeat(long SensorID, double ReportInterval,double ActiveStateInterval)
        {
            var queryParam = string.Format("SensorID={0}&ReportInterval={1}&ActiveStateInterval={2}", SensorID, ReportInterval, ActiveStateInterval);

            var res = await _httpService.GetAsAsync<string>("SensorSetHeartbeat", queryParam, false, false);

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        /// <summary>
        /// Creates/Updates sensor attribute.
        /// </summary>
        /// <param name="SensorAttribute">Sensor attribute model</param>
        /// <returns>Created/Updated Sensor attribute model</returns>
        public async Task<Model.DTO.SensorAttribute> SensorAttributeSet(Model.DTO.SensorAttribute SensorAttribute)
        {
            var queryParam = string.Format("SensorID={0}&Name={1}&Value={2}", SensorAttribute.SensorID, SensorAttribute.Name, SensorAttribute.Value);

           return await _httpService.GetAsAsync<Model.DTO.SensorAttribute>("SensorAttributeSet", queryParam, false, false);
        }

        /// <summary>
        /// Returns the list of attributes that belong to a sensor.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>List of sensor Attributes</returns>
        public async Task<List<Model.DTO.SensorAttribute>> SensorAttributes(long SensorID)
        {
            var queryParam = string.Format("SensorID={0}", SensorID);

            return await _httpService.GetAsAsync<List<Model.DTO.SensorAttribute>>("SensorAttributes", queryParam, false, false);
        }

        #endregion


        #region INTEGRATED API

        /// <summary>
        /// Get the note for a given SensorID
        /// </summary>
        /// <param name="SensorID">Unique identofier for the Sensor</param>
        /// <returns>Note value</returns>
        public async Task<string> GetSensorNote(long SensorID)
        {
            var sensorMessage = await _httpService.GetAsAsync<Model.DTO.SensorNoteResponse>("sensor/GetSensorNote", string.Format("Sensorid={0}", SensorID), true,false);
            return sensorMessage.Message;
        }

        /// <summary>
        /// Update the Note for a given SensorID
        /// </summary>
        /// <param name="SensorNote">SensorNote update model</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> SetSensorNote(Model.DTO.UpdateSensorNote SensorNote)
        {
            var sensorMessage = await _httpService.PutAsAsync<Model.DTO.SensorNoteResponse>("sensor/SetSensorNote", SensorNote);
            return sensorMessage.Message;
        }

        /// <summary>
        /// Create a Sensor
        /// </summary>
        /// <param name="CreateSensor">Create sensor model</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> CreateSensor(Model.DTO.CreateSensor Sensor)
        {
            var res = await _httpService.PostAsAsync<string>("sensor/CreateSensor", Sensor, false);

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        /// <summary>
        /// Assigns sensor to the specified network
        /// </summary>
        /// <param name="SensorID">Identifier of sensor to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <param name="CheckDigit">Check digit to prevent unauthorized movement of sensors</param>
        /// <returns>true/false</returns>
        public async Task<string> AssignSensor(long SensorID, long NetworkID, string CheckDigit)
        {
            //Create the URI
            var method = string.Format("sensor/assignsensor?sensorid={0}&NetworkID={1}&sensorCode={2}", SensorID, NetworkID, CheckDigit);

            //Make the call
            var res = await _httpService.PostAsAsync<string>(method, null, false);

            if (res != "Success")
            {
                throw new System.Exception(res);
            }

            return res;
        }

        #endregion

        #endregion


        #region PRIVATE MEMBERS     

        #endregion


    }
}
