using AssetManagementBlazor.Models;
using System.Net;
using System.Text.Json;

namespace AssetManagementBlazor.Service
{
    public class MachineService:IMachineService
    {
        private  HttpClient _httpClient { get; set; }
        private readonly Serilog.ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MachineService(HttpClient httpClient, Serilog.ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ApiResponse<List<string>>> GetMachines(string?assetName,string?assetVersion,bool latestAssets)
        {
            _logger.Information("GetMachines called with assetName={AssetName}, assetVersion={AssetVersion}, latestAssets={Latest}", assetName, assetVersion, latestAssets);
            try
            {
                var correlationId = _httpContextAccessor.HttpContext?.Request.Headers["X-Correlation-ID"].ToString();
                if (!string.IsNullOrEmpty(correlationId))
                {
                    _logger.Debug("Forwarding call with CorrelationId={CorrelationId}", correlationId);
                }

                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7130/api/v1/machines?assetName={assetName}&assetVersion={assetVersion}&&latestAssets={latestAssets}");

                _logger.Information("Remote API returned status {StatusCode} for GetMachines", response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<string> machine = JsonSerializer.Deserialize<List<string>>(result);
                    _logger.Information("Deserialized {Count} machines from API response", machine?.Count ?? 0);
                    ApiResponse<List<string>> apiResponse = new ApiResponse<List<string>>(machine);
                    return apiResponse;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.Warning("GetMachines returned 404 NotFound");
                    return new ApiResponse<List<string>>("Machines not found");
                }
                else
                {
                    _logger.Error("GetMachines received unexpected status code {StatusCode}", response.StatusCode);
                    return new ApiResponse<List<string>>($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException error)
            {
                _logger.Error(error, "Network error while calling GetMachines");
                return new ApiResponse<List<string>>($"Network Error: {error.Message}");
            }
            catch (Exception error)
            {
                _logger.Error(error, "Unexpected error in GetMachines");
                return new ApiResponse<List<string>>($"Unexpected Error: {error.Message}");
            }
        }


        public async Task<ApiResponse<MachineModel>>GetMachine(string machineName)
        {
            _logger.Information("GetMachine called for {MachineName}", machineName);
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7130/api/v1/machines/{machineName}");
                _logger.Information("Remote API returned status {StatusCode} for GetMachine {MachineName}", response.StatusCode, machineName);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    MachineModel machine = JsonSerializer.Deserialize<MachineModel>(result);
                    _logger.Information("Deserialized machine {MachineName} from API response", machineName);
                    return new ApiResponse<MachineModel>(machine); ;
                }
                else if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.Warning("GetMachine {MachineName} returned NotFound", machineName);
                    return new ApiResponse<MachineModel>("Machine Not Found");
                }
                else
                {
                    _logger.Error("GetMachine {MachineName} received status {StatusCode}", machineName, response.StatusCode);
                    return new ApiResponse<MachineModel>($"Error: {response.StatusCode}");
                }
            }
            catch(HttpRequestException error) {
                _logger.Error(error, "Network error while calling GetMachine {MachineName}", machineName);
                return new ApiResponse<MachineModel>($"Network Error: {error.Message}");
            }
            catch (Exception error)
            {
                _logger.Error(error, "Unexpected error in GetMachine {MachineName}", machineName);
                return new ApiResponse<MachineModel>($"Unexpected Error: {error.Message}");
            }
        }


        public async Task<List<string>>GetMachineAssets(string machineName)
        {
            _logger.Information("GetMachineAssets called for {MachineName}", machineName);
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<string>>($"https://localhost:7130/api/v1/machines/{machineName}/assets");
                _logger.Information("GetMachineAssets returned {Count} assets for {MachineName}", result?.Count ?? 0, machineName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while fetching machine assets for {MachineName}", machineName);
                throw;
            }
        }
    }
}
