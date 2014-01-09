using System;
using System.Net.Http;
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

            Func<HttpClient> builder = () => client;

            var httpClient = new SalesforceHttpClient("http://localhost:1899", "v29", "accessToken", builder);

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

            Func<HttpClient> builder = () => client;

            var httpClient = new SalesforceHttpClient("http://localhost:1899", "v29", "accessToken", builder);
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

            Func<HttpClient> builder = () => client;

            var httpClient = new SalesforceHttpClient("http://localhost:1899", "v29", "accessToken", builder);
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

            Func<HttpClient> builder = () => client;

            var httpClient = new SalesforceHttpClient("http://localhost:1899", "v29", "accessToken", builder);
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

            Func<HttpClient> builder = () => client;

            var httpClient = new SalesforceHttpClient("http://localhost:1899", "v29", "accessToken", builder);
            await httpClient.HttpDelete("wade");
        }
    }
}
