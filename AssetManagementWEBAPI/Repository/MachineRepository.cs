﻿using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;

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
        public List<Machine> GetAllMachines()
        {
            return AppConstants.AppConstants.Machines;
        }
        public Machine GetMachine(string machineName)
        {
            return AppConstants.AppConstants.Machines.Where(data => data.MachineName == machineName).FirstOrDefault();
        }
    }
}
