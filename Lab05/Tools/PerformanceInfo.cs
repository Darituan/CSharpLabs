using System;
using System.Runtime.InteropServices;

namespace Lab05.Tools
{
    internal static class PerformanceInfo
    {
        [DllImport("psapi.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation,
            [In] int Size);

        [StructLayout(LayoutKind.Sequential)]
        private struct PerformanceInformation
        {
            internal int Size;
            internal IntPtr CommitTotal;
            internal IntPtr CommitLimit;
            internal IntPtr CommitPeak;
            internal IntPtr PhysicalTotal;
            internal IntPtr PhysicalAvailable;
            internal IntPtr SystemCache;
            internal IntPtr KernelTotal;
            internal IntPtr KernelPaged;
            internal IntPtr KernelNonPaged;
            internal IntPtr PageSize;
            internal int HandlesCount;
            internal int ProcessCount;
            internal int ThreadCount;
        }

        public static long GetTotalMemory()
        {
            var pi = new PerformanceInformation();
            if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64()));
            return -1;
        }
    }
}