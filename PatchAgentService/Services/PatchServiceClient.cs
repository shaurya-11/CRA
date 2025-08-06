using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PatchAgentService.Models;

namespace PatchAgentService.Services
{
    public class PatchServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PatchServiceClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["ApiBaseUrl"]!;
        }

        public async Task<List<AgentQueryDto>> GetLatestPatchStatusesAsync(int customerId)
        {
            var url = $"{_baseUrl}/query/latest-per-product/{customerId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AgentQueryDto>>(json)!;
        }

        public async Task UpdateStatusAsync(int customerId, int patchId, int status)
        {
            var url = $"{_baseUrl}/patchstatus/update-status?customerId={customerId}&patchId={patchId}";
            var payload = JsonConvert.SerializeObject(new StatusUpdateDto { Status = status });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
