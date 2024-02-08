using AssetManagementBlazor.Models;
using Microsoft.AspNetCore.Components;

namespace AssetManagementBlazor.Components.Pages
{
    public partial class AssetList:ComponentBase
    {
        [Parameter]
        public List<AssetModel> Assets { get; set; }
    }
}
