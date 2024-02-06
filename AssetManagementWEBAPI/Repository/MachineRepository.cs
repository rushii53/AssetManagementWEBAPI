using AssetManagementWEBAPI.DataContext;
using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Repository
{
    public class  MachineRepository: IMachineRepository
    {
       
        private readonly IFileScanner _fileScanner;
        public MachineRepository(IFileScanner fileScanner)
        {
            _fileScanner = fileScanner;
            fileScanner.ScanAndParseFile();
        }
        
        public List<MachineModel> GetAllMachines()
        {
            return GlobalAppConstants.AppConstants.Machines;
        }

        public MachineModel GetMachine(string machineName)
        {
            return GlobalAppConstants.AppConstants.Machines.Where(machine=>machine.MachineName==machineName).FirstOrDefault();
        }
    }
}













































































/*private readonly MongoDbAssetManagement _mongoDbAssetManagement;
       public MachineRepository(MongoDbAssetManagement mongoDbAssetManagement,)
       {
           _mongoDbAssetManagement = mongoDbAssetManagement;
       }*/
/*public async Task<List<Machine>> GetAllMachines()
{
    List<Machine> result= (await _mongoDbAssetManagement._machineCollection.FindAsync(_=>true)).ToList();
    return result;
}
public async Task<Machine> GetMachine(string machineName)
{
    return await _mongoDbAssetManagement._machineCollection.Find(_=>_.MachineName == machineName).FirstAsync();
}*/