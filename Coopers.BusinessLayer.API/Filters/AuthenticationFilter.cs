using Coopers.BusinessLayer.Localizer;
using Coopers.BusinessLayer.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Coopers.BusinessLayer.API.Filters
{
    /// <summary>
    /// Autheitcation Filter
    /// </summary>
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using System.Web.Http.Filters.AuthorizationFilterAttribute.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            //actionContext.an
            var token = FetchFromHeader(actionContext);

            if (token != null)
            {
                //get authentocation service
                var authenticationAppService = actionContext.ControllerContext.Configuration.DependencyResolver.
                                                GetService(typeof(IAuthenticationApplicationService)) as IAuthenticationApplicationService;
              
                //Check if user is valid or not
                if (!await authenticationAppService.IsValidToken(token))
                {
                    //If user is not valid then return 401 response.
                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    response.Content = new StringContent(AppLocalizer.ERR_SESSION_EXPIRED);
                    actionContext.Response = response;
                    return;
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return ;
            }

            base.OnAuthorization(actionContext);
        }

        #region PRIVATE METHODS

        /// <summary>
        /// retrive header detail from the request 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                requestToken = authRequest.Parameter;

            return requestToken;
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
         
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        #endregion

    }

}