using AssetManagementBlazor.Models;
using AssetManagementBlazor.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class MachineLayout:ComponentBase
    {
        public List<string> _machines = new List<string>
        {
            "Machine 1",
            "Machine 2",
            "Machine 3",
            "Machine 4",
            "Machine 5",
            "Machine 6",
            "Machine 7"
        };
    }
}
