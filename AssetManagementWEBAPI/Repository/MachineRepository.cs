using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;

namespace AssetManagementWEBAPI.Repository
{
    public class  MachineRepository: IMachineRepository
    {
        private readonly IFileScanner _fileScanner;
        public MachineRepository(IFileScanner fileScanner)
        {
            _fileScanner = fileScanner;
            fileScanner.ScanAndParseFile();
        }
        public List<Machine> GetAllMachines()
        {
            return GlobalAppConstants.AppConstants.Machines;
        }
        public Machine GetMachine(string machineName)
        {
            return GlobalAppConstants.AppConstants.Machines.Where(data => data.MachineName == machineName).FirstOrDefault();
        }
        public List<string> GetMachineNamesUsingThisAsset(string assetName)
        {
            List<Machine> machines = GlobalAppConstants.AppConstants.Machines;

            Dictionary<string, List<string>> MachinesList = new Dictionary<string, List<string>>();

            foreach (var machine in machines)
            {
                foreach (var asset in machine.Asset)
                {
                    if (!MachinesList.ContainsKey(asset.AssetName))
                    {
                        MachinesList[asset.AssetName] = new List<string>();
                    }
                    MachinesList[asset.AssetName].Add(machine.MachineName);
                }
            }

            if (MachinesList.ContainsKey(assetName))
            {
                List<string>MachineLists = MachinesList[assetName].Select(
                        machine=>machine).ToList();
                return MachineLists;
            }
            return null;
        }

        public List<Machine> GetMachinesWithLatestAssets()
        {
            List<Machine> machines = GlobalAppConstants.AppConstants.Machines;
            List<Machine> result = new List<Machine>();

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

            foreach (Machine machine in machines)
            {
                bool isAllNewVersions = true;
                foreach(Asset asset in machine.Asset)
                {
                    string latestAssetVersion = AssetsLatestVersionsDictionary.GetValueOrDefault(asset.AssetName);
                    if(latestAssetVersion != asset.AssetVersion)
                    {
                        isAllNewVersions = false;
                        break;
                    }
                }
                if(isAllNewVersions)
                {
                    result.Add(machine);
                }
            }
            return result;
        }
    }
}
