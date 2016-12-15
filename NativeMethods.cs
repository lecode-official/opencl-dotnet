
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
        #region Platform API Methods

        /// <summary>
        /// Obtain the list of platforms available.
        /// </summary>
        /// <param name="num_entries">
        /// The number of platform entries that can be added to <see cref="platforms"/>. If <see cref="platforms"/> is not
        /// <c>null</c>, the <see cref="num_entries"/> must be greater than zero.
        /// </param>
        /// <param name="platforms">
        /// Returns a list of OpenCL platforms found. The platform values returned in <see cref="platforms"/> can be used to
        /// identify a specific OpenCL platform. If <see cref="platforms"/> argument is <c>null</c>, this argument is ignored. The
        /// number of OpenCL platforms returned is the mininum of the value specified by <see cref="num_entries"/> or the number
        /// of OpenCL platforms available.
        /// </param>
        /// <param name="num_platforms">
        /// Returns the number of OpenCL platforms available. If <see cref="num_platforms"/> is <c>null</c>, this argument is
        /// ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. If the cl_khr_icd extension is enabled, 
        /// <see cref="GetPlatformIds"/> returns <c>Result.Success</c> if the function is executed successfully and there are a non
        /// zero number of platforms available. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="platforms"/> is not <c>null</c>,
        /// or if both <see cref="num_platforms"/> and <see cref="platforms"/> are <c>null</c>.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// 
        /// <c>Result.PlatformNotFoundKhr</c> if the cl_khr_icd extension is enabled and no platforms are found.
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
        /// The platform ID returned by <see cref="GetPlatformIds"/> or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>,
        /// the behavior is implementation-defined.
        /// </param>
        /// <param name="param_name">
        /// An enumeration constant that identifies the platform information being queried. It can be one of the following values:
        /// 
        /// <c>PlatformInfo.Profile</c>: OpenCL profile string. Returns the profile name supported by the implementation. The
        /// profile name returned can be one of the following strings:
        /// FULL_PROFILE - if the implementation supports the OpenCL specification (functionality defined as part of the core
        /// specification and does not require any extensions to be supported).
        /// EMBEDDED_PROFILE - if the implementation supports the OpenCL embedded profile. The embedded profile is defined to be a
        /// subset for each version of OpenCL. The embedded profile for OpenCL 2.1 is described in section 7.
        /// 
        /// <c>PlatformInfo.Version</c>: OpenCL version string. Returns the OpenCL version supported by the implementation. This
        /// version string has the following format: "OpenCL[space][major_version.minor_version][space][platform-specific information]".
        /// 
        /// <c>PlatformInfo.Name</c>: Platform name string.
        /// 
        /// <c>PlatformInfo.Vendor</c>: Platform vendor string.
        /// 
        /// <c>PlatformInfo.Extensions</c>: Returns a space-separated list of extension names (the extension names themselves do
        /// not contain any spaces) supported by the platform. Extensions defined here must be supported by all devices associated
        /// with this platform.
        /// 
        /// <c>PlatformInfo.PlatformHostTimerResolution</c>: Returns the resolution of the host timer in nanoseconds as used by
        /// <see cref="GetDeviceAndHostTimer"/>.
        /// 
        /// <c>PlatformInfo.PlatformIcdSuffixKhr</c>: If the cl_khr_icd extension is enabled, the function name suffix used to
        /// identify extension functions to be directed to this platform by the ICD Loader.
        /// </param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than
        /// or equal to size of return type specified above.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_value"/> will be returned. Possible
        /// <see cref="param_value"/> values are listed above. If <see cref="param_value"/> is <c>null</c>, it is ignored.
        /// </param>
        /// <param name="param_value_size_ret">
        /// Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following: (The OpenCL
        /// specification does not describe the order of precedence for error codes returned by API calls)
        /// 
        /// <c>Result.InvalidPlatform</c> if platform is not a valid platform.!--
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified
        /// by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/ is not a <c>null</c>
        /// value.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformInfo")]
        public static extern Result GetPlatformInfo(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] PlatformInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret);

        #endregion

        #region Device API Methods

        /// <summary>
        /// Obtain the list of devices available on a platform.
        /// </summary>
        /// <param name="platform">
        /// Refers to the platform ID returned by <see cref="GetPlatformIds"/< or can be <c>null</c>. If <see cref="platform"/> is
        /// <c>null</c>, the behavior is implementation-defined.
        /// </param>
        /// <param name="device_type">
        /// A bitfield that identifies the type of OpenCL device. The <see cref="device_type"/> can be used to query specific OpenCL
        /// devices or all OpenCL devices available. The valid values for <see cref="device_type"/> are specified in the following
        /// list:
        /// 
        /// <c>DeviceType.Default</c>: The default OpenCL device in the system. The default device cannot be a <c>DeviceType.Custom</c>
        /// device.
        /// 
        /// <c>DeviceType.Cpu</c>: An OpenCL device that is the host processor. The host processor runs the OpenCL implementations and
        /// is a single or multi-core CPU.
        /// 
        /// <c>DeviceType.Gpu</c>: An OpenCL device that is a GPU. By this we mean that the device can also be used to accelerate a
        /// 3D API such as OpenGL or DirectX.
        /// 
        /// <c>DeviceType.Accelerator</c>: Dedicated OpenCL accelerators (for example the IBM CELL Blade). These devices communicate
        /// with the host processor using a peripheral interconnect such as PCIe.
        /// 
        /// <c>DeviceType.Custom</c>: Dedicated accelerators that do not support programs written in OpenCL C.
        /// 
        /// <c>DeviceType.All</c>: All OpenCL devices available in the system except <c>DeviceType.Custom</c> devices.
        /// </param>
        /// <param name="num_entries">
        /// The number of device entries that can be added to <see cref="devices"/>. If <see cref="devices"/> is not <c>null</c>,
        /// the <see cref="num_entries"/> must be greater than zero.
        /// </param>
        /// <param name="devices">
        /// A list of OpenCL devices found. The device values returned in <see cref="devices"/> can be used to identify a specific
        /// OpenCL device. If <see cref="devices"/> argument is <c>null</c>, this argument is ignored. The number of OpenCL devices
        /// returned is the mininum of the value specified by <see cref="num_entries"/> or the number of OpenCL devices whose type
        /// matches <see cref="device_type"/>.
        /// </param>
        /// <param name="num_devices">
        /// The number of OpenCL devices available that match <see cref="device_type". If <see cref="num_devices"/> is <c>null</c>,
        /// this argument is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidPlatform</c> if <see cref="platform"/> is not a valid platform.
        /// 
        /// <c>Result.InvalidDeviceType</c> if <see cref="device_type"/> is not a valid value.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="devices"/> is not <c>null</c>
        /// or if both <see cref="num_devices"/> and <see cref="devices"/> are <c>null</c>.
        /// 
        /// <c>Result.DeviceNotFound</c> if no OpenCL devices that matched <see cref="device_type"/> were found.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceIDs")]
        public static extern Result GetDeviceIds(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceType device_type,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_devices);

        #endregion
    }
}