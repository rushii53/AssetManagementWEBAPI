using AssetManagementBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Net;
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


        public async Task<ApiResponse<List<string>>> GetMachines(string?assetName,string?assetVersion,bool latestAssets)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7130/api/v1/machines?assetName={assetName}&assetVersion={assetVersion}&&latestAssets={latestAssets}");

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<string> machine = JsonSerializer.Deserialize<List<string>>(result);
                    ApiResponse<List<string>> apiResponse = new ApiResponse<List<string>>(machine);
                    return apiResponse;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new ApiResponse<List<string>>("Machines not found");
                }
                else
                {
                    return new ApiResponse<List<string>>($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException error)
            {
                return new ApiResponse<List<string>>($"Network Error: {error.Message}");
            }
            catch (Exception error)
            {
                return new ApiResponse<List<string>>($"Unexpected Error: {error.Message}");
            }
        }


        public async Task<ApiResponse<MachineModel>>GetMachine(string machineName)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7130/api/v1/machines/{machineName}");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MachineModel machine = JsonSerializer.Deserialize<MachineModel>(result);
                    return new ApiResponse<MachineModel>(machine); ;
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new ApiResponse<MachineModel>("Machine Not Found");
                }
                else
                {
                    return new ApiResponse<MachineModel>($"Error: {response.StatusCode}");
                }
            }
            catch(HttpRequestException error) {
                return new ApiResponse<MachineModel>($"Network Error: {error.Message}");
            }
            catch (Exception error)
            {
                return new ApiResponse<MachineModel>($"Unexpected Error: {error.Message}");
            }
        }


        public async Task<List<string>>GetMachineAssets(string machineName)
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"https://localhost:7130/api/v1/machines/{machineName}/assets");
        }
    }
}
