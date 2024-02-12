using AssetManagementBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

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
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7130/api/v1/machines?assetName={assetName}&assetVersion={assetVersion}&&latestAssets={latestAssets}");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<string> machine = JsonSerializer.Deserialize<List<string>>(result);
                    return machine;
                }
                else
                {
                    return new List<string>();
                }
            }catch(Exception ex)
            {
                return new List<string>();
            }

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
