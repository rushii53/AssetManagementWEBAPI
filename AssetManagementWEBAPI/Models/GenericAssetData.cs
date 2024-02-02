namespace AssetManagementWEBAPI.Models
{
    public class GenericAssetData
    {
        public string MachineName { get; set; } 
        public string AssetName { get; set; }
        public string AssetVersion { get; set; }


        public GenericAssetData(string MachineName,string AssetName,string AssetVersion) {
            this.AssetName = AssetName;
            this.AssetVersion = AssetVersion;
            this.MachineName = MachineName;
        }
    }
}
