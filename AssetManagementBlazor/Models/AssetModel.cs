using System.Text.Json.Serialization;

namespace AssetManagementBlazor.Models
{
    public class AssetModel
    {
        [JsonPropertyName("assetName")]
        public string AssetName { get; set; }

        [JsonPropertyName("assetVersion")]
        public string AssetVersion { get; set; }
    }
}
