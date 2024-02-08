namespace AssetManagementBlazor.Models
{
    public class MachineModel
    {
        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public List<AssetModel>Asset {  get; set; }
    }
}
