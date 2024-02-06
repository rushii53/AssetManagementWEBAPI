using AssetManagementWEBAPI.DataContext;
using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService
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
            List<string> MachineLists = new List<string>();
            if (MachinesList.ContainsKey(assetName))
            {
                MachineLists = MachinesList[assetName].Select(
                       machine => machine).ToList();
            }
            return MachineLists;
        }

        public List<MachineModel> GetMachinesWithLatestAssets()
        {
            List<MachineModel> machines = _machineRepository.GetAllMachines();
            List<MachineModel> result = new List<MachineModel>();

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

            foreach (MachineModel machine in machines)
            {
                bool isAllNewVersions = true;
                foreach (AssetModel asset in machine.Asset)
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
                    result.Add(machine);
                }
            }
            return result;
        }
    }
}
