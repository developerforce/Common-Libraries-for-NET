using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Salesforce.Common.Models;
using Newtonsoft.Json;

namespace Salesforce.Common
{
    public class AuthenticationClient : IAuthenticationClient, IDisposable
    {
        public string InstanceUrl { get; set; }
        public string AccessToken { get; set; }
        public string ApiVersion { get; set; }
        private const string UserAgent = "common-libraries-dotnet";
        private const string TokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";
        private static HttpClient _httpClient;

        public AuthenticationClient()
        {
            ApiVersion = "v29.0";
        }

        public AuthenticationClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();

            await UsernamePassword(clientId, clientSecret, username, password, UserAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();

            await UsernamePassword(clientId, clientSecret, username, password, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password, HttpClient httpClient)
        {
            _httpClient = httpClient;
            await UsernamePassword(clientId, clientSecret, username, password, UserAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, HttpClient httpClient)
        {
            _httpClient = httpClient;
            await UsernamePassword(clientId, clientSecret, username, password, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();
            await UsernamePassword(clientId, clientSecret, username, password, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl, HttpClient _httpClient)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(userAgent, "/", ApiVersion));

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

            var responseMessage = await _httpClient.SendAsync(request);
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

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient(); 
        
            await WebServer(clientId, clientSecret, redirectUri, code, UserAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient();
         
            await WebServer(clientId, clientSecret, redirectUri, code, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, string tokenRequestEndpointUrl)
        {
            if (_httpClient == null)
                _httpClient = new HttpClient(); 
         
            await WebServer(clientId, clientSecret, redirectUri, code, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code, HttpClient httpClient)
        {
            _httpClient = httpClient;
            await WebServer(clientId, clientSecret, redirectUri, code, UserAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, HttpClient httpClient)
        {
            _httpClient = httpClient;
            await WebServer(clientId, clientSecret, redirectUri, code, userAgent, TokenRequestEndpointUrl, _httpClient);
        }

        public async Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, string tokenRequestEndpointUrl, HttpClient httpClient)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(userAgent, "/", ApiVersion));

            var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("code", code)
                });

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(tokenRequestEndpointUrl),
                Content = content
            };

            var responseMessage = await _httpClient.SendAsync(request);
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

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
