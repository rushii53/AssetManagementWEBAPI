using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class Home:ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        [Parameter]
        public List<MachineModel>? Machines { get; set; } = null;
        public string machineName = null;
        public string assetName = null;

        protected override async Task OnInitializedAsync()
        {
            Machines = (await MachineService.GetAllMachines()).ToList();
        }

        public async void GetLatestMachines()
        {
            Machines = (await MachineService.GetMachinesUsesLatestAssets()).ToList();
            StateHasChanged();
        }
        public async void GetAllMachines()
        {
            Machines = (await MachineService.GetAllMachines()).ToList();
            StateHasChanged();
        }
        public async void GetMachineByAssetName()
        {
            if(assetName!=null && assetName.Length!=0)
            {
                Machines = (await MachineService.GetMachineByAssetName(assetName)).ToList();
                StateHasChanged();
            }
            assetName = "";
        }
        public async void GetMachineByMachineName()
        {
            if(machineName!=null && machineName.Length!=0)
            {
                Machines = (await MachineService.GetMachineByMachineName(machineName)).ToList();
                StateHasChanged();
            }
            machineName = "";
        }
    }
}
