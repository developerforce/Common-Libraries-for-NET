using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonToolkitForNET
{
    interface IAuthClient
    {
        string InstanceUrl { get; set; }
        string AccessToken { get; set; }
        string ApiVersion { get; set; }
        Task Authenticate(string clientId, string clientSecret, string username, string password, string userAgent);
        Task Authenticate(string clientId, string clientSecret, string username, string password, string userAgent, string tokenRequestEndpointUrl);
    }
}
