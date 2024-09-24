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
