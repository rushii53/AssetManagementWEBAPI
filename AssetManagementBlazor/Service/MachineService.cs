using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Service
{
    public class MachineService:IMachineService
    {
        private HttpClient _httpClient;
        public MachineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MachineModel>> GetAllMachines()
        {
            return await _httpClient.GetFromJsonAsync<MachineModel[]>("api/machine");
        }
    }
}
