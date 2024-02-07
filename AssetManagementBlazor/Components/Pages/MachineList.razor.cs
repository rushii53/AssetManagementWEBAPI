using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineList:ComponentBase
    {
        [Inject]
        public IMachineService MachineService { get; set; }
        public List<MachineModel>Machines { get; set; }

        public Dictionary<string,bool>IsMachineSelected { get; set; } = new Dictionary<string,bool>();
        protected override async Task OnInitializedAsync()
        {
            Machines = (await MachineService.GetAllMachines()).ToList();

            foreach (var machine in Machines)
            {
                IsMachineSelected[machine.MachineName] = false;
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
