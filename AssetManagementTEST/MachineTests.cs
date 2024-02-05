using AssetManagementWEBAPI.Models;

namespace AssetManagementTEST
{
    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void MachineTest()
        {
            //Arrange
            Machine machine = new Machine("C300");

            Asset asset1 = new Asset("Cuuter Head", "S6");
            Asset asset2 = new Asset("Blade safety cover", "S10");
            Asset asset3 = new Asset("Deburring blades", "S6");

            machine.Asset.Add(asset1);
            machine.Asset.Add(asset2);  
            machine.Asset.Add(asset3);
            
            //Test
        }
    }
}