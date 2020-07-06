using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cryptollet.Common.Network
{
    public interface INetworkService
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult>(string uri, string jsonData);
        Task<TResult> PutAsync<TResult>(string uri, string jsonData);
        Task DeleteAsync(string uri);
    }

    public class NetworkService : INetworkService
    {
        private HttpClient _httpClient;

        public NetworkService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, string jsonData)
        {
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;
        }

        public async Task DeleteAsync(string uri)
        {
            await _httpClient.DeleteAsync(uri);
        }
    }
}
