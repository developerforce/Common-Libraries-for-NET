using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Common
{
    interface IAuthenticationClient
    {
        string InstanceUrl { get; set; }
        string AccessToken { get; set; }
        string ApiVersion { get; set; }
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent);
        Task UsernamePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl);
    }
}
