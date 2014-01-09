using System.Threading.Tasks;

namespace Salesforce.Common
{
    public interface IServiceHttpClient
    {
        Task<T> HttpGet<T>(string urlSuffix);
        Task<T> HttpGet<T>(string urlSuffix, string nodeName);
        Task<T> HttpPost<T>(object inputObject, string urlSuffix);
        Task<bool> HttpPatch(object inputObject, string urlSuffix);
        Task<bool> HttpDelete(string urlSuffix);
    }
}