using AssetManagementWEBAPI.Controllers;
using AssetManagementWEBAPI.Entity;
using AssetManagementWEBAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AssetManagement.Tests.ControllerTests
{
    public class MachineControllerTests
    {
        private Mock<IMachineService> _machineServiceMock = new Mock<IMachineService>();
        private MachineControllerV1 _machineController;

        public MachineControllerTests()
        {
            _machineController = new MachineControllerV1(_machineServiceMock.Object);
        }

        [Fact]
        public void GetMachines_Returns_OkResult_With_Machines_When_Machines_Found()
        {
            #region Arrange
            List<string> expectedResult = new List<string> { "M1", "M2" };

            _machineServiceMock.Setup(service => service.GetMachines(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedResult);
            #endregion
            #region Act
            var actionResult = _machineController.GetMachines(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            #endregion
            #region Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(expectedResult, okResult.Value);
            #endregion
        }

        [Fact]
        public void GetMachines_Return_NotFound_When_Machines_Not_Found()
        {
            #region Arrange
            List<string> expectedResult = new List<string>();

            _machineServiceMock.Setup(service => service.GetMachines(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedResult);
            #endregion
            #region Act
            var actionResult = _machineController.GetMachines(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>());
            #endregion
            #region Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            #endregion
        }

        [Fact]
        public void GetMachine_Return_OkResult_With_Machine_When_Machine_Name_Found()
        {
            #region Arrange
            string machineName = "M1";
            List<Asset> assets = new List<Asset>()
            {
                new Asset {AssetName="A1",AssetVersion="V1"},
                new Asset {AssetName="A2",AssetVersion="V2"}
            };
            Machine expectedResult = new Machine() { MachineName = "M1", Asset = assets };

            _machineServiceMock.Setup(service => service.GetMachine(machineName)).Returns(expectedResult);
            #endregion
            #region Act
            var actionResult = _machineController.GetMachine(machineName);
            #endregion
            #region Assert
            var actual = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(expectedResult, actual.Value);
            #endregion
        }

        [Fact]
        public void GetMachineAssets_Return_OkResult_When_Machine_Name_Found()
        {
            #region Arrange
            string machineName = "M1";
            List<Asset> expectedResult = new List<Asset>() {
                new Asset{ AssetName = "A1",AssetVersion="V1"},
                new Asset{ AssetName = "A2",AssetVersion="V2"}
            };

            _machineServiceMock.Setup(service => service.GetMachineAssets(machineName)).Returns(expectedResult);
            #endregion
            #region Act
            var actionResult = _machineController.GetMachineAssets(machineName);
            #endregion
            #region Assert
            var actual = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(expectedResult, actual.Value);
            #endregion
        }
    }
}
