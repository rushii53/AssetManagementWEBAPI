using AssetManagementWEBAPI.Entity;
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
                    return Ok(new List<MachineModel>());
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpGet("{machineName}")]
        public ActionResult<MachineModel> GetMachine(string machineName)
        {
            try
            {
                var result = _machineService.GetMachineByMachineName(machineName);
                if (result == null)
                    return Ok($"Machine: {machineName} not found");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("asset/{assetName}")]
        public ActionResult<List<MachineModel>> GetMachineNames(string assetName)
        {
            try
            {
                var result = _machineService.GetMachineNamesUsingThisAsset(assetName);
                if( result.Count() == 0)
                {
                    return Ok($"There is no machine which uses {assetName} asset");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("latest-assets")]
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
