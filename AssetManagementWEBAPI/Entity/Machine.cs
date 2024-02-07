using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetManagementWEBAPI.Entity
{
    [BsonIgnoreExtraElements]
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("machineName")]
        public string? MachineName { get; set; }
        [BsonElement("asset")]
        public List<Asset>? Asset { get; set; }

        public Machine(string machineName)
        {
            MachineName = machineName;
            Asset = new List<Asset>();
        }
    }
}
