namespace AssetManagementWEBAPI.Models
{
    public class AssetModel
    {
        public string? AssetName { get; set; }   
        public string? AssetVersion { get; set; }    

        public AssetModel(string assetName, string assetVersion)
        {
            AssetName = assetName;
            AssetVersion = assetVersion;
        }
    }
}
