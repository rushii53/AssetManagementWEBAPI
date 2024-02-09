using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("/api/machines/")]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineService;
        public MachineController(IMachineService MachineService)
        {
            _machineService = MachineService;
        }

        [HttpGet]
        public ActionResult<List<Machine>> GetMachines([FromQuery] string? assetName, [FromQuery] string? assetVersion, [FromQuery] bool? latestAssets)
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

        [HttpGet("latest-assets")]
        public ActionResult<List<Machine>> GetMachinesUsingAllLatestAssetVersions()
        {
            try
            {
                var result = _machineService.GetMachinesWithLatestAssets();
                if(result.Count()==0)
                {
                    return Ok("Machines with all the latest version of assets not found");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        /*[HttpPost("save")]
        public ActionResult SaveMachine(Machine machine)
        {
            try
            {
                var result = _machineService.SaveMachine(machine);
                if (result)
                {
                    return Created();
                   
                }
                else
                {
                    return Conflict("Machine already exist!");
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("edit/{machineName}")]
        public ActionResult EditMachine(Machine machine)
        {
            try
            {
                var result = _machineService.EditMachine(machine);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
