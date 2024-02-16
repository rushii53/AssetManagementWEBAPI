
using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Repository;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService:IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public List<string?> GetMachines(string?assetName,string?assetVersion,bool latestAssetsFlag)
        {
            List<string> machines = null;

            if (!string.IsNullOrEmpty(assetName) && !string.IsNullOrEmpty(assetVersion))
                machines = _machineRepository.GetMachinesByAssetNameAndAssetVersion(assetName, assetVersion);
            else if (!string.IsNullOrEmpty(assetName) && !string.IsNullOrWhiteSpace(assetName))
                machines = _machineRepository.GetMachinesByAssetName(assetName);
            else if (!string.IsNullOrEmpty(assetVersion) && !string.IsNullOrWhiteSpace(assetVersion))
                machines = _machineRepository.GetMachinesByAssetVersion(assetVersion);
            else 
                machines = _machineRepository.GetMachines();

            if (latestAssetsFlag)
            {
                var machinesWithLatestAssets = _machineRepository.GetMachinesWithLatestAssets();
                machines = machines.Intersect(machinesWithLatestAssets).ToList();
            }
            return machines;
        }
        public Machine GetMachine(string machineName)
        {
            return _machineRepository.GetMachine(machineName);
        }
        public List<Asset>GetMachineAssets(string machineName)
        {
            return _machineRepository.GetMachineAssets(machineName);
        }
    }
}
