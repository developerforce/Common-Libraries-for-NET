using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonToolkitForNET.Models;
using Newtonsoft.Json;

namespace CommonToolkitForNET
{
    public class AuthClient : IAuthClient
    {
        public string InstanceUrl { get; set; }
        public string AccessToken { get; set; }
        public string ApiVersion { get; set; }
        
        public AuthClient()
        {
            ApiVersion = "v29.0";
        }

        public AuthClient(string clientId, string clientSecret, string username, string password)
            : this()
        {
            Authenticate(clientId, clientSecret, username, password).Wait();
        }

        public async Task Authenticate(string clientId, string clientSecret, string username, string password)
        {
            var tokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";

            await Authenticate(clientId, clientSecret, username, password, tokenRequestEndpointUrl);
        }

        public async Task Authenticate(string clientId, string clientSecret, string username, string password, string tokenRequestEndpointUrl)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(string.Format("salesforce-toolkit-dotnet/{0}", ApiVersion));

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });

                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(tokenRequestEndpointUrl),
                    Content = content
                };

                var responseMessage = await client.SendAsync(request);
                var response = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var authToken = JsonConvert.DeserializeObject<AuthToken>(response);

                    AccessToken = authToken.access_token;
                    InstanceUrl = authToken.instance_url;
                }
                else
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                    throw new ForceException(errorResponse.error, errorResponse.error_description);
                }
            }
        }
    }
}
