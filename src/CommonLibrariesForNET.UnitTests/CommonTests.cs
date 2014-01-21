using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using NUnit.Framework;

namespace Salesforce.Common.UnitTests
{
    public class CommonTests
    {
        [Test]
        public async void Auth_CheckHttpRequestMessage_HttpGet()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.AreEqual(r.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(r.Headers.UserAgent);
                Assert.AreEqual(r.Headers.UserAgent.ToString(), "common-libraries-dotnet/v29");

                Assert.IsNotNull(r.Headers.Authorization);
                Assert.AreEqual(r.Headers.Authorization.ToString(), "Bearer accessToken");
            }));

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);

            await httpClient.HttpGet<object>("wade");
        }

        [Test]
        public async void Auth_CheckHttpRequestMessage_HttpGet_WithNode()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.AreEqual(r.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(r.Headers.UserAgent);
                Assert.AreEqual(r.Headers.UserAgent.ToString(), "common-libraries-dotnet/v29");

                Assert.IsNotNull(r.Headers.Authorization);
                Assert.AreEqual(r.Headers.Authorization.ToString(), "Bearer accessToken");
            }));

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);
            await httpClient.HttpGet<object>("wade", "node");
        }

        [Test]
        public async void Auth_CheckHttpRequestMessage_HttpPost()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.AreEqual(r.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(r.Headers.UserAgent);
                Assert.AreEqual(r.Headers.UserAgent.ToString(), "common-libraries-dotnet/v29");

                Assert.IsNotNull(r.Headers.Authorization);
                Assert.AreEqual(r.Headers.Authorization.ToString(), "Bearer accessToken");
            }));

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);
            await httpClient.HttpPost<object>(null, "wade");
        }

        [Test]
        public async void Auth_CheckHttpRequestMessage_HttpPatch()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.AreEqual(r.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(r.Headers.UserAgent);
                Assert.AreEqual(r.Headers.UserAgent.ToString(), "common-libraries-dotnet/v29");

                Assert.IsNotNull(r.Headers.Authorization);
                Assert.AreEqual(r.Headers.Authorization.ToString(), "Bearer accessToken");
            }));

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);
            await httpClient.HttpPatch(null, "wade");
        }

        [Test]
        public async void Auth_CheckHttpRequestMessage_HttpDelete()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.AreEqual(r.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(r.Headers.UserAgent);
                Assert.AreEqual(r.Headers.UserAgent.ToString(), "common-libraries-dotnet/v29");

                Assert.IsNotNull(r.Headers.Authorization);
                Assert.AreEqual(r.Headers.Authorization.ToString(), "Bearer accessToken");
            }));

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);
            await httpClient.HttpDelete("wade");
        }

        [Test]
        public async void Requests_CheckAddedRequestsHeaders()
        {
            var client = new HttpClient(new TestingRouteHandler(r =>
            {
                Assert.IsNotNull(r.Headers.GetValues("headername"));
                Assert.AreEqual(r.Headers.GetValues("headername").FirstOrDefault(), "headervalue");
            }));

            client.DefaultRequestHeaders.Add("headername","headervalue");

            var httpClient = new ServiceHttpClient("http://localhost:1899", "v29", "accessToken", client);
            await httpClient.HttpGet<object>("wade", "node");
        }
    }
}
