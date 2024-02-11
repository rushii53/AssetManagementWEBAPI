using AssetManagementBlazor.Models;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineView
    {
        public MachineModel Model = new MachineModel()
        {
            MachineName = "C300",
            Assets = new List<AssetModel>()
            {
                new AssetModel(){
                    AssetName="Cutter Head",
                    AssetVersion="S10"
                },
                new AssetModel(){
                    AssetName="Clamping Fixture",
                    AssetVersion="S7"
                },
                new AssetModel(){
                    AssetName="Blade Handle",
                    AssetVersion="S4"
                }
            }
        };
    }

}
