using AssetManagementWEBAPI.Entity;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public List<string?> GetMachines();
        public List<string?> GetMachinesByAssetName(string assetName);
        public List<string> GetMachinesByAssetVersion(string assetVersion);
        public List<string?> GetMachinesByAssetNameAndAssetVersion(string asssetName, string assetVersion);
        public Machine GetMachine(string machineName);
        public List<Asset> GetMachineAssets(string machineName);
        public List<string> GetMachinesWithLatestAssets();
    }
}
