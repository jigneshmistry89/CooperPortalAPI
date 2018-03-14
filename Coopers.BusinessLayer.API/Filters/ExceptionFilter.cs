using Coopers.BusinessLayer.Model.DTO;
using Elmah;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Coopers.BusinessLayer.API.Filters
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            ErrorSignal.FromCurrentContext().Raise(new Exception(actionExecutedContext.Exception.Message));

            var error = GetError(actionExecutedContext);
            
            if(error !=null)
            {
                //Define the Response Message
                response = new HttpResponseMessage(error.StatusCode)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(error))
                };
            }
            else
            {
                //The Response Message Set by the Action During Ececution
                var res = actionExecutedContext.Exception.Message;

                //Define the Response Message
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(res),
                    ReasonPhrase = res
                };
            }

            //Create the Error Response
            actionExecutedContext.Response = response;

            return Task.FromResult(0);
        }


        #region PRIVATE METHODS

        private ErrorInfo GetError(HttpActionExecutedContext actionExecutedContext)
        {
            ErrorInfo errInfo = null;

            var ex = actionExecutedContext.Exception;

            if(ex.Data["Method"] != null)
            {
                errInfo = new ErrorInfo();
                errInfo.Method = (string)ex.Data["Method"];
            }

            if (ex.Data["Message"] != null)
            {
                errInfo.Message = (string)ex.Data["Message"];
            }

            if (ex.Data["StatusCode"] != null)
            {
                errInfo.StatusCode = (HttpStatusCode)ex.Data["StatusCode"];
            }

            return errInfo;
        }

        #endregion

    }

}