using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class GatewayClient : IGatewayClient
    {

        #region PRIVATE MEMBERS

        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public GatewayClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region NOTIFEYEAPI

        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="Name">Filters list to gateways with names containing this string. (case-insensitive)</param>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <param name="ApplicationID">Filters list to gateway that are this application type</param>
        /// <param name="Status">Filters list to gateway that match this status</param>
        /// <returns>gateway list</returns>
        public async Task<List<GatewayDTO>> GetGatewayList(string UserName, string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0)
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
                return await _httpService.GetAsAsync<List<GatewayDTO>>("GatewayList", filter, false);
            }
            else
            {
                return await _httpService.GetWithUserAsync<List<GatewayDTO>>(UserName,"GatewayList", filter, false);
            }
        }


        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <returns>List of GatewayDTO</returns>
        public async Task<List<GatewayDTO>> GetGatewayListByNetworkID(long NetworkID, string UserName = "")
        {
            return await GetGatewayList(UserName,"", NetworkID);
        }


        /// <summary>
        /// Returns the gateway object.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>Gateway Details</returns>
        public async Task<GatewayDTO> GatewayGet(long GatewayID)
        {
            GatewayDTO retVal = null;

            try
            {
                retVal = await _httpService.GetAsAsync<GatewayDTO>("GatewayGet", string.Format("GatewayID={0}", GatewayID), false);
            }
            catch (Exception ex)
            {
                return retVal;
            }

            return retVal;
        }


        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <param name="CheckDigit">Check digit to prevent unauthorized movement of gateways</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> AssignGateway(long GatewayID, long NetworkID, string CheckDigit)
        {
            return await _httpService.GetAsAsync<string>("AssignGateway", string.Format("GatewayID={0}&NetworkID={1}&CheckDigit={2}", GatewayID, NetworkID, CheckDigit), false);
        }


        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the gateway</param>
        /// <returns>true/false</returns>
        public async Task<string> RemoveGateway(long GatewayID)
        {
            return await _httpService.GetAsAsync<string>("RemoveGateway", string.Format("GatewayID={0}", GatewayID), false);
        }

        #endregion


        #region INTEGRATED API

        /// <summary>
        /// Create a gateway
        /// </summary>
        /// <param name="Gateway">Gateway model</param>
        /// <returns>if created 1 else 0  </returns>
        public async Task<int> CreateGateway(Gateway Gateway)
        {
            return await _httpService.PostAsAsync<int>("gateway/CreateGateway", Gateway);
        }

        /// <summary>
        /// Update the Gateway
        /// </summary>
        /// <param name="UpdateGateway">Gateway Model</param>
        /// <returns>No of records updated</returns>
        public async Task<int> UpdateGateway(UpdateGateway UpdateGateway)
        {
            string path = string.Format("gateway/UpdateGateway?NetworkID={0}", UpdateGateway.NetworkID);
            return await _httpService.PutAsAsync<int>(path, UpdateGateway);
        }

        #endregion


        #region PRIVATE MEMBERS    

        #endregion


    }
}
