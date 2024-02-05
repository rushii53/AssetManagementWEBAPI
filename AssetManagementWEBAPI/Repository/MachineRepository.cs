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
        public List<string> GetMachineNames(string assetName)
        {
            if (GlobalAppConstants.AppConstants.MachinesList.ContainsKey(assetName))
            {
                List<string>MachineLists = GlobalAppConstants.AppConstants.MachinesList[assetName].Select(
                        machine=>machine).ToList();
                return MachineLists;
            }
            return null;
        }

        public List<Machine> GetMachinesWithLatestAssets()
        {
            List<Machine> machines = GlobalAppConstants.AppConstants.Machines;
            List<Machine> result = new List<Machine>();
            foreach (Machine machine in machines)
            {
                bool flag = false;
                foreach(Asset asset in machine.Asset)
                {
                    string latestAssetVersion = GlobalAppConstants.AppConstants.AssetsLatestVersionsDictionary.GetValueOrDefault(asset.AssetName);
                    if(latestAssetVersion != asset.AssetVersion)
                    {
                        flag = true;
                        break;
                    }
                }
                if(!flag)
                {
                    result.Add(machine);
                }
            }
            return result;
        }
    }
}
