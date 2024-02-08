using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineList:ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        [Parameter]
        public List<MachineModel>?Machines { get; set; }

        public Dictionary<string,bool>IsMachineSelected { get; set; } = new Dictionary<string,bool>();
        protected override async Task OnInitializedAsync()
        {
            foreach (var machine in Machines)
            {
                IsMachineSelected.Add(machine.MachineName, false);
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            IsMachineSelected = new Dictionary<string,bool>();
            foreach (var machine in Machines)
            {
                IsMachineSelected.Add(machine.MachineName, false);
            }
        }
        public void HandleClick(string machineName)
        {
            if (IsMachineSelected[machineName])
                IsMachineSelected[machineName] = false;
            else
                IsMachineSelected[machineName] = true;
        }
    }
}
