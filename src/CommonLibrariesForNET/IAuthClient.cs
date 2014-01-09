using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesforce.Common
{
    interface IAuthClient
    {
        string InstanceUrl { get; set; }
        string AccessToken { get; set; }
        string ApiVersion { get; set; }
        Task AuthenticatePassword(string clientId, string clientSecret, string username, string password, string userAgent);
        Task AuthenticatePassword(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl);
    }
}
