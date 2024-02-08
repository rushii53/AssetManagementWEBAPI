using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public class  MachineMongoRepository: IMachineRepository
    {
       private readonly IMongoCollection<Machine> _machines;
        public MachineMongoRepository(IOptions<DBModel>options)
        {
            var MongoConnection = new MongoClient(options.Value.ConnectionString);
            var MongoDatabase = MongoConnection.GetDatabase(options.Value.DatabaseName);
            _machines = MongoDatabase.GetCollection<Machine>(options.Value.CollectionName);
        }
        public List<MachineModel> GetAllMachines()
        {
            List<Machine> machineList = _machines.Find(_ => true).ToList();
            List<MachineModel> result = machineList.Select(m => new MachineModel {MachineId=m.Id, MachineName = m.MachineName, Asset = m.Asset.Select(a=>new AssetModel { AssetName = a.AssetName,AssetVersion = a.AssetVersion}).ToList() }).ToList();
            return result;
        }
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