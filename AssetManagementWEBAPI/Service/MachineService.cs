
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService:IMachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        public List<MachineModel> GetAllMachines()
        {
            return _machineRepository.GetAllMachines();
        }
        public MachineModel GetMachineByMachineName(string machineName)
        {
            return _machineRepository.GetMachine(machineName);
        }
        public List<string> GetMachineNamesUsingThisAsset(string assetName)
        {
            List<MachineModel> machines = _machineRepository.GetAllMachines();

            List<string>result = new List<string>();

            foreach (var machine in machines)
            {
                foreach(var asset in machine.Asset)
                {
                    if (asset.AssetName == assetName)
                        result.Add(machine.MachineName);
                }
            }
            return result;
        }

        public List<MachineModel> GetMachinesWithLatestAssets()
        {
            List<MachineModel> machines = _machineRepository.GetAllMachines();
            List<MachineModel> result = new List<MachineModel>();

            //Creating dictionary which will store the latest version of respective assets
            Dictionary<string, string> AssetsLatestVersionsDictionary = new Dictionary<string, string>();

            foreach (var machine in machines)
            {
                foreach (var asset in machine.Asset)
                {
                    if (AssetsLatestVersionsDictionary.ContainsKey(asset.AssetName))
                    {
                        string assetVersion = AssetsLatestVersionsDictionary[asset.AssetName];
                        int version = int.Parse(assetVersion.Trim().Substring(1));

                        int currentVersion = int.Parse(asset.AssetVersion.Substring(1));

                        if (currentVersion > version)
                            AssetsLatestVersionsDictionary[asset.AssetName] = asset.AssetVersion;
                    }
                    else
                    {
                        AssetsLatestVersionsDictionary[asset.AssetName] = asset.AssetVersion;
                    }
                }
            }

            //Traversing on each machine, which uses latest version of all the assets
            foreach (var machine in machines)
            {
                bool isAllNewVersions = true;
                foreach (var asset in machine.Asset)
                {
                    string? latestAssetVersion = AssetsLatestVersionsDictionary.GetValueOrDefault(asset.AssetName);
                    if (latestAssetVersion != asset.AssetVersion)
                    {
                        isAllNewVersions = false;
                        break;
                    }
                }
                if (isAllNewVersions)
                {
                    //adding the machine to result which uses all the latest versions of assets
                    result.Add(machine);
                }
            }
            return result;
        }
    }
}
