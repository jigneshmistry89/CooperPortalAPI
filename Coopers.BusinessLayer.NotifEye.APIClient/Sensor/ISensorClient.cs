﻿using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface ISensorClient
    {

        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <param name="ApplicationID">Integer (optional)	Filters list to sensor that are this application type</param>
        /// <param name="Status">Integer (optional)	Filters list to sensor that match this status</param>
        /// <param name="Name">String (optional)	Filters list to sensors with names containing this string. (case-insensitive)</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        Task<object> GetSensorList(string UserName, string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0);

        /// <summary>
        /// Returns the list of sensors that belongs to currently loggedin user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        Task<List<SensorDetail>> GetSensorListByNetworkID(long NetworkID, string UserName = "");
        

        /// <summary>
        /// Returns the sensor detials.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorDetail</returns>
        Task<SensorDetail> GetSensorDetailsByID(long SensorID);

        /// <summary>
        /// Returns the sensor object with extended Properties.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorExtendedDetail model</returns>
        Task<SensorExtendedDetail> GetSensorExtendedDetailsByID(long SensorID);

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 7 day window)
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="FromDate">	Start of range from which messages will be returned</param>
        /// <param name="ToDate">End of range from which messages will be returned</param>
        /// <returns>List of DataMessagesDTO</returns>
        Task<List<DataMessages>> GetSensorDataMessages(long SensorID, string FromDate, string ToDate);

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 1 day window)
        /// </summary>
        /// <param name="SensorID">	Unique identifier of the sensor</param>
        /// <param name="Minutes">Number of minutes past messages will be returned</param>
        /// <param name="LastMessageID">(optional)	Only return messages received after this message ID</param>
        /// <returns>List of datamessages</returns>
        Task<List<DataMessages>> GetSensorRecentDataMessages(long SensorID, int Minutes, long LastMessageID = 0);

        /// <summary>
        /// Assigns sensor to the specified network
        /// </summary>
        /// <param name="SensorID">Identifier of sensor to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <param name="CheckDigit">Check digit to prevent unauthorized movement of sensors</param>
        /// <returns>true/false</returns>
        Task<string> AssignSensor(long SensorID, long NetworkID, string CheckDigit);

        /// <summary>
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>true/false</returns>
        Task<string> RemoveSensor(long SensorID);

        /// <summary>
        /// Sets the display name of the sensor
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="SensorName">Name to give the sensor</param>
        /// <returns></returns>
        Task<string> SensorSetName(long SensorID, string SensorName);

        /// <summary>
        /// Sets the heartbeat intervals of the sensor
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="ReportInterval">Standard state heart beat</param>
        /// <param name="ActiveStateInterval">Aware state heart beat</param>
        /// <returns></returns>
        Task<string> SensorSetHeartbeat(long SensorID, double ReportInterval, double ActiveStateInterval);

        /// <summary>
        /// Creates/Updates sensor attribute.
        /// </summary>
        /// <param name="SensorAttribute">Sensor attribute model</param>
        /// <returns>Created/Updated Sensor attribute model</returns>
        Task<Model.DTO.SensorAttribute> SensorAttributeSet(Model.DTO.SensorAttribute SensorAttribute);

        /// <summary>
        /// Returns the list of attributes that belong to a sensor.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>List of sensor Attributes</returns>
        Task<List<Model.DTO.SensorAttribute>> SensorAttributes(long SensorID);
    }
}
