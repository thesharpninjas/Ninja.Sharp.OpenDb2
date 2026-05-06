// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Ninja.Sharp.OpenDb2.Interfaces;
using Ninja.Sharp.OpenDb2.Tests.Attribute;
using Ninja.Sharp.OpenDb2.Utilities;
using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Tests
{
    public class EnvironmentDetectorTests
    {
        private readonly IEnvironmentDetector _detector = new EnvironmentDetector();

        [WindowsOnlyFact]
        public void GetCurrentOSPlatform_Should_Return_Windows_On_Windows()
        {
            Assert.Equal(OSPlatform.Windows, _detector.GetCurrentOSPlatform());
        }

        [WindowsOnlyFact]
        public void IsWindows_Should_Return_True_On_Windows()
        {
            Assert.True(_detector.IsWindows());
        }

        [WindowsOnlyFact]
        public void IsLinux_Should_Return_False_On_Windows()
        {
            Assert.False(_detector.IsLinux());
        }
    }
}
