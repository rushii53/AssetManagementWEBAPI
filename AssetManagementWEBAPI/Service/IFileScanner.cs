using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public interface IFileScanner
    {
        public Task ScanAndParseFile();
    }
}
