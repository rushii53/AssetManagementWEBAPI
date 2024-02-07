using AssetManagementWEBAPI.Models;
using Microsoft.Extensions.Options;

namespace AssetManagementWEBAPI.Repository
{
    public class MachineTextFileRepository:IMachineRepository
    {
        private readonly List<MachineModel> _machines;
        public MachineTextFileRepository(IOptions<TextFileModel>options)
        {
            List<GenericAssetData> genericAssetDatas = new List<GenericAssetData>();

            using (var reader = new StreamReader(options.Value.Path_1))
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
            _machines = genericAssetDatas.GroupBy(d => d.MachineName).Select(o => new MachineModel
            {
                MachineName = o.Key,
                Asset = o.Select(l => new AssetModel(l.AssetName, l.AssetVersion)).ToList()
            }).ToList();
        }
        public List<MachineModel> GetAllMachines()
        {
            return _machines;
        }
    }
}
