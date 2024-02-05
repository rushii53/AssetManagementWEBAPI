using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public List<Machine> GetAllMachines();  
        public Machine GetMachine(string machineName);
        List<string> GetMachineNames(string assetName);
        List<Machine> GetMachinesWithLatestAssets();
    }
}
