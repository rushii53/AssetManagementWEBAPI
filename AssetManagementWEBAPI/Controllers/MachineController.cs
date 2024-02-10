using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/machines/")]
    public class MachineControllerV1 : ControllerBase
    {
        private readonly IMachineService _machineService;
        public MachineControllerV1(IMachineService MachineService)
        {
            _machineService = MachineService;
        }

        [HttpGet]
        public ActionResult<List<string>> GetMachines([FromQuery] string? assetName, [FromQuery] string? assetVersion, [FromQuery] bool latestAssets)
        {
            try
            {
                var result = _machineService.GetMachines(assetName,assetVersion,latestAssets);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Machines not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet("{machineName}")]
        public ActionResult<Machine> GetMachine(string machineName)
        {
            try
            {
                var result = _machineService.GetMachine(machineName);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Machine not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpGet("{machineName}/assets")]
        public ActionResult<List<string>>GetMachineAssets(string machineName)
        {
            try
            {
                var result = _machineService.GetMachineAssets(machineName);
                if( result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Machine not found");
                }
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
