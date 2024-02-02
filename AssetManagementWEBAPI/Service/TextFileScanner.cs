using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.Service
{
    public class TextFileScanner : IFileScanner
    {
        public async Task<List<GenericAssetData>> ParseFile(Stream fileStream)
        {
            List<GenericAssetData> genericAssetDatas = new List<GenericAssetData>();
           
            using(var reader = new StreamReader(fileStream))
            {
                string line;
                while ((line = await reader.ReadLineAsync())!=null)
                {
                    string[] data = line.Split(',');

                    string machineName = data[0].Trim();
                    string assetName = data[1].Trim();
                    string assetVersion = data[2].Trim();

                    genericAssetDatas.Add(new GenericAssetData(machineName, assetName, assetVersion));
                }
            }
            
            return genericAssetDatas;
        }
    }

}
