using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public interface IMachineService
    {
        List<string?> GetMachines(string? assetName,string? assetVersion,bool? latestAssets);
        Machine GetMachine(string machineName);
        List<string?> GetMachineAssets(string machineName);
        List<string?> GetMachinesByAsset(string assetName);
        List<Machine> GetMachinesWithLatestAssets();
      /*  bool SaveMachine(Machine machine);
        bool EditMachine(Machine machine);*/
    }
}
