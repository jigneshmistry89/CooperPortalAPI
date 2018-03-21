using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface ISensorApplicationService
    {

        /// <summary>
        /// Returns the list of sensors that belongs to user.
        /// </summary>
        /// <param name="NetworkID">Integer (optional)	Filters list to sensor that belong to this network id</param>
        /// <param name="ApplicationID">Integer (optional)	Filters list to sensor that are this application type</param>
        /// <param name="Status">Integer (optional)	Filters list to sensor that match this status</param>
        /// <param name="Name">String (optional)	Filters list to sensors with names containing this string. (case-insensitive)</param>
        /// <returns>Returns the list of sensors that belongs to user.</returns>
        Task<object> GetSensorList(string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0);

        /// <summary>
        /// Returns the sensor detials.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>SensorDetail</returns>
        Task<Model.DTO.SensorDetail> GetSensorDetailsByID(long ID);

        /// <summary>
        /// Returns data points recorded in a range of time (limited to a 7 day window)
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <param name="FromDate">	Start of range from which messages will be returned</param>
        /// <param name="ToDate">End of range from which messages will be returned</param>
        /// <returns>List of DataMessagesDTO</returns>
        Task<List<DataMessages>> GetSensorDataMessagesByID(long ID, string FromDate, string ToDate);


        /// <summary>
        /// Assigns sensor to the specified network
        /// </summary>
        /// <param name="SensorID">Identifier of sensor to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>true/false</returns>
        Task<string> AssignSensor(long SensorID, long NetworkID);

        /// <summary>
        /// Create a Sensor
        /// </summary>
        /// <param name="CreateSensor">Create sensor model</param>
        /// <returns>Success/Failure</returns>
        Task<string> CreateSensor(Model.DTO.CreateSensor Sensor);

        /// <summary>
        /// Assign the sensors to the given network
        /// </summary>
        /// <param name="SensorBulkAssing">Sensor bulk Assing model</param>
        /// <returns>Success/Failure</returns>
        Task<List<SensorBulkResponse>> BulkAssignSensor(Model.DTO.SensorBulkAssign SensorBulkAssing);

        /// <summary>
        /// Removes the sensor object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the sensor</param>
        /// <returns>true/false</returns>
        Task<string> RemoveSensor(long SensorID);

        /// <summary>
        /// Remove the sensors to the given network
        /// </summary>
        /// <param name="SensorIDs">List of sensorIDs</param>
        /// <returns>success/failure</returns>
        Task<List<SensorBulkResponse>> BulkRemoveSensor(List<long> SensorIDs);

        /// <summary>
        /// Update the SensorName and the Heartbeat info
        /// </summary>
        /// <param name="UpdateSensor">Update sensor model</param>
        /// <returns>Success/Failure</returns>
        Task<string> UpdateSensor(UpdateSensor UpdateSensor);

        /// <summary>
        /// Bulk Update sensor details 
        /// </summary>
        /// <param name="UpdateSensors">List of BulkUpdate model</param>
        /// <returns>List of SensorBulkResponse</returns>
        Task<List<SensorBulkResponse>> BulkUpdateSensor(List<UpdateSensor> UpdateSensors);

        /// <summary>
        /// Creates/Updates sensor attribute.
        /// </summary>
        /// <param name="SensorAttribute">Sensor attribute model</param>
        /// <returns>Created/Updated Sensor attribute model</returns>
        Task<SensorAttribute> UpdateSensorAttribute(SensorAttribute SensorAttribute);

        /// <summary>
        /// Get the note for a given SensorID
        /// </summary>
        /// <param name="SensorID">Unique identofier for the Sensor</param>
        /// <returns>Note value</returns>
        Task<string> GetSensorNote(long SensorID);

        /// <summary>
        /// Update the Note for a given SensorID
        /// </summary>
        /// <param name="SensorNote">SensorNote update model</param>
        /// <returns>Success/Failure</returns>
        Task<string> SetSensorNote(UpdateSensorNote SensorNote);
    }
}
