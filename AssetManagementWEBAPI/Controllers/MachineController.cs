using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("/api/machine/")]
    public class MachineController:ControllerBase
    {
        private readonly MachineService _machineService;

        public MachineController(MachineService MachineService)
        {
            _machineService = MachineService;
        }

        [HttpGet]
        public List<Machine> GetAllMachine()
        {
            return _machineService.GetAllMachines();
        }

        [HttpGet("{MachineName}")]
        public Machine GetMachine(string MachineName)
        {
            return _machineService.GetMachineByMachineName(MachineName);
        }

        [HttpGet("asset/{assetName}")]
        public List<string> GetMachineNames(string assetName) {
            return _machineService.GetMachineNamesUsingThisAsset(assetName);
        }

        [HttpGet("machines/")]
        public List<Machine> GetMachinesUsingAllLatestAssetVersions()
        {
            return _machineService.GetMachinesWithLatestAssets();
        }

    }
}
