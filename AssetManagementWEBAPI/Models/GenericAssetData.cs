namespace AssetManagementWEBAPI.Models
{
    public class GenericAssetData
    {
        public string MachineName { get; set; }
        public string AssetVersion { get; set; }
        public string AssetName { get; set; }

        public GenericAssetData(string machineName, string assetName, string assetVersion)
        {
            MachineName = machineName;
            AssetVersion = assetVersion;
            AssetName = assetName;
        }
    }
}
