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

        [Parameter]
        public string machineName { get; set; }

        public MachineModel Machine { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(machineName))
            {
                ApiResponse<MachineModel>response = await _machineService.GetMachine(machineName);
                if (response.Success)
                {
                    Machine = response.Data;
                }
            }
        }
    }

}
