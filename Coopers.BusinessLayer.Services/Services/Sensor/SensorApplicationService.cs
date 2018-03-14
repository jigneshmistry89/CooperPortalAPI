using AutoMapper;
using Coopers.BusinessLayer.Database.APIClient.Location;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>true/false</returns>
        public async Task<string> RemoveSensor(long SensorID)
        {
            return await _sensorClient.RemoveSensor(SensorID);
        }


        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
