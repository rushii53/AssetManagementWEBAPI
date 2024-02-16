using AssetManagementWEBAPI.Entity;
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
        /// <summary> Gets the list of machine names that are using a specific asset and series number</summary>
        /// <param name="assetName">The name of the asset to search for</param>
        /// <param name="seriesNumber">The series number of the asset to search for</param>
        /// <response code="200">Returns the list of machine names that match the specified asset and series number</response>
        /// <response code="404">The specified asset name or series number does not exist</response>
        /// <response code="500">Internal server error</response>
        /// <returns>A list of machine names that use the specified asset and series number</returns>
        [HttpGet]
        public ActionResult<List<string>> GetMachines([FromQuery] string? assetName, [FromQuery] string? assetVersion, [FromQuery] bool latestAssets)
        {
            try
            {
                var result = _machineService.GetMachines(assetName,assetVersion,latestAssets);

                if (result != null && result.Count!=0)
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

        /// <summary> Gets the machine by machine name</summary>
        /// <param name="machineName">The name of the machine to search</param>
        /// <response code="200">Returns the machine that match the specified machine name</response>
        /// <response code="404">The specified machine name does not exist</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Required machine</returns>
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
                    return NotFound($"Machine:{machineName} not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary> Gets the list of machine assets of specified machine name</summary>
        /// <param name="machineName">The name of the machine to search for</param>
        /// <response code="200">Returns the list of machine assets</response>
        /// <response code="404">Specified machine does not exist</response>
        /// <response code="500">Internal server error</response>
        /// <returns>A list of machine assets</returns>
        [HttpGet("{machineName}/assets")]
        public ActionResult<List<Asset>>GetMachineAssets(string machineName)
        {
            try
            {
                var result = _machineService.GetMachineAssets(machineName);
                if( result != null && result.Count!=0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound($"Machine:{machineName} not found");
                }
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
