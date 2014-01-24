using System;
using System.Configuration;
using NUnit.Framework;
using Salesforce.Common;

namespace Salesforce.Common.FunctionalTests
{
    public class CommonTests
    {
        private static string _tokenRequestEndpointUrl = ConfigurationSettings.AppSettings["TokenRequestEndpointUrl"];
        private static string _securityToken = ConfigurationSettings.AppSettings["SecurityToken"];
        private static string _consumerKey = ConfigurationSettings.AppSettings["ConsumerKey"];
        private static string _consumerSecret = ConfigurationSettings.AppSettings["ConsumerSecret"];
        private static string _username = ConfigurationSettings.AppSettings["Username"];
        private static string _password = ConfigurationSettings.AppSettings["Password"] + _securityToken;


        [Test]
        public async void Auth_UsernamePassword_HasAccessToken()
        {
            const string userAgent = "common-libraries-dotnet";

            var auth = new AuthenticationClient();
            await auth.UsernamePassword(_consumerKey, _consumerSecret, _username, _password, userAgent, _tokenRequestEndpointUrl);

            Assert.IsNotNullOrEmpty(auth.AccessToken);
        }

        [Test]
        public async void Auth_UsernamePassword_HasInstanceUrl()
        {
            const string userAgent = "common-libraries-dotnet";

            var auth = new AuthenticationClient();
            await auth.UsernamePassword(_consumerKey, _consumerSecret, _username, _password, userAgent, _tokenRequestEndpointUrl);

            Assert.IsNotNullOrEmpty(auth.InstanceUrl);
        }

        [Test]
        public async void Auth_InvalidLogin()
        {
            const string userAgent = "common-libraries-dotnet";

            try
            {
                var auth = new AuthenticationClient();
                await auth.UsernamePassword(_consumerKey, _consumerSecret, _username, "WRONGPASSWORD", userAgent, _tokenRequestEndpointUrl);
            }
            catch (ForceException ex)
            {
                Assert.IsNotNull(ex);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.Error);
            }

        }
    }
}
