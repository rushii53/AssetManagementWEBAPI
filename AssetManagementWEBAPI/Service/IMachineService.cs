using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public interface IMachineService
    {
        List<MachineModel> GetAllMachines();
        MachineModel GetMachineByMachineName(string machineName);
        List<MachineModel> GetMachineNamesUsingThisAsset(string assetName);
        List<MachineModel> GetMachinesWithLatestAssets();
    }
}
