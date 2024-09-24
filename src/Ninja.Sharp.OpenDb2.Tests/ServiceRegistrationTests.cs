// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ninja.Sharp.OpenDb2.Interfaces;
using Ninja.Sharp.OpenDb2.Tests.Attribute;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Interfaces.Windows;
using OpenDb2.Services;
using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Tests
{
    public class ServiceRegistrationTests
    {
        private readonly Mock<IServiceCollection> _servicesMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private const string TestConnectionString = "TestConnectionString";

        public ServiceRegistrationTests()
        {
            _servicesMock = new Mock<IServiceCollection>();
            _configurationMock = new Mock<IConfiguration>();
        }

        [WindowsOnlyFact]
        public void AddDb2Services_Should_Register_WinDb2Connection_On_Windows()
        {
            // Arrange
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Windows);

            // Act
            _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(IWinDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [LinuxOnlyFact]
        public void AddDb2Services_Should_Register_LnxDb2Connection_On_Linux()
        {
            // Arrange
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Linux);

            // Act
            _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(ILnxDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }
    }
}