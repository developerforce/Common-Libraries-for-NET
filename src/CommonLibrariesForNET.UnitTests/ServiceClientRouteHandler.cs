using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Salesforce.Common.UnitTests
{
    internal class ServiceClientRouteHandler : DelegatingHandler
    {
        Action<HttpRequestMessage> _testingAction;

        public ServiceClientRouteHandler(Action<HttpRequestMessage> testingAction)
        {
            _testingAction = testingAction;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            _testingAction(request);
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new JsonContent(new
                {
                    node = new JsonContent(new
                    {
                        Success = true,
                        Message = "Success"
                    })
                })
            };

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(resp);
            return tsc.Task;
        }
    }
}