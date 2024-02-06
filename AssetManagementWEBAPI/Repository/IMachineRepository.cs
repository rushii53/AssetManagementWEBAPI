using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Repository
{
    public interface IMachineRepository
    {
        public List<MachineModel> GetAllMachines();  
    }
}
