// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Tests.Attribute
{
    public class LinuxOnlyFactAttribute : FactAttribute
    {
        public LinuxOnlyFactAttribute()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Skip = "This test can only be run on Linux.";
            }
        }
    }
}
