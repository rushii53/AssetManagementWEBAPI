using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("/api/machine/")]
    public class MachineController:ControllerBase
    {
        private readonly IMachineRepository _machineRepository;

        public MachineController(IMachineRepository machineRepository)
        {
            _machineRepository = machineRepository;
        }

        [HttpGet]
        public List<Machine> GetAllMachine()
        {
            return _machineRepository.GetAllMachines();
        }

        [HttpGet("{MachineName}")]
        public Machine GetMachine(string MachineName)
        {
            return _machineRepository.GetMachine(MachineName);
        }

        [HttpPost]
        public void SaveMachine(Machine machine)
        {
            return;
        }
    }
}
