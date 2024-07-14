// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Utilities
{
    public class EnvironmentDetector
    {
        public static OSPlatform GetCurrentOSPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? OSPlatform.Windows :
                   RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? OSPlatform.Linux :
                   RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? OSPlatform.OSX :
                   OSPlatform.Create("Other");
        }

        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
