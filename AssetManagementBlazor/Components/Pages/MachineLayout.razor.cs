using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineLayout:ComponentBase
    {
        [Inject]
        public IMachineService _machineService { set; get; }
        public MachineLayout() { }
        public MachineLayout(IMachineService machineService) {
            _machineService = machineService;
        }
        public List<string> _machines = new List<string>();
        public MachineModel machine = new MachineModel();
        public string? assetName = null;
        public string? assetVersion = null;
        public string machineName;
        protected override async Task OnInitializedAsync()
        {
            _machines = await _machineService.GetMachines(null,null,false);
        }
        
        public async void GetMachines()
        {
            _machines = await _machineService.GetMachines(assetName,assetVersion,false);
            StateHasChanged();
        }
        public async void GetMachinesWithLatestAssets()
        {
            _machines = await _machineService.GetMachines(assetName,assetVersion,true);
            StateHasChanged();
        }
        public void NavigateToMachinePage(string machineName)
        {
            NavigationManager.NavigateTo($"/machine/{machineName}");
        }
    }
}
