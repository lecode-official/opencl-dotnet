
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Samplers
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Samplers API.
    /// </summary>
    public static class SamplersNativeApi
    {
        #region Public Static Methods

        [DllImport("OpenCL", EntryPoint = "clCreateSamplerWithProperties")]
        public static extern IntPtr CreateSamplerWithProperties(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] normalizedCoordinates,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        [DllImport("OpenCL", EntryPoint = "clRetainSampler")]
        public static extern Result RetainSample(
            [In] IntPtr sampler
        );

        [DllImport("OpenCL", EntryPoint = "clReleaseSampler")]
        public static extern Result ReleaseSampler(
            [In] IntPtr sampler
        );

        [DllImport("OpenCL", EntryPoint = "clGetSamplerInfo")]
        public static extern Result GetSamplerInformation(
            [In] IntPtr sampler,
            [In] [MarshalAs(UnmanagedType.U4)] SamplerInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        #endregion

        #region Deprecated Static Methods

        [DllImport("OpenCL", EntryPoint = "clCreateSampler")]
        [Obsolete("This is a deprecated OpenCL 1.2 method, please use CreateImage instead.")]
        public static extern IntPtr CreateSample(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U4)] uint normalizedCoordinates,
            [In] [MarshalAs(UnmanagedType.U4)] AddressingMode addressingMode,
            [In] [MarshalAs(UnmanagedType.U4)] FilterMode filterMode,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        #endregion
    }
}