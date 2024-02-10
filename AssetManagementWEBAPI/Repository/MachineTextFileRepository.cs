using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public class MachineTextFileRepository:IMachineRepository
    {
        private readonly List<Machine> _machines;
        public MachineTextFileRepository(IOptions<TextFileModel> options)
        {
            _machines = this.ScanAndParse(options);
        }

        public Machine GetMachine(string machineName)
        {
            Machine machine = _machines.Where(m =>m.MachineName.ToLower() == machineName.ToLower())
                .FirstOrDefault();
            return machine;
        }

        public List<Asset> GetMachineAssets(string machineName)
        {
            Machine machine = _machines.Where(m=>m.MachineName.ToLower() == machineName.ToLower())
                .FirstOrDefault();
            return machine != null ? machine.Asset : null;
        }

        public List<string?> GetMachines()
        {
            return _machines.Select(m=>m.MachineName).ToList();
        }

        public List<string?> GetMachinesByAssetName(string assetName)
        {
            List<string?> machines = _machines.Where(a => a.Asset
                .Any(a => a.AssetName.ToLower() == assetName.ToLower()))
                .Select(m=>m.MachineName).ToList();
            return machines;
        }

        public List<string?> GetMachinesByAssetNameAndAssetVersion(string assetName, string assetVersion)
        {
            List<string?> machines = _machines.Where(m => m.Asset
                .Any(a => a.AssetName.ToLower() == assetName.ToLower()
                && a.AssetVersion.ToLower() == assetVersion.ToLower()))
                .Select(m => m.MachineName).ToList();

            return machines;
        }

        public List<string> GetMachinesByAssetVersion(string assetVersion)
        {
            List<string> machines = _machines.Where(m=>m.Asset
                .Any(a=>a.AssetVersion.ToLower()==assetVersion.ToLower()))
                .Select(m=>m.MachineName) .ToList();

            return machines;
        }

        public List<string> GetMachinesWithLatestAssets()
        {
            var latestAssets = _machines.SelectMany(m=>m.Asset)
                .GroupBy(a=>a.AssetName)
                .Select(group => new
                {
                    Name = group.Key,
                    LatestVersion = group.Max(a=>a.AssetVersion)
                })
                .ToList();

            var machinesList = _machines.Where(m => m.Asset.All(
                        asset => latestAssets.Any(
                            latestAsset => latestAsset.Name == asset.AssetName && latestAsset.LatestVersion == asset.AssetVersion
                            )))
                .Select(machine => machine.MachineName).ToList();

            return machinesList;
        }

        public List<Machine> ScanAndParse(IOptions<TextFileModel> options)
        {
            List<GenericAssetData> genericAssetDatas = new List<GenericAssetData>();

            using (var reader = new StreamReader(options.Value.Path_2))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    string machineName = data[0].Trim();
                    string assetName = data[1].Trim();
                    string assetVersion = data[2].Trim();

                    genericAssetDatas.Add(new GenericAssetData(machineName, assetName, assetVersion));
                }
            }
            //Saving machines app constants data
            List<Machine>Machines = genericAssetDatas.GroupBy(d => d.MachineName).Select(o => new Machine
            {
                MachineName = o.Key,
                Asset = o.Select(l => new Asset(l.AssetName, l.AssetVersion)).ToList()
            }).ToList();

            return Machines;
        }


    }
}
