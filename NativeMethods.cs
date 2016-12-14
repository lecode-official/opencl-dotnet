
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL library.
    /// </summary>
    public static class NativeMethods
    {
        #region Platform Quering Methods

        /// <summary>
        /// Obtain the list of platforms available.
        /// </summary>
        /// <param name="num_entries">
        /// The number of cl_platform_id entries that can be added to <see cref="platforms"/>. If <see cref="platforms"/> is not <c>null</c>, the <see cref="num_entries"/> must
        /// be greater than zero.
        /// </param>
        /// <param name="platforms">
        /// Returns a list of OpenCL platforms found. The cl_platform_id values returned in <see cref="platforms"/> can be used to identify a specific OpenCL platform. If
        /// <see cref="platforms"/> argument is <c>null</c>, this argument is ignored. The number of OpenCL platforms returned is the mininum of the value specified byte
        /// <see cref="num_entries"/> or the number of OpenCL platforms available.
        /// </param>
        /// <param name="num_platforms">
        /// Returns the number of OpenCL platforms available. If <see cref="num_platforms"/> is <c>null</c>, this argument is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise it returns <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero
        /// and <see cref="platforms"/> is not <c>null</c>, or if both <see cref="num_platforms"/> and <see cref="platforms"/> are NULL.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformIDs")]
        public static extern Result GetPlatformIds(
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_platforms);

        /// <summary>
        /// Get specific information about the OpenCL platform.
        /// </summary>
        /// <param name="platform">
        /// The platform ID returned by <see cref="GetPlatformIds"/> or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>, the behavior is implementation-defined.
        /// </param>
        /// <param name="param_name">
        /// An enumeration constant that identifies the platform information being queried. It can be one of the following values:
        /// 
        /// <c>PlatformInfo.Profile</c>: OpenCL profile string. Returns the profile name supported by the implementation. The profile name returned can be one of the
        /// following strings: FULL_PROFILE - if the implementation supports the OpenCL specification (functionality defined as part of the core specification and does not require
        /// any extensions to be supported). EMBEDDED_PROFILE - if the implementation supports the OpenCL embedded profile. The embedded profile is defined to be a subset for each
        /// version of OpenCL.
        /// 
        /// <c>PlatformInfo.Version</c>: OpenCL version string. Returns the OpenCL version supported by the implementation. This version string has the following format:
        /// "OpenCL[space][major_version.minor_version][space][platform-specific information].
        /// 
        /// <c>PlatformInfo.Name</c>: Platform name string.
        /// 
        /// <c>PlatformInfo.Vendor</c>: Platform vendor string.
        /// 
        /// <c>PlatformInfo.Extensions</c>: Returns a space-separated list of extension names (the extension names themselves do not contain any spaces) supported by the platform.
        /// Extensions defined here must be supported by all devices associated with this platform.
        /// 
        /// <c>PlatformInfo.HostTimerResolution</c>: The host timer resolution.
        /// </param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than or equal to size of return type specified in
        /// the table below.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_value"/> will be returned. Acceptable <see cref="param_value"/> values are listed
        /// above. If <see cref="param_value"/> is <c>null</c>, it is ignored.
        /// </param>
        /// <param name="param_value_size_ret">
        /// Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/> is <c>null</c>, it is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns <c>Result.InvalidPlatform</c> if platform is not a valid platform or
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified by <see cref="param_value_size"/> is less
        /// than size of return type and <see cref="param_value"/ is not a <c>null</c> value.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformInfo")]
        public static extern Result GetPlatformInfo(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] PlatformInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret);

        #endregion
    }
}