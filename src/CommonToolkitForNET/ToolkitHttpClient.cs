using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CommonToolkitForNET.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CommonToolkitForNET
{
    public class ToolkitHttpClient
    {
        private readonly string _instanceUrl;
        private readonly string _apiVersion;
        private readonly string _userAgent;
        private readonly string _accessToken;
        public ToolkitHttpClient(string instanceUrl, string apiVersion, string accessToken)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
            _userAgent = "common-toolkit-dotnet";

        }

        public ToolkitHttpClient(string instanceUrl, string apiVersion, string accessToken, string userAgent)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
            _userAgent = userAgent;
        }

        public async Task<T> HttpGet<T>(string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/" , _apiVersion));

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                request.Headers.Add("Authorization", "Bearer " + _accessToken);

                var responseMessage = await client.SendAsync(request);
                var response = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jObject = JObject.Parse(response);

                    var r = JsonConvert.DeserializeObject<T>(jObject.ToString());
                    return r;
                }

                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                throw new ForceException(errorResponse.errorCode, errorResponse.message);
            }
        }

        public async Task<T> HttpPost<T>(object inputObject, string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, _apiVersion));

                var json = JsonConvert.SerializeObject(inputObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync(url, content);
                var response = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var r = JsonConvert.DeserializeObject<T>(response);
                    return r;
                }

                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
                throw new ForceException(errorResponse.message, errorResponse.errorCode);
            }
        }
    }
}
