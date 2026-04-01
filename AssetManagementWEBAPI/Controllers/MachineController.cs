using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AssetManagementWEBAPI.Controllers
{

    [ApiController]
    [Route("/api/v1/machines/")]
    public class MachineControllerV1 : ControllerBase
    {
        private readonly IMachineService _machineService;
        private readonly Serilog.ILogger _logger;
        public MachineControllerV1(IMachineService MachineService, Serilog.ILogger logger)
        {
            _machineService = MachineService;
            _logger = logger;
        }
        /// <summary> Gets the list of machine names that are using a specific asset and series number</summary>
        /// <param name="assetName">The name of the asset to search for</param>
        /// <param name="assetVersion">The series number of the asset to search for</param>
        /// <response code="200">Returns the list of machine names that match the specified asset and series number</response>
        /// <response code="404">The specified asset name or series number does not exist</response>
        /// <response code="500">Internal server error</response>
        /// <returns>A list of machine names that use the specified asset and series number</returns>
        [HttpGet]
        public ActionResult<List<string>> GetMachines([FromQuery] string? assetName, [FromQuery] string? assetVersion, [FromQuery] bool latestAssets)
        {
            _logger.Information("GetMachines API called with assetName={AssetName}, assetVersion={AssetVersion}, latestAssets={Latest}", assetName, assetVersion, latestAssets);
            try
            {
                var result = _machineService.GetMachines(assetName,assetVersion,latestAssets);

                if (result != null && result.Count!=0)
                {
                    _logger.Information("GetMachines returning {Count} machines", result.Count);
                    return Ok(result);
                }
                _logger.Warning("GetMachines found no machines for assetName={AssetName}, assetVersion={AssetVersion}", assetName, assetVersion);
                return NotFound("Machines not found");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unhandled exception in GetMachines for assetName={AssetName}, assetVersion={AssetVersion}", assetName, assetVersion);
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
            _logger.Information("GetMachine API called for {MachineName}", machineName);
            try
            {
                var result = _machineService.GetMachine(machineName);
                if(result != null)
                {
                    _logger.Information("GetMachine found machine {MachineName}", machineName);
                    return Ok(result);
                }
                else
                {
                    _logger.Warning("GetMachine did not find machine {MachineName}", machineName);
                    return NotFound($"Machine:{machineName} not found");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unhandled exception in GetMachine for {MachineName}", machineName);
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
            _logger.Information("GetMachineAssets API called for {MachineName}", machineName);
            try
            {
                var result = _machineService.GetMachineAssets(machineName);
                if( result != null && result.Count!=0)
                {
                    _logger.Information("GetMachineAssets returning {Count} assets for {MachineName}", result.Count, machineName);
                    return Ok(result);
                }
                else
                {
                    _logger.Warning("GetMachineAssets did not find assets for {MachineName}", machineName);
                    return NotFound($"Machine:{machineName} not found");
                }
            }catch(Exception ex)
            {
                _logger.Error(ex, "Unhandled exception in GetMachineAssets for {MachineName}", machineName);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
