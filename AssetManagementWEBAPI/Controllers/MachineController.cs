using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("/api/machine/")]
    public class MachineController:ControllerBase
    {
        private readonly IMachineService _machineService;
        public MachineController(IMachineService MachineService)
        {
            _machineService = MachineService;
        }

        [HttpGet]
        public ActionResult<List<MachineModel>> GetAllMachines() {

            try
            {
                var result = _machineService.GetAllMachines();
                if (result.Count()==0)
                {
                    return Ok("Does not contain any machine");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

        }

        [HttpGet("{MachineName}")]
        public ActionResult<MachineModel> GetMachine(string MachineName)
        {
            try
            {
                var result = _machineService.GetMachineByMachineName(MachineName);
                if (result == null)
                    return Ok("Machine not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

        }

        [HttpGet("asset/{assetName}")]
        public ActionResult<List<string>> GetMachineNames(string assetName)
        {
            try
            {
                var result = _machineService.GetMachineNamesUsingThisAsset(assetName);
                if( result.Count() == 0)
                {
                    return Ok($"Asset: {assetName} not found");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Internal Server Error");
            }
            
        }

        [HttpGet("/machines/latest-assets")]
        public ActionResult<List<MachineModel>> GetMachinesUsingAllLatestAssetVersions()
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

        }

    }
}
