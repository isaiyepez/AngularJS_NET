using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace EIS.API
{
    public class EISExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is DivideByZeroException)
            {
                HttpResponseMessage msgResp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msgResp.ReasonPhrase = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response = msgResp;
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                HttpResponseMessage msgResp = new HttpResponseMessage(HttpStatusCode.BadGateway);
                msgResp.ReasonPhrase = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response = msgResp;
            }
            else
            {
                HttpResponseMessage msgResp = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                msgResp.ReasonPhrase = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response = msgResp;
            }
            base.OnException(actionExecutedContext);
        }
    }
}