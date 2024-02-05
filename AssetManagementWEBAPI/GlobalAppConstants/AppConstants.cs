using AssetManagementWEBAPI.Models;

namespace AssetManagementWEBAPI.GlobalAppConstants
{
    public static class AppConstants
    {
        public static List<Machine> Machines = new List<Machine>();
  
        public static Dictionary<string,List<string>> MachinesList { get; set; } = new Dictionary<string, List<string>>();

        public static Dictionary<string, string> AssetsLatestVersionsDictionary { get; set; } = new Dictionary<string, string>();
    }
}
