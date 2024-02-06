using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public List<Machine> GetAllMachines();  
        public Machine GetMachine(string machineName);
        List<string> GetMachineNamesUsingThisAsset(string assetName);
        List<Machine> GetMachinesWithLatestAssets();
    }
}
