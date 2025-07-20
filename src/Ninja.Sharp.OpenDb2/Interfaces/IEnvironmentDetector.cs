// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Interfaces
{
    public interface IEnvironmentDetector
    {
        OSPlatform GetCurrentOSPlatform();
        bool IsWindows();
        bool IsLinux();
    }
}
