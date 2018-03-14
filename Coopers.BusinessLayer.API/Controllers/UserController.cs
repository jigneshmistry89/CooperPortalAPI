using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// User Endpoint
    /// </summary>
    [RoutePrefix("api/User")]
    [AuthenticationFilter]
    public class UserController : ApiController
    {

        #region PRIVATE MEMBERS

        private IUserApplicationService _userApplicationService;

        #endregion


        #region CONSTRUCTOR

        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs

        /// <summary>
        /// Get the currently loggeg in User info. 
        /// </summary>
        /// <returns>User Model</returns>
        [HttpGet]
        [Route("Info")]
        [ResponseType(typeof(UserWithAccountInfo))]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            return Ok(await _userApplicationService.GetUserInfo());
        }

        /// <summary>
        /// Get the currently loggeg in User's details. 
        /// Returns the Users List and payment info if the current user is Admin
        /// </summary>
        /// <returns>User Detail model</returns>
        [HttpGet]
        [Route("Details")]
        [ResponseType(typeof(UserDetailWithPaymentHistory))]
        public async Task<IHttpActionResult> GetUserDetails()
        {
            return Ok(await _userApplicationService.GetUserDetails());
        }

        [HttpGet]
        [Route("{UserID}/Info")]
        [ResponseType(typeof(UserWithAccountInfo))]
        public async Task<IHttpActionResult> GetUserInfoByID(long UserID)
        {
            return Ok(await _userApplicationService.GetUserInfoByID(UserID));
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("SendRegisterationLink")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> SendRegisterationLink([FromUri]string Email)
        {
            return Ok(await _userApplicationService.SendRegisterationLink(Email));
        }


        #endregion


        #region PUT | APIs

        /// <summary>
        /// Delete the user by the ID
        /// </summary>
        /// <param name="UserID">Unique identifier for the user</param>
        /// <returns>Success/Failure</returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> DeleteUser(long UserID)
        {
            return Ok(await _userApplicationService.DeleteUser(UserID));
        }

        #endregion


        #region DELETE | APIs

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>success/failure</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> UpdateUser(UpdateUser User)
        {
            return Ok(await _userApplicationService.UpdateUser(User));
        }

        #endregion


        #region POST | APIs

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterNewUser")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> RegisterNewUser(UserNewRegistration User)
        {
            return Ok(await _userApplicationService.RegisterNewUser(User));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterNotifEyeUser")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> RegisterNotifEyeUser(UserNotifEyeRegistration User)
        {
            return Ok(await _userApplicationService.RegisterNotifEyeUser(User));
        }


        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>unique indetifier for the created user</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> CreateUser(UserCreate User)
        {
            return Ok(await _userApplicationService.CreateUser(User));
        }

        #endregion


        #endregion


    }
}
