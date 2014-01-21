using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Common
{
    interface IAuthenticationClient
    {
        string InstanceUrl { get; set; }
        string AccessToken { get; set; }
        string ApiVersion { get; set; }
        Task UsernamePassword(string clientId, string clientSecret, string username, string password);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, HttpClient httpClient);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, HttpClient httpClient);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl, HttpClient _httpClient);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, string tokenRequestEndpointUrl);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code, HttpClient httpClient);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, HttpClient httpClient);
        Task WebServer(string clientId, string clientSecret, string redirectUri, string code, string userAgent, string tokenRequestEndpointUrl, HttpClient httpClient);
        void Dispose();
    }
}
