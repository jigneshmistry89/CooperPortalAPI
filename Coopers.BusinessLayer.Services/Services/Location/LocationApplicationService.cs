using AutoMapper;
using Coopers.BusinessLayer.Database.APIClient.DTO;
using Coopers.BusinessLayer.Database.APIClient.Location;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Model.Interface;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Services.DTO;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class LocationApplicationService : ILocationApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly ILocationClient _locationClient;
        private readonly ISensorClient _sensorClient;
        private readonly IGatewayClient _gatewayClient;
        private readonly IMapper _mapper;
        private readonly INetworkClient _networkClient;
        private readonly INetworkLocationClient _networkLocationClient;
        

        #endregion


        #region CONSTRUCTOR

        public LocationApplicationService(ILocationClient locationClient, IGatewayClient gatewayClient, ISensorClient sensorClient, INetworkClient networkClient, IMapper mapper,
              INetworkLocationClient networkLocationClient)
        {
            _locationClient = locationClient;
            _sensorClient = sensorClient;
            _gatewayClient = gatewayClient;
            _networkClient = networkClient;
            _mapper = mapper;
            _networkLocationClient = networkLocationClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Get the Location Summary for a given Location ID
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationSummaryDTO</returns>
        public async Task<LocationSummary> GetLocationSummaryByID(long ID)
        {
            //get location info
            var loc = (await _locationClient.GetLocationByID(ID));

            //map the location info 
            LocationSummary locaSummary = _mapper.Map<LocationSummary>(loc);

            //get sensor summary info
            locaSummary.SensorSummary = await PrepareLocationSummary(loc);

            //prepare and return location summary
            return locaSummary;

        }


        /// <summary>
        /// Get the location summary for a give user
        /// </summary>
        /// <returns>List of LocationSummaryDTO</returns>
        public async Task<List<UserLocationSummary>> GetUserLocationSummary()
        {
            List<UserLocationSummary> userLocSummaryList = new List<UserLocationSummary>();

            //Get the list of available network for the current user
            List<NotifEye.APIClient.DTO.Network> networks = await _networkClient.GetNetworkList();
            UserLocationSummary userLocaSummary = null;
            foreach (var network in networks)
            {

                //Get the address detais for the network
                var netWorkLocation = await _networkLocationClient.GetNetworkLocationByID(network.NetworkID);
                
                if (netWorkLocation != null)
                {
                    userLocaSummary = _mapper.Map<UserLocationSummary>(netWorkLocation);
                    //prepare the sensor summary from the network
                    var sensorSummary = await PreareSensorSummaryForNetwork(network.NetworkID);
                    if (sensorSummary != null)
                    {
                        userLocaSummary.LowBattery = sensorSummary.LowBattery;
                        userLocaSummary.LowSignal = sensorSummary.LowSignal;
                        userLocaSummary.MissedCommunication = sensorSummary.MissedCommunication;
                        userLocaSummary.Alerts = sensorSummary.Alerts;
                        userLocaSummary.ActiveSensors = sensorSummary.ActiveSensors;
                    }
                }
                else
                {
                    userLocaSummary = _mapper.Map<UserLocationSummary>(network);
                }

                userLocSummaryList.Add(userLocaSummary);
            }

            return userLocSummaryList;
        }

        /// <summary>
        /// Get the Location details.
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationDetailsDTO</returns>
        public async Task<LocationDetails> GetLocationDetails(int ID)
        {
            LocationDetails locDetails = new LocationDetails();

            //Get the Network info from the Integrated API
            var netWork = await  _networkClient.GetNetworkByID(ID);
            
            //If the newtwork found then 
            if (netWork != null)
            {
                //Map the network info.
                locDetails = _mapper.Map<LocationDetails>(netWork);

                //Get the address detais for the network
                var netWorkLocation = await _networkLocationClient.GetNetworkLocationByID(ID);
                
                if (netWorkLocation != null)
                {
                    locDetails = _mapper.Map<LocationDetails>(netWorkLocation);
                }

                //get gayway list for a network
                var gateWayList = await _gatewayClient.GetGatewayListByNetworkID(netWork.CSNetID);
                locDetails.Gateways = _mapper.Map<List<GatewayDetails>>(gateWayList);
                locDetails.NoOfGateways = locDetails.Gateways.Count;

                //get the sensor list for a given network
                var sensorList = await _sensorClient.GetSensorListByNetworkID(netWork.CSNetID);
                locDetails.Sensors = _mapper.Map<List<SensorDetails>>(sensorList);
                locDetails.NoOfSensors = locDetails.Sensors.Count;
            }

            return locDetails;
        }

        #endregion


        #region PRIVATE MEMBERS     

        private async Task<List<SensorSummary>> PrepareLocationSummary(LocationDTO location)
        {
            List<SensorSummary> sensorSummaryList = new List<SensorSummary>();

            //prepare the sensor summary for each network
            foreach (var netWorkID in location.NetworkIDs)
            {
                sensorSummaryList.Add(await PreareSensorSummaryForNetwork(netWorkID));
            }

            //return the location summry
            return sensorSummaryList;
        }

        private async Task<SensorSummary> PreareSensorSummaryForNetwork(long NetWorkID)
        {
            SensorSummary sensorSummary = new SensorSummary();

            //Get Sensors for a network
            var sensorList = await _sensorClient.GetSensorListByNetworkID(NetWorkID);

            //preare the sensor summary
            sensorSummary.LowBattery = sensorList.Count(sensor => sensor.BatteryLevel < 50);
            sensorSummary.LowSignal = sensorList.Count(sensor => sensor.SignalStrength < 100);
            sensorSummary.MissedCommunication = sensorList.Count(sensor => sensor.Status == (int)SensorStatus.MissedCommunication);
            sensorSummary.Alerts = sensorList.Count(sensor => sensor.Status == (int)SensorStatus.Alert);
            sensorSummary.ActiveSensors = sensorList.Count();

            //return thr sensor summary
            return sensorSummary;
        }

        #endregion


    }
}
