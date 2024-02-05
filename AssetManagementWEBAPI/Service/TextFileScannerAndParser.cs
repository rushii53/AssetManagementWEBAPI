using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetManagementWEBAPI.Service
{
    public class TextFileScannerAndParser : IFileScanner
    {
        private readonly string _filePath;

        public TextFileScannerAndParser(string filePath)
        {
            _filePath = filePath;
        }
        public void ScanAndParseFile()
        {
            List<GenericAssetData> genericAssetDatas = new List<GenericAssetData>();
           
            using(var reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = await reader.ReadLinec())!=null)
                {
                    string[] data = line.Split(',');

                    string machineName = data[0].Trim();
                    string assetName = data[1].Trim();
                    string assetVersion = data[2].Trim();

                    genericAssetDatas.Add(new GenericAssetData(machineName, assetName, assetVersion));
                }
            }

            //Saving machines app constants data
            List<Machine> machines = genericAssetDatas.GroupBy(d => d.MachineName).Select(o => new Machine
            {
                MachineName = o.Key,
                Asset = o.Select(l => new Asset
                {
                    AssetName = l.AssetName,
                    AssetVersion = l.AssetVersion
                }).ToList()
            }).ToList();

            GlobalAppConstants.AppConstants.Machines = machines;

            //Saving assets information in app constansts dictionary data
            Dictionary<string,List<string>>MachineLists = new Dictionary<string,List<string>>();

            foreach(var machine in machines)
            {
                foreach(var asset in machine.Asset)
                {
                    if(MachineLists.ContainsKey(asset.AssetName))
                    {
                        MachineLists.GetValueOrDefault(asset.AssetName).Add(machine.MachineName);
                    }
                    else
                    {
                        MachineLists[asset.AssetName] = new List<string>();
                        MachineLists[asset.AssetName].Add(machine.MachineName);
                    }
                }
            }
            GlobalAppConstants.AppConstants.MachinesList = MachineLists;

            //Saving latest versions of assets
            Dictionary<string, string> AssetsLatestVersionsDictionary = new Dictionary<string, string>();
            foreach (var machine in machines)
            {
                foreach (var asset in machine.Asset)
                {
                    if (AssetsLatestVersionsDictionary.ContainsKey(asset.AssetName))
                    {
                        string assetVersion = AssetsLatestVersionsDictionary[asset.AssetName];
                        int version = int.Parse(assetVersion.Substring(1));

                        int currentVersion = int.Parse(asset.AssetVersion.Substring(1));

                        if(currentVersion>version)
                            AssetsLatestVersionsDictionary[asset.AssetName]=asset.AssetVersion;
                    }
                    else
                    {
                        AssetsLatestVersionsDictionary[asset.AssetName] = asset.AssetVersion;
                    }
                }
            }
            GlobalAppConstants.AppConstants.AssetsLatestVersionsDictionary = AssetsLatestVersionsDictionary;
        }
    }

}
