using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public List<MachineModel> GetAllMachines();
        public IMongoCollection<Machine> GetMongoCollection();
        public Task<bool> SaveMachine(Machine machine);
        public Task<bool> EditMachine(Machine machine);
    }
}
