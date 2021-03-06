﻿using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface IGatewayClient
    {

        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="Name">Filters list to gateways with names containing this string. (case-insensitive)</param>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <param name="ApplicationID">Filters list to gateway that are this application type</param>
        /// <param name="Status">Filters list to gateway that match this status</param>
        /// <returns>gateway list</returns>
        Task<List<GatewayDTO>> GetGatewayList(string UserName, string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0);


        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <returns>List of GatewayDTO</returns>
        Task<List<GatewayDTO>> GetGatewayListByNetworkID(long NetworkID,string UserName = "");


        /// <summary>
        /// Returns the gateway object.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>Gateway Details</returns>
        Task<GatewayDTO> GatewayGet(long GatewayID);


        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>Success/Failure</returns>
        Task<string> AssignGateway(long GatewayID, long NetworkID);

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the gateway</param>
        /// <returns>Success/Failure</returns>
        Task<string> RemoveGateway(long GatewayID);

        /// <summary>
        /// Create a gateway
        /// </summary>
        /// <param name="Gateway">Gateway model</param>
        /// <returns>if created 1 else 0  </returns>
        Task<int> CreateGateway(Gateway Gateway);

        /// <summary>
        /// Update the Gateway
        /// </summary>
        /// <param name="UpdateGateway">Gateway Model</param>
        /// <returns>No of records updated</returns>
        Task<int> UpdateGateway(UpdateGateway UpdateGateway);
    }
}
