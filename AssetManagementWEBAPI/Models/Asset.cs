namespace AssetManagementWEBAPI.Models
{
    public class Asset
    {
        public string AssetName { get; set; }   
        public string AssetVersion { get; set; }    

        public Asset(string assetName, string assetVersion)
        {
            AssetName = assetName;
            AssetVersion = assetVersion;
        }
    }
}
