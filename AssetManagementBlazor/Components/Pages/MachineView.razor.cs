using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineView:ComponentBase
    {
        [Inject]
        public  IMachineService _machineService { get; set; }
        public MachineView() { }
        public MachineView(IMachineService machineService)
        {
            _machineService = machineService;
        }

        public MachineModel Model = null;

        [Parameter]
        public string machineName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(machineName))
            {
                Model = await _machineService.GetMachine(machineName);
            }
        }
    }

}
