using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Service
{
    public interface IMachineService
    {
        Task<IEnumerable<MachineModel>> GetAllMachines();
    }
}
