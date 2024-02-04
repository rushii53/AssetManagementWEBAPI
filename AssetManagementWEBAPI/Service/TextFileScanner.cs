using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetManagementWEBAPI.Service
{
    public class TextFileScanner : IFileScanner
    {
        private readonly string _filePath;

        public TextFileScanner(string filePath)
        {
            _filePath = filePath;
        }
        public async Task ScanAndParseFile()
        {
            List<GenericAssetData> genericAssetDatas = new List<GenericAssetData>();
           
            using(var reader = new StreamReader(_filePath))
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

            List<Machine> machines = genericAssetDatas.GroupBy(d => d.MachineName).Select(o => new Machine
            {
                MachineName = o.Key,
                Asset = o.Select(l => new Asset
                {
                    AssetName = l.AssetName,
                    AssetVersion = l.AssetVersion
                }).ToList()
            }).ToList();

            AppConstants.AppConstants.Machines = machines;
            return;
        }
    }

}
