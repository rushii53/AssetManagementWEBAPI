namespace AssetManagementWEBAPI.Models
{
    public class Machine
    {
        public string MachineName { get; set; }
        public List<Asset> Asset { get; set; } = new List<Asset>();
    }
}
