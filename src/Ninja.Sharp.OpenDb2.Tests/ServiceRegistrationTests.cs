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

        [Fact]
        public void AddDb2Services_Should_Register_WinDb2Connection_On_Windows()
        {
            // Arrange
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Windows);

            // Act
            _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, environmentDetectorMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(IWinDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [Fact]
        public void AddDb2Services_Should_Register_LnxDb2Connection_On_Linux()
        {
            // Arrange
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Linux);

            // Act
            _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, environmentDetectorMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(ILnxDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [Fact]
        public void AddDb2Services_Should_Throw_ArgumentNullException_When_Services_Is_Null()
        {
            IServiceCollection? services = null;

            Assert.Throws<ArgumentNullException>(() =>
                services!.AddDb2Services(TestConnectionString, _configurationMock.Object));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void AddDb2Services_Should_Throw_ArgumentException_When_ConnectionString_Is_Invalid(string? connectionString)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                _servicesMock.Object.AddDb2Services(connectionString!, _configurationMock.Object));
        }

        [Fact]
        public void AddWinDb2Services_Should_Throw_ArgumentNullException_When_Services_Is_Null()
        {
            IServiceCollection? services = null;

            Assert.Throws<ArgumentNullException>(() =>
                services!.AddWinDb2Services(TestConnectionString, _configurationMock.Object));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void AddWinDb2Services_Should_Throw_ArgumentException_When_ConnectionString_Is_Invalid(string? connectionString)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                _servicesMock.Object.AddWinDb2Services(connectionString!, _configurationMock.Object));
        }

        [WindowsOnlyFact]
        public void AddWinDb2Services_Should_Register_WinDb2Connection()
        {
            // Act
            _servicesMock.Object.AddWinDb2Services(TestConnectionString, _configurationMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(IWinDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [Fact]
        public void AddLnxDb2Services_Should_Throw_ArgumentNullException_When_Services_Is_Null()
        {
            IServiceCollection? services = null;

            Assert.Throws<ArgumentNullException>(() =>
                services!.AddLnxDb2Services(TestConnectionString, _configurationMock.Object));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void AddLnxDb2Services_Should_Throw_ArgumentException_When_ConnectionString_Is_Invalid(string? connectionString)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
                _servicesMock.Object.AddLnxDb2Services(connectionString!, _configurationMock.Object));
        }

        [LinuxOnlyFact]
        public void AddLnxDb2Services_Should_Register_LnxDb2Connection()
        {
            // Act
            _servicesMock.Object.AddLnxDb2Services(TestConnectionString, _configurationMock.Object);

            // Assert
            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(ILnxDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [Fact]
        public void AddDb2Services_Should_Return_Same_ServiceCollection()
        {
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Windows);

            var result = _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, environmentDetectorMock.Object);

            Assert.Same(_servicesMock.Object, result);
        }

        [Fact]
        public void AddDb2Services_Should_Register_LnxDb2Connection_On_OSX()
        {
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.OSX);

            _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, environmentDetectorMock.Object);

            _servicesMock.Verify(s => s.Add(It.Is<ServiceDescriptor>(sd =>
                sd.ServiceType == typeof(ILnxDb2Connection) &&
                sd.ImplementationFactory != null)), Times.Once);
        }

        [Fact]
        public void AddDb2Services_Should_Throw_NotSupportedException_On_Unknown_Platform()
        {
            var environmentDetectorMock = new Mock<IEnvironmentDetector>();
            environmentDetectorMock.Setup(e => e.GetCurrentOSPlatform()).Returns(OSPlatform.Create("Other"));

            Assert.Throws<NotSupportedException>(() =>
                _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, environmentDetectorMock.Object));
        }

        [Fact]
        public void AddDb2Services_Should_Throw_ArgumentNullException_When_EnvironmentDetector_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _servicesMock.Object.AddDb2Services(TestConnectionString, _configurationMock.Object, null!));
        }

        [WindowsOnlyFact]
        public void AddWinDb2Services_Should_Return_Same_ServiceCollection()
        {
            var result = _servicesMock.Object.AddWinDb2Services(TestConnectionString, _configurationMock.Object);

            Assert.Same(_servicesMock.Object, result);
        }

        [LinuxOnlyFact]
        public void AddLnxDb2Services_Should_Return_Same_ServiceCollection()
        {
            var result = _servicesMock.Object.AddLnxDb2Services(TestConnectionString, _configurationMock.Object);

            Assert.Same(_servicesMock.Object, result);
        }
    }
}