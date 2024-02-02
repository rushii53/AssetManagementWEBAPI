using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public class JsonFileScanner:IFileScanner
    {
        public Task<List<GenericAssetData>>ParseFile(Stream stream)
        {
            return null;
        }
    }
}
