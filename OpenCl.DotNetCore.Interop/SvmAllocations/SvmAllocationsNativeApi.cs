
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.SvmAllocations
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL SVM Allocations API.
    /// </summary>
    public static class SvmAllocationsNativeApi
    {
        #region Public Static Methods

        [DllImport("OpenCL", EntryPoint = "clSVMAlloc")]
        public static extern IntPtr SvmAllocate(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U8)] SvmMemoryFlag flags,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint alignment
        );

        [DllImport("OpenCL", EntryPoint = "clSVMFree")]
        public static extern void SvmFree(
            [In] IntPtr context,
            [In] IntPtr svmPointer
        );

        #endregion
    }
}