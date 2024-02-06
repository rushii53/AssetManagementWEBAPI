using AssetManagementWEBAPI.Models;
using AssetManagementWEBAPI.Service;
namespace AssetManagementTEST
{
    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void MachineTest()
        {
            //Arrange
            MachineModel machine = new MachineModel("C300");

            AssetModel asset1 = new AssetModel("Cuuter Head", "S6");
            AssetModel asset2 = new AssetModel("Blade safety cover", "S10");
            AssetModel asset3 = new AssetModel("Deburring blades", "S6");

            machine.Asset.Add(asset1);
            machine.Asset.Add(asset2);  
            machine.Asset.Add(asset3);

            //Test
            
        }
    }
}