namespace AssetManagementWEBAPI.Models
{
    public class MachineModel
    {
        public string? MachineName { get; set; }
        public List<AssetModel>? Asset { get; set; }

        public MachineModel(string machineName) {
            MachineName = machineName;
            Asset = new List<AssetModel>();
        }

        public MachineModel() { }
    }
}
