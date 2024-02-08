
using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService:IMachineService
    {
        private readonly List<MachineModel>?_machines;
        private readonly IMachineRepository _machineRepository;
        public MachineService(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
            _machines = machineRepository.GetAllMachines();
        }

        public List<MachineModel> GetAllMachines()
        {
            return _machines;
        }
        public MachineModel GetMachineByMachineName(string machineName)
        {
            return _machines.Where(m => m.MachineName.ToLower() == machineName.ToLower()).First();
        }
        public List<MachineModel> GetMachineNamesUsingThisAsset(string assetName)
        {

            List<MachineModel>result = new List<MachineModel>();

            foreach (var machine in _machines)
            {
                foreach(var asset in machine.Asset)
                {
                    if (asset.AssetName.ToLower() == assetName.ToLower())
                        result.Add(machine);
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
        public bool SaveMachine(Machine machine)
        {
            var exstingMachine = _machineRepository.GetAllMachines().Where(m=>m.MachineName==machine.MachineName).FirstOrDefault();
            if(exstingMachine != null)
            {
                return false;
            }
            return _machineRepository.SaveMachine(machine).Result;
        }
        public bool EditMachine(Machine machine)
        {
            var existingMachine = _machineRepository.GetMongoCollection().Find(m=>m.MachineName == machine.MachineName).FirstOrDefault();
            if(existingMachine != null)
            {
                if(machine.MachineName != existingMachine.MachineName)
                {
                    existingMachine.MachineName = machine.MachineName;
                }
                return _machineRepository.EditMachine(existingMachine).Result;
            }
            return false;
        }
    }
}
