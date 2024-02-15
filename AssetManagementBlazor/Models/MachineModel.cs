using System.Text.Json.Serialization;

namespace AssetManagementBlazor.Models
{
    public class MachineModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("machineName")]
        public string MachineName { get; set; }

        [JsonPropertyName("asset")]
        public List<AssetModel> Asset {  get; set; }

     
    }
}
