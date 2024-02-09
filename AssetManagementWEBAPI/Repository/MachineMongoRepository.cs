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
        public List<string?> GetMachines()
        {
            List<string?> machines = _machineCollection.Find(_ => true).ToList().Select(m=>m.MachineName).ToList();
            return machines;
        }
        public List<string?>GetMachines(string?assetName)
        {
            List<string?>machines=_machineCollection.AsQueryable()
                .Where(m=>m.Asset.Any(a=>a.AssetName==assetName))
                .Select(m=>m.MachineName) .ToList();
            return machines;
        }
        public List<string?>GetMachines(string? assetName,string?assetVersion)
        {
            List<string?> machines = _machineCollection.AsQueryable()
                .Where(m => m.Asset.Any(a => a.AssetName == assetName && a.AssetVersion == assetVersion))
                .Select(m => m.MachineName)
                .ToList();
            return machines;
        }
       
        public List<string?> GetMachineAssets(string machineName)
        {
            Machine? machine = _machineCollection.AsQueryable()
                                        .Where(machine => machine.MachineName.ToLower() == machineName.ToLower())
                                        .FirstOrDefault();
            List<string?> result = machine != null ? machine.Asset.Select(a =>a.AssetName).ToList() : null;
            return result;
        }
        public List<string> GetMachinesByAsset(string assetName)
        {
            List<string> machines =_machineCollection.AsQueryable().Where(m=>m.Asset.Any(a=>a.AssetName.ToLower() == assetName.ToLower())).Select(m=>m.MachineName).ToList();
           
            return machines.Count>0?machines:null;
        }

        public Machine GetMachine(string machineName) { 
            Machine result = _machineCollection.Find(_=>_.MachineName.ToLower() == machineName.ToLower()).FirstOrDefault();
            return result;
        }

        public IMongoCollection<Machine> GetMongoCollection()
        {
            return _machineCollection; 
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