using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Service
{
    public interface IMachineService
    {
        public Task<List<string>> GetMachines(string? assetName, string? assetVersion, bool latestAssets);
        public Task<MachineModel> GetMachine(string machineName);
        public Task<List<string>> GetMachineAssets(string machineName);
    }
}
