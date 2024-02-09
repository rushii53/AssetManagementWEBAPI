using AssetManagementWEBAPI.Entity;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public IMongoCollection<Machine> GetMongoCollection();
        public List<string?> GetMachines();
        public List<string?> GetMachinesByAssetName(string assetName);
        public List<string?> GetMachines(string asssetName, string assetVersion);
        public Machine GetMachine(string machineName);
        public List<string> GetMachineAssets(string machineName);
        public List<string> GetMachinesByAsset(string assetsName);
        /*public bool SaveMachine(Machine machine);
        public bool EditMachine(Machine machine);*/
    }
}
