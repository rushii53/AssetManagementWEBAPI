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
        public async Task<IEnumerable<MachineModel>> GetMachinesUsesLatestAssets()
        {
            return await _httpClient.GetFromJsonAsync<List<MachineModel>>("api/machine/latest-assets");
        }
        public async Task<IEnumerable<MachineModel>> GetMachineByAssetName(string assetName)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<MachineModel>>($"api/machine/asset/{assetName.Trim()}");
            }catch(Exception ex)
            {
                return new List<MachineModel>();
            }
        }

        public async Task<IEnumerable<MachineModel>> GetMachineByMachineName(string machineName)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<MachineModel>($"api/machine/{machineName.Trim()}");
                List<MachineModel> ans = new List<MachineModel>();
                ans.Add(result);
                return ans;
            }catch(Exception ex)
            {
                return new List<MachineModel>();
            }
        }
    }
}
