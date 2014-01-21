using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Salesforce.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Salesforce.Common
{
    public class ServiceHttpClient : IServiceHttpClient, IDisposable
    {
        private readonly string _instanceUrl;
        private readonly string _apiVersion;
        private static string _userAgent = "common-libraries-dotnet";
        private readonly string _accessToken;
        private static HttpClient _httpClient;

        public ServiceHttpClient(string instanceUrl, string apiVersion, string accessToken)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;

            _httpClient = new HttpClient();
        }

        public ServiceHttpClient(string instanceUrl, string apiVersion, string accessToken, HttpClient httpClient)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
            _httpClient = httpClient;
        }

        public ServiceHttpClient(string instanceUrl, string apiVersion, string accessToken, string userAgent)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
            _userAgent = userAgent;

            _httpClient = new HttpClient();
        }

        public ServiceHttpClient(string instanceUrl, string apiVersion, string accessToken, string userAgent, HttpClient httpClient)
        {
            _instanceUrl = instanceUrl;
            _apiVersion = apiVersion;
            _accessToken = accessToken;
            _userAgent = userAgent;
            _httpClient = httpClient;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<T> HttpGet<T>(string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/", _apiVersion));

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var responseMessage = await _httpClient.SendAsync(request);
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

        public async Task<T> HttpGet<T>(string urlSuffix, string nodeName)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/", _apiVersion));

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", "Bearer " + _accessToken);

            var responseMessage = await _httpClient.SendAsync(request);
            var response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var jObject = JObject.Parse(response);
                var jToken = jObject.GetValue(nodeName);

                var r = JsonConvert.DeserializeObject<T>(jToken.ToString());
                return r;
            }

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
            throw new ForceException(errorResponse.errorCode, errorResponse.message);
        }

        public async Task<T> HttpPost<T>(object inputObject, string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/", _apiVersion));

            var json = JsonConvert.SerializeObject(inputObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync(url, content);
            var response = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                var r = JsonConvert.DeserializeObject<T>(response);
                return r;
            }

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
            throw new ForceException(errorResponse.message, errorResponse.errorCode);
        }

        public async Task<bool> HttpPatch(object inputObject, string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/", _apiVersion));

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = new HttpMethod("PATCH")
            };

            var json = JsonConvert.SerializeObject(inputObject);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            var response = await responseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
            throw new ForceException(errorResponse.error, errorResponse.error_description);
        }

        public async Task<bool> HttpDelete(string urlSuffix)
        {
            var url = Common.FormatUrl(urlSuffix, _instanceUrl, _apiVersion);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(string.Concat(_userAgent, "/", _apiVersion));

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Delete
            };

            var responseMessage = await _httpClient.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }

            var response = await responseMessage.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response);
            throw new ForceException(errorResponse.error, errorResponse.error_description);
        }
    }
}
