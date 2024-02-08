using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Service
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineModel>> GetAllMachines();
        Task<IEnumerable<MachineModel>> GetMachinesUsesLatestAssets();
        Task<IEnumerable<MachineModel>> GetMachineByMachineName(string machineName);
        Task<IEnumerable<MachineModel>> GetMachineByAssetName(string assetName);
    }
}
