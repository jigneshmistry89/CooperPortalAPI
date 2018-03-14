using AutoMapper;
using Coopers.BusinessLayer.Database.APIClient.Location;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class SensorApplicationService : ISensorApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly ISensorClient _sensorClient;
        private readonly IGatewayClient _gatewayClient;
        private readonly IMapper _mapper;
        private readonly INetworkLocationClient _networkLocationClient;

        #endregion


        #region CONSTRUCTOR

        public SensorApplicationService(ISensorClient sensorClient, IGatewayClient gatewayClient, IMapper mapper, INetworkLocationClient networkLocationClient)
        {
            _sensorClient = sensorClient;
            _gatewayClient = gatewayClient;
            _mapper = mapper;
            _networkLocationClient = networkLocationClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <param name="ApplicationID">Integer (optional)	Filters list to sensor that are this application type</param>
        /// <param name="Status">Integer (optional)	Filters list to sensor that match this status</param>
        /// <param name="Name">String (optional)	Filters list to sensors with names containing this string. (case-insensitive)</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        public async Task<object> GetSensorList(string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0)
        {
            return await _sensorClient.GetSensorList(Name, NetworkID, ApplicationID, Status);
        }

        /// <summary>
        /// Returns the sensor detials.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorDetail</returns>
        public async Task<Model.DTO.SensorDetail> GetSensorDetailsByID(long ID)
        {
            //Get the sensor detail
            var sensorInfo =  await _sensorClient.GetSensorExtendedDetailsByID(ID);

            //Polulate the sensor detail from the sensor info
            var sensorDetail = _mapper.Map<Model.DTO.SensorDetail>(sensorInfo);

            sensorDetail.CorF = await GetCorkForSensor(ID);

            var recentDataMessage = await _sensorClient.GetSensorRecentDataMessages(sensorInfo.SensorID, 1439, sensorInfo.LastDataMessageID);

            if(recentDataMessage.Count > 0)
            {
               
                sensorDetail.DataCelsius = recentDataMessage[0].Data;
                sensorDetail.DataFahrenheit = recentDataMessage[0].PlotValue;
                sensorDetail.DisplayData = recentDataMessage[0].DisplayData;

                var gateway = await _gatewayClient.GatewayGet(recentDataMessage[0].GatewayID);
                if(gateway != null)
                {
                    sensorDetail.GatewayID = gateway.GatewayID;
                    sensorDetail.GatewayName = gateway.Name;
                    sensorDetail.GatewayType = gateway.GatewayType;
                }
            }

            //Get the network detials
            var netWorkLocation = await _networkLocationClient.GetNetworkLocationByID(sensorInfo.CSNetID);

            //Populate the Networkname
            sensorDetail.NetworkName = netWorkLocation.Name;
            
            //retrun the sensor detail
            return sensorDetail;
        }


        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 7 day window)
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="FromDate">	Start of range from which messages will be returned</param>
        /// <param name="ToDate">End of range from which messages will be returned</param>
        /// <returns>List of DataMessages</returns>
        public async Task<List<DataMessages>> GetSensorDataMessagesByID(long ID, string FromDate, string ToDate)
        {
            if(string.IsNullOrEmpty(FromDate) || string.IsNullOrEmpty(ToDate))
            {
                return await _sensorClient.GetSensorRecentDataMessages(ID, 1439);
            }
            else
            {
                return await _sensorClient.GetSensorDataMessages(ID, FromDate, ToDate);
            }
        }

        /// <summary>
        /// Assigns sensor to the specified network
        /// </summary>
        /// <param name="SensorID">Identifier of sensor to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>true/false</returns>
        public async Task<string> AssignSensor(long SensorID, long NetworkID)
        {
            var CheckDigit = CheckDigitGenerator.GenerateSecurityCode(SensorID.ToString());
            return await _sensorClient.AssignSensor(SensorID, NetworkID, CheckDigit);
        }
        
        /// <summary>
        /// Assign the sensors to the given network
        /// </summary>
        /// <param name="SensorBulkAssing">Sensor bulk Assing model</param>
        /// <returns>Success/Failure</returns>
        public async Task<List<SensorBulkResponse>> BulkAssignSensor(Model.DTO.SensorBulkAssign SensorBulkAssing)
        {
            List<SensorBulkResponse> response = new List<SensorBulkResponse>();

            foreach (var sensorID in SensorBulkAssing.SensorIDs)
            {
                response.Add(new SensorBulkResponse() { SensorID = sensorID, Result = await AssignSensor(sensorID, SensorBulkAssing.NetworkID) });
            }

            return response;
        }
             
        /// <summary>
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>true/false</returns>
        public async Task<string> RemoveSensor(long SensorID)
        {
            return await _sensorClient.RemoveSensor(SensorID);
        }

        /// <summary>
        /// Remove the sensors to the given network
        /// </summary>
        /// <param name="SensorIDs">List of sensorIDs</param>
        /// <returns>success/failure</returns>
        public async Task<List<SensorBulkResponse>> BulkRemoveSensor(List<long> SensorIDs)
        {
            List<SensorBulkResponse> response = new List<SensorBulkResponse>();
            
            foreach (var sensorID in SensorIDs)
            {
                response.Add(new SensorBulkResponse() { SensorID = sensorID, Result = await RemoveSensor(sensorID) });
            }

            return response;
        }

        /// <summary>
        /// Update the SensorName and the Heartbeat info
        /// </summary>
        /// <param name="UpdateSensor">Update sensor model</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> UpdateSensor(UpdateSensor UpdateSensor)
        {
            var retVal = "";

            retVal = await _sensorClient.SensorSetName(UpdateSensor.SensorID, UpdateSensor.SensorName);
            if(retVal == "Success")
            {
                retVal = await _sensorClient.SensorSetHeartbeat(UpdateSensor.SensorID, UpdateSensor.HeartBeat, UpdateSensor.HeartBeat);
            }

            return retVal;
        }

        /// <summary>
        /// Bulk Update sensor details 
        /// </summary>
        /// <param name="UpdateSensors">List of BulkUpdate model</param>
        /// <returns>List of SensorBulkResponse</returns>
        public async Task<List<SensorBulkResponse>> BulkUpdateSensor(List<UpdateSensor> UpdateSensors)
        {
            List<SensorBulkResponse> response = new List<SensorBulkResponse>();

            foreach (var updateSensor in UpdateSensors)
            {
                response.Add(new SensorBulkResponse() { SensorID = updateSensor.SensorID, Result = await UpdateSensor(updateSensor) });
            } 

            return response;
        }

        /// <summary>
        /// Creates/Updates sensor attribute.
        /// </summary>
        /// <param name="SensorAttribute">Sensor attribute model</param>
        /// <returns>Created/Updated Sensor attribute model</returns>
        public async Task<SensorAttribute> UpdateSensorAttribute(SensorAttribute SensorAttribute)
        {
            return await _sensorClient.SensorAttributeSet(SensorAttribute);
        }

        #endregion


        #region PRIVATE MEMBERS     

        /// <summary>
        /// Get the CorF attribute value for the given sensor
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>Value for the CorF attribute</returns>
        private async Task<string> GetCorkForSensor(long SensorID)
        {
            var retVal = "F";

            var attributes = await _sensorClient.SensorAttributes(SensorID);

            if(attributes.Any(x=>x.Name == "CorF"))
            {
                retVal = attributes.Where(x => x.Name == "CorF").Select(x => x.Value).FirstOrDefault();
            }

            return retVal;            
        }

        #endregion


    }
}
