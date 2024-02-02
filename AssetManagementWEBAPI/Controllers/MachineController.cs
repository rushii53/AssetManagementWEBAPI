using AssetManagementWEBAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    public class MachineController
    {
        [HttpGet]
        public List<Machine> GetAllMachine()
        {
            return new List<Machine>();
        }

        [HttpGet]
        public Machine GetMachine(string MachineName)
        {
            return new Machine();
        }

        [HttpPost]
        public void SaveMachine(Machine machine)
        {
            return;
        }
    }
}
