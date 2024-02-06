namespace AssetManagementWEBAPI.Models
{
    public class Machine
    {
        public string MachineName { get; set; }
        public List<Asset> Asset { get; set; } = new List<Asset>();

        public Machine() { }
        public Machine(string machineName)
        {
            MachineName = machineName;
            Asset = new List<Asset>();
        }
    }
}
