using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public interface IMachineService
    {
        List<string?> GetMachines(string? assetName,string? assetVersion,bool latestAssets);
        Machine GetMachine(string machineName);
        List<Asset> GetMachineAssets(string machineName);
    }
}
