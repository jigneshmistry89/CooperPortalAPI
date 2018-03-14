using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Token Endpoint
    /// </summary>
    [RoutePrefix("api/Token")]
    [AllowAnonymous]
    public class TokenController : ApiController
    {

        #region PRIVATE MEMBERS

        private IAuthenticationApplicationService _authenticationApplicationService;

        #endregion


        #region CONSTRUCTOR

        public TokenController(IAuthenticationApplicationService authenticationApplicationService)
        {
            _authenticationApplicationService = authenticationApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS  

        /// <summary>
        /// Returns authorization token for username password to be used in all other api methods.
        /// </summary>
        /// <param name="UserName">Username you use to access iMonnit</param>
        /// <param name="Password">Password you use to access iMonnit</param>
        /// <returns>Token</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> GetAuthToken([FromBody]User User)
        {
            return Ok(await _authenticationApplicationService.GetDashboardAuthToken(User.UserName, User.Password));
        }

        #endregion


    }
}
