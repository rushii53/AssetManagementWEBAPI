using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Repository;
using MongoDB.Driver;

namespace AssetManagementWEBAPI.Service
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private Serilog.ILogger _logger;

        public MachineService(IMachineRepository machineRepository, Serilog.ILogger logger)
        {
            _machineRepository = machineRepository;
            _logger = logger;
        }

        public List<string?> GetMachines(string? assetName, string? assetVersion, bool latestAssetsFlag)
        {
            _logger.Information("GetMachines called with assetName={AssetName}, assetVersion={AssetVersion}, latestAssets={Latest}", assetName, assetVersion, latestAssetsFlag);

            try
            {
                List<string> machines = [];

                if (!string.IsNullOrEmpty(assetName) && !string.IsNullOrEmpty(assetVersion))
                {
                    _logger.Debug("Querying repository for machines by asset name and version");
                    machines = _machineRepository.GetMachinesByAssetNameAndAssetVersion(assetName, assetVersion);
                }
                else if (!string.IsNullOrEmpty(assetName) && !string.IsNullOrWhiteSpace(assetName))
                {
                    _logger.Debug("Querying repository for machines by asset name");
                    machines = _machineRepository.GetMachinesByAssetName(assetName);
                }
                else if (!string.IsNullOrEmpty(assetVersion) && !string.IsNullOrWhiteSpace(assetVersion))
                {
                    _logger.Debug("Querying repository for machines by asset version");
                    machines = _machineRepository.GetMachinesByAssetVersion(assetVersion);
                }
                else
                {
                    _logger.Debug("Querying repository for all machines");
                    machines = _machineRepository.GetMachines();
                }

                if (latestAssetsFlag)
                {
                    _logger.Debug("Filtering machines to only those with latest assets");
                    var machinesWithLatestAssets = _machineRepository.GetMachinesWithLatestAssets();
                    machines = machines.Intersect(machinesWithLatestAssets).ToList();
                }

                _logger.Information("GetMachines returning {Count} machines", machines?.Count ?? 0);
                return machines;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while getting machines");
                throw;
            }
        }
        public Machine GetMachine(string machineName)
        {
            _logger.Information("GetMachine called for {MachineName}", machineName);
            try
            {
                var result = _machineRepository.GetMachine(machineName);
                _logger.Information(result != null ? "Found machine {MachineName}" : "Machine {MachineName} not found", machineName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while getting machine {MachineName}", machineName);
                throw;
            }
        }
        public List<Asset> GetMachineAssets(string machineName)
        {
            _logger.Information("GetMachineAssets called for {MachineName}", machineName);
            try
            {
                var result = _machineRepository.GetMachineAssets(machineName);
                _logger.Information("GetMachineAssets returning {Count} assets for {MachineName}", result?.Count ?? 0, machineName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while getting assets for machine {MachineName}", machineName);
                throw;
            }
        }
    }
}
