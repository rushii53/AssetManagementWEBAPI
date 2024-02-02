using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public interface IFileScanner
    {
        Task<List<GenericAssetData>> ParseFile(Stream stream);
    }
}
