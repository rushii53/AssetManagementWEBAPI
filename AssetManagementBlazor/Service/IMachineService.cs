using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Service
{
    public interface IMachineService
    {
        public Task<ApiResponse<List<string>>> GetMachines(string? assetName, string? assetVersion, bool latestAssets);
        public Task<ApiResponse<MachineModel>> GetMachine(string machineName);
        public Task<List<string>> GetMachineAssets(string machineName);
    }
}
