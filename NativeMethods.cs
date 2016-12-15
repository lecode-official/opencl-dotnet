
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
        /// <param name="param_name">An enumeration constant that identifies the platform information being queried.</param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than
        /// or equal to size of return type specified above.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_value"/> will be returned. If
        /// <see cref="param_value"/> is <c>null</c>, it is ignored.
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
        /// devices or all OpenCL devices available.
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

        /// <summary>
        /// Get information about an OpenCL device.
        /// </summary>
        /// <param name="device">
        /// A device returned by <see cref="GetDeviceIds"/>. May be a device returned by <see cref="GetDeviceIds"/> or a sub-device
        /// created by <see cref="CreateSubDevices"/>. If device is a sub-device, the specific information for the sub-device will
        /// be returned.
        /// </param>
        /// <param name="param_name">An enumeration constant that identifies the device information being queried.</param>
        /// <param name="param_value_size">
        /// Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than
        /// or equal to the size of return type specified.
        /// </param>
        /// <param name="param_value">
        /// A pointer to memory location where appropriate values for a given <see cref="param_name"/>. If <see cref="param_value"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <param name="param_value_size_ret">
        /// Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/>
        /// is <c>null</c>, it is ignored.
        /// </param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not valid.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified
        /// by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/> is not a <c>null</c>
        /// value or if <see cref="param_name"/> is a value that is available as an extension and the corresponding extension is
        /// not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the
        /// device.
        /// 
        /// <c>Result.OutOfHostMemory</c>  if there is a failure to allocate resources required by the OpenCL implementation on the
        /// host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceInfo")]
        public static extern Result GetDeviceInfo(
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret);

        #endregion
    }
}