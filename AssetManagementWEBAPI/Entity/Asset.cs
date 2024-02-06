using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace AssetManagementWEBAPI.Entity
{
    public class Asset
    {
        [BsonElement("assetName")]
        public string? AssetName {  get; set ; }
        [BsonElement("assetVersion")]
        public string? AssetVersion { get; set; }

    }
}
