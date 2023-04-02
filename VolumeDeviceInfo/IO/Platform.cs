using System;

namespace RJCP.IO
{
    internal static class Platform
    {
        public static bool IsWinNT()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
    }
}