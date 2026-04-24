// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using System.Runtime.InteropServices;

namespace Ninja.Sharp.OpenDb2.Interfaces
{
    /// <summary>
    /// Detects the operating system platform at runtime.
    /// </summary>
    public interface IEnvironmentDetector
    {
        /// <summary>
        /// Returns the current <see cref="OSPlatform"/> (Windows, Linux, OSX, or a custom "Other" value).
        /// </summary>
        OSPlatform GetCurrentOSPlatform();

        /// <summary>
        /// Returns <see langword="true"/> when running on Windows.
        /// </summary>
        bool IsWindows();

        /// <summary>
        /// Returns <see langword="true"/> when running on Linux.
        /// </summary>
        bool IsLinux();
    }
}
