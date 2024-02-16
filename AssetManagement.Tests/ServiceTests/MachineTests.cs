using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Repository;
using AssetManagementWEBAPI.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Tests.ServiceTests
{
    public class MachineTests
    {
        private readonly Mock<IMachineRepository> _machineRepository = new();
        private readonly MachineService _machineService;

        public MachineTests() { 
            _machineService = new MachineService(_machineRepository.Object);
        }

        [Fact]
        public void Get_Machine_By_Providing_Machine_Name()
        {
            #region Arragne
            string machineName = "M1";

            List<Asset> assets = new List<Asset>()
            {
                new Asset{AssetName="A1",AssetVersion="v1"},
                new Asset{AssetName="A2",AssetVersion="v2"}
            };

            Machine machine = new Machine { MachineName = machineName, Asset = assets };

            _machineRepository.Setup(repo => repo.GetMachine(machineName)).Returns(machine);
            #endregion


            #region Act
            var actual = _machineService.GetMachine(machineName);
            #endregion

            #region Assert
            Assert.NotNull(actual);
            Assert.Equal(machine.Asset.Count, actual.Asset.Count);
            #endregion
        }

        [Fact]
        public void Get_Names_Of_Machines_By_Providing_AssetName()
        {
            #region Arrange
            string assetName = "A1";
            string assetVersion = null;
            bool latestAsset = false;
            List<string> expectedResult = new List<string>() { "M1", "M2" };
            _machineRepository.Setup(repo=>repo.GetMachinesByAssetName(assetName)).Returns(expectedResult);
            #endregion

            #region Act
            var actual = _machineService.GetMachines(assetName,assetVersion,latestAsset);
            #endregion

            #region Assert
            Assert.Equal(expectedResult, actual);
            #endregion
        }

        [Fact]
        public void Get_Names_Of_Machines_By_Providing_AssetName_And_AssetVersion()
        {
            #region Arrange
            string assetName = "A1";
            string assetVersion = "V1";
            bool latestAsset = false;

            List<string> expectedResult = new List<string>() { "M1", "M2" };

            _machineRepository.Setup(repo=>repo.GetMachinesByAssetNameAndAssetVersion(assetName,assetVersion)).Returns(expectedResult);
            #endregion

            #region Act
            var actual = _machineService.GetMachines(assetName, assetVersion, latestAsset);
            #endregion

            #region Assert
            Assert.Equal(expectedResult,actual);
            #endregion
        }
        [Fact]
        public void Get_Names_Of_Machines_By_Providing_AssetName_AssetVersion_LatestAssetFlag()
        {
            #region Arrange
            string assetName = "A1";
            string assetVersion = "V1";
            bool latestAsset = true;

            List<string>expectedResult_1 = new List<string>() { "M1", "M2" };
            List<string> expectedResult = new List<string>() { "M1"};

            _machineRepository.Setup(repo => repo.GetMachinesByAssetNameAndAssetVersion(assetName, assetVersion)).Returns(expectedResult_1);
            _machineRepository.Setup(repo=>repo.GetMachinesWithLatestAssets()).Returns(expectedResult);
            #endregion

            #region Act
            var actual = _machineService.GetMachines(assetName,assetVersion,latestAsset);
            #endregion

            #region Assert
            Assert.Equal(expectedResult, actual);
            #endregion
        }
    }
}
