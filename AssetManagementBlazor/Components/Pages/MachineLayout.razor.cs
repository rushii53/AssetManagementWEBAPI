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
        public List<string> _machines;
        public MachineModel machine = new MachineModel();
        public string? assetName = null;
        public string? assetVersion = null;
        public string machineName;
        protected override async Task OnInitializedAsync()
        {
            ApiResponse<List<string>> response = await _machineService.GetMachines(null, null, false);
            if(response.Success)
            {
                _machines = response.Data;
            }
        }
        
        public async void GetMachines()
        {
            ApiResponse<List<string>> response = await _machineService.GetMachines(assetName, assetVersion, false);
            if (response.Success)
            {
                _machines = response.Data;
            }
            StateHasChanged();
        }
        public async void GetMachinesWithLatestAssets()
        {
            ApiResponse<List<string>> response = await _machineService.GetMachines(assetName, assetVersion, true);
            if (response.Success)
            {
                _machines = response.Data;
            }
            StateHasChanged();
        }
        public void NavigateToMachinePage(string machineName)
        {
            NavigationManager.NavigateTo($"/machine/{machineName}");
        }
    }
}
