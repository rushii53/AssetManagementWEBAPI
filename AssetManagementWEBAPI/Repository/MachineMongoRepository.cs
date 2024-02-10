using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public class  MachineMongoRepository: IMachineRepository
    {
        private readonly IMongoCollection<Machine> _machineCollection;
        public MachineMongoRepository(IOptions<DBModel>options)
        {
            var MongoConnection = new MongoClient(options.Value.ConnectionString);
            var MongoDatabase = MongoConnection.GetDatabase(options.Value.DatabaseName);
            _machineCollection = MongoDatabase.GetCollection<Machine>(options.Value.CollectionName);
        }
        public List<string> GetMachines()
        {
            List<string> machines = _machineCollection.Find(_ => true).ToList().Select(m=>m.MachineName).ToList();
            return machines;
        }
        public List<string>GetMachinesByAssetName(string assetName)
        {
            List<string>machines=_machineCollection.AsQueryable()
                .Where(m=>m.Asset.Any(a=>a.AssetName.ToLower()==assetName.ToLower()))
                .Select(m=>m.MachineName) .ToList();
            return machines;
        }

        public List<string>GetMachinesByAssetVersion(string assetVersion)
        {
            List<string> machines = _machineCollection.Find(m => m.Asset
            .Any(a => a.AssetVersion.ToLower() == assetVersion.ToLower())).ToList()
            .Select(m => m.MachineName).ToList();
            return machines;
        }
        public List<string>GetMachinesByAssetNameAndAssetVersion(string? assetName,string?assetVersion)
        {
            List<string> machines = _machineCollection.AsQueryable()
                .Where(m => m.Asset.Any(a => a.AssetName.ToLower() == assetName.ToLower() && a.AssetVersion.ToLower() == assetVersion.ToLower()))
                .Select(m => m.MachineName)
                .ToList();
            return machines;
        }
       
        public List<Asset> GetMachineAssets(string machineName)
        {
            Machine? machine = _machineCollection.AsQueryable()
                                        .Where(machine => machine.MachineName.ToLower() == machineName.ToLower())
                                        .FirstOrDefault();
            List<Asset> result = machine != null ? machine.Asset.ToList() : null;
            return result;
        }

        public Machine GetMachine(string machineName) { 
            Machine result = _machineCollection.Find(_=>_.MachineName.ToLower() == machineName.ToLower()).FirstOrDefault();
            return result;
        }

        public List<string> GetMachinesWithLatestAssets()
        {
            var latestAssets = _machineCollection.AsQueryable()
                .SelectMany(m => m.Asset)
                .GroupBy(a => a.AssetName)
                .Select(group => new
                {
                    Name = group.Key,
                    LatestVersion = group.Max(asset=>asset.AssetVersion)
                }).ToList();

            var machinesList = _machineCollection.AsQueryable()
                .Where(m => m.Asset.All(
                        asset => latestAssets.Any(
                            latestAsset => latestAsset.Name == asset.AssetName && latestAsset.LatestVersion == asset.AssetVersion
                            )))
                .Select(machine=>machine.MachineName).ToList();

            return machinesList;
        }

        /*public bool SaveMachine(Machine machine)
       {
           try
           {
               _machineCollection.InsertOneAsync(machine);
               return true;
           }catch(Exception e)
           {
               return false;
           }
       }*/

        /*public bool EditMachine(Machine machine)
        {
            try { 
                var filter = Builders<Machine>.Filter.Eq(x => x.MachineName, machine.MachineName);
                var update = Builders<Machine>.Update.Set(x => x,machine);
                var updatedResult = _machineCollection.UpdateOne(filter,update);
                return updatedResult.ModifiedCount>0?true:false;
            }catch(Exception e)
            {
                return false;
            }
        }*/
    }
}













































































/*private readonly MongoDbAssetManagement _mongoDbAssetManagement;
       public MachineRepository(MongoDbAssetManagement mongoDbAssetManagement,)
       {
           _mongoDbAssetManagement = mongoDbAssetManagement;
       }*/
/*public async Task<List<Machine>> GetAllMachines()
{
    List<Machine> result= (await _mongoDbAssetManagement._machineCollection.FindAsync(_=>true)).ToList();
    return result;
}
public async Task<Machine> GetMachine(string machineName)
{
    return await _mongoDbAssetManagement._machineCollection.Find(_=>_.MachineName == machineName).FirstAsync();
}*/