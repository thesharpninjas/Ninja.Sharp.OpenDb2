// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Ninja.Sharp.OpenDb2.Interfaces;
using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Utilities
{
    public class EnvironmentDetector : IEnvironmentDetector
    {
        public OSPlatform GetCurrentOSPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows :
                   RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSPlatform.Linux :
                   RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX :
                   OSPlatform.Create("Other");
        }

        public bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
