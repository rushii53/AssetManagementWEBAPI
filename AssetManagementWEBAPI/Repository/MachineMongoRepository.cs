using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public class  MachineMongoRepository: IMachineRepository
    {
        private readonly IMongoCollection<Machine> _machines;
        private readonly IMapper _mapper;
        public MachineMongoRepository(IMapper mapper,IOptions<DBModel>options)
        {
            var MongoConnection = new MongoClient(options.Value.ConnectionString);
            var MongoDatabase = MongoConnection.GetDatabase(options.Value.DatabaseName);
            _machines = MongoDatabase.GetCollection<Machine>(options.Value.CollectionName);
            _mapper = mapper;
        }
        public List<MachineModel> GetAllMachines()
        {
            List<Machine> machineList = _machines.Find(_ => true).ToList();
            List<MachineModel> result = machineList.Select(m => new MachineModel {MachineId=m.Id, MachineName = m.MachineName, Asset = m.Asset.Select(a=>new AssetModel { AssetName = a.AssetName,AssetVersion = a.AssetVersion}).ToList() }).ToList();
            return result;
        }
        public async Task<bool>SaveMachine(Machine machine)
        {
            try
            {
                await _machines.InsertOneAsync(machine);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool>EditMachine(Machine machine)
        {
            try { 
                var filter = Builders<Machine>.Filter.Eq(x => x.MachineName, machine.MachineName);
                var update = Builders<Machine>.Update.Set(x => x,machine);
                var updatedResult = await _machines.UpdateOneAsync(filter,update);
                return updatedResult.ModifiedCount>0?true:false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public IMongoCollection<Machine> GetMongoCollection()
        {
            return _machines; 
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