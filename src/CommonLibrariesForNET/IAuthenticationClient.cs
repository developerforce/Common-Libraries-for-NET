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
        Task UsernamePasswordAysnc(string clientId, string clientSecret, string username, string password);
        Task UsernamePasswordAysnc(string clientId, string clientSecret, string username, string password, string userAgent);
        Task UsernamePasswordAysnc(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl);
        Task WebServerAysnc(string clientId, string clientSecret, string redirectUri, string code);
        Task WebServerAysnc(string clientId, string clientSecret, string redirectUri, string code, string userAgent);
        Task WebServerAysnc(string clientId, string clientSecret, string redirectUri, string code, string userAgent, string tokenRequestEndpointUrl);
        void Dispose();
    }
}
