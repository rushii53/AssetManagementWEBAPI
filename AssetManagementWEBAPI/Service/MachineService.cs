
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService:IMachineService
    {
        private readonly List<MachineModel>?_machines;

        public MachineService(IMachineRepository machineRepository)
        {
            _machines = machineRepository.GetAllMachines();
        }

        public List<MachineModel> GetAllMachines()
        {
            return _machines;
        }
        public MachineModel GetMachineByMachineName(string machineName)
        {
            return _machines.Where(m => m.MachineName == machineName).First();
        }
        public List<string> GetMachineNamesUsingThisAsset(string assetName)
        {

            List<string>result = new List<string>();

            foreach (var machine in _machines)
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
            List<MachineModel> result = new List<MachineModel>();

            //Creating dictionary which will store the latest version of respective assets
            Dictionary<string, string> AssetsLatestVersionsDictionary = new Dictionary<string, string>();

            foreach (var machine in _machines)
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
            foreach (var machine in _machines)
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
