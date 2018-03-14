using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Coopers.BusinessLayer.API.Filters
{
    /// <summary>
    /// Model Validator
    /// </summary>
    public class ModelValidationAction : ActionFilterAttribute
    {

        /// <summary>
        /// Occurs before the action method is invoked.
        /// Chech the Model Validation errrors
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                actionContext.Response = actionContext.Request
                     .CreateErrorResponse(HttpStatusCode.BadRequest, PrepareModelValidationError(actionContext));
            }
            else
            {
                //Do Nothing..
            }
        }


        /// <summary>
        /// Prepare the model errors
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <returns>error string</returns>
        private string PrepareModelValidationError(HttpActionContext actionContext)
        {
            var errors = "";
            foreach (var state in actionContext.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    errors += error.ErrorMessage + " " ;
                }
            }
            
            return errors.Trim(' ');
        }

    }
}