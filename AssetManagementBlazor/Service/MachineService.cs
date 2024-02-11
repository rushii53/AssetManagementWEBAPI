using AssetManagementBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Service
{
    public class MachineService:IMachineService
    {
        private HttpClient _httpClient { get; set; }
        public MachineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<string>> GetMachines(string?assetName,string?assetVersion,bool latestAssets)
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"https://localhost:7130/api/v1/machines?assetName={assetName}&assetVersion={assetVersion}&&latestAssets={latestAssets}");
        }
        public async Task<MachineModel>GetMachine(string machineName)
        {
            return await _httpClient.GetFromJsonAsync<MachineModel>($"https://localhost:7130/api/v1/machines/{machineName}");
        }
        public async Task<List<string>>GetMachineAssets(string machineName)
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"https://localhost:7130/api/v1/machines/{machineName}/assets");
        }
    }
}
