using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AssetManagementWEBAPI.Models
{
    public class MachineModel
    {
       /* [BsonRepresentation(BsonType.ObjectId)]*/
        /*public string Id { get; set; } = string.Empty;*/
       /* [BsonElement("machineName")]*/
        public string? MachineName { get; set; }
        public List<AssetModel>? Asset { get; set; }

        public MachineModel(string machineName) {
            MachineName = machineName;
        }
        public MachineModel(string? machineName, List<AssetModel>? asset) : this(machineName)
        {
            Asset = asset;
        }

        public MachineModel() { }
    }
}
