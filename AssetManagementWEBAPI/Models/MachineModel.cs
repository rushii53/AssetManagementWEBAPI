using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AssetManagementWEBAPI.Models
{
    public class MachineModel
    {
        public string? MachineId { get; set; }
        public string? MachineName { get; set; }
        public List<AssetModel>? Asset { get; set; }

        public MachineModel(string machineId,string machineName) {
            MachineId = machineId;
            MachineName = machineName;
            Asset = new List<AssetModel>();
        }
        public MachineModel(string machineId,string? machineName, List<AssetModel>? asset) : this(machineId,machineName)
        {
            Asset = asset;
        }

        public MachineModel() { }
    }
}
