using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace EIS.API
{
    public class EISAPIKeyHandler : DelegatingHandler
    {
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            
            if (request.Headers.Contains("Access-Control-Request-Headers"))
            {
                return base.SendAsync(request, cancellationToken);

            }
            if (request.Headers.Contains("my_Token"))
            {
                var apiKey = request.Headers.GetValues("my_Token").FirstOrDefault();

                if (apiKey == "123456789")
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }
            
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                tsc.SetResult(response);
                return tsc.Task;
           
        }
    }
}