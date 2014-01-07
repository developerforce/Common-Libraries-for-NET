using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.SelfHost;
using NUnit.Framework;


namespace CommonToolkitForNET.FunctionalTests
{
    public class AuthClientTests
    {
        private static string _tokenRequestEndpointUrl = ConfigurationSettings.AppSettings["TokenRequestEndpointUrl"];
        private static string _securityToken = ConfigurationSettings.AppSettings["SecurityToken"];
        private static string _consumerKey = ConfigurationSettings.AppSettings["ConsumerKey"];
        private static string _consumerSecret = ConfigurationSettings.AppSettings["ConsumerSecret"];
        private static string _username = ConfigurationSettings.AppSettings["Username"];
        private static string _password = ConfigurationSettings.AppSettings["Password"] + _securityToken;

        [Test]
        public async void Auth_ValidCreds_HasApiVersion()
        {
            var auth = new AuthClient();
            Assert.IsNotNullOrEmpty(auth.ApiVersion);
        }

        [Test]
        public async void Auth_ValidCreds_HasAccessToken()
        {
            const string userAgent = "common-toolkit-dotnet";

            var auth = new AuthClient();
            await auth.Authenticate(_consumerKey, _consumerSecret, _username, _password, userAgent, _tokenRequestEndpointUrl);

            Assert.IsNotNullOrEmpty(auth.AccessToken);
        }

        [Test]
        public async void Auth_ValidCreds_HasInstanceUrl()
        {
            const string userAgent = "common-toolkit-dotnet";

            var auth = new AuthClient();
            await auth.Authenticate(_consumerKey, _consumerSecret, _username, _password, userAgent, _tokenRequestEndpointUrl);

            Assert.IsNotNullOrEmpty(auth.InstanceUrl);
        }

        [Test]
        public async void Auth_CheckHttpRequestMessage()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:1899/services/data/v29/wade");
            config.HostNameComparisonMode = HostNameComparisonMode.Exact; // doesn't require admin privileges
            config.Routes.MapHttpRoute("default", "services/data/v29/wade");

            //var httpConfig = new HttpConfiguration();
            //httpConfig.Routes.MapHttpRoute(name: "Deafult", routeTemplate: "services/data/v29/wade");



            var server = new HttpSelfHostServer(config);
            var task = server.OpenAsync();
            task.Wait();

            Func<HttpClient> builder = () => new HttpClient();

            var toolkitHttpClient = new ToolkitHttpClient("http://localhost:1899", "v29", "accessToken", builder);
            try
            {
                await toolkitHttpClient.HttpGet<object>("wade");

            }
            catch
            {
                Assert.AreEqual(toolkitHttpClient.HttpRequestMessage.RequestUri.ToString(), "http://localhost:1899/services/data/v29/wade");

                Assert.IsNotNull(toolkitHttpClient.HttpRequestMessage.Headers.UserAgent);
                Assert.AreEqual(toolkitHttpClient.HttpRequestMessage.Headers.UserAgent.ToString(), "common-toolkit-dotnet/v29");
                
                Assert.IsNotNull(toolkitHttpClient.HttpRequestMessage.Headers.Authorization);
                Assert.AreEqual(toolkitHttpClient.HttpRequestMessage.Headers.Authorization.ToString(), "Bearer accessToken");
            }
        }
    }
}
