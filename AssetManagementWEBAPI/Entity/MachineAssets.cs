using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Entity
{
    public class MachineAssets
    {
        public string MachineName {  get; set; }
        public List<Asset>Assets { get; set; }
    }
}
