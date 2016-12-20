
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Devices
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Devices API.
    /// </summary>
    public static class DevicesNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Obtain the list of devices available on a platform.
        /// </summary>
        /// <param name="platform">Refers to the platform ID returned by <see cref="GetPlatformIds"/< or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>, the behavior is implementation-defined.</param>
        /// <param name="deviceType">A bitfield that identifies the type of OpenCL device. The <see cref="deviceType"/> can be used to query specific OpenCL devices or all OpenCL devices available.</param>
        /// <param name="numberOfEntries">The number of device entries that can be added to <see cref="devices"/>. If <see cref="devices"/> is not <c>null</c>, the <see cref="numberOfEntries"/> must be greater than zero.</param>
        /// <param name="devices">
        /// A list of OpenCL devices found. The device values returned in <see cref="devices"/> can be used to identify a specific OpenCL device. If <see cref="devices"/> argument is <c>null</c>, this argument is ignored. The number of
        /// OpenCL devices returned is the mininum of the value specified by <see cref="numberOfEntries"/> or the number of OpenCL devices whose type matches <see cref="deviceType"/>.
        /// </param>
        /// <param name="numberOfDevices">The number of OpenCL devices available that match <see cref="deviceType". If <see cref="numberOfDevices"/> is <c>null</c>, this argument is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidPlatform</c> if <see cref="platform"/> is not a valid platform.
        /// 
        /// <c>Result.InvalidDeviceType</c> if <see cref="deviceType"/> is not a valid value.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="numberOfEntries"/> is equal to zero and <see cref="devices"/> is not <c>null</c> or if both <see cref="numberOfDevices"/> and <see cref="devices"/> are <c>null</c>.
        /// 
        /// <c>Result.DeviceNotFound</c> if no OpenCL devices that matched <see cref="deviceType"/> were found.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceIDs")]
        public static extern Result GetDeviceIds(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceType deviceType,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEntries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint numberOfDevices
        );

        /// <summary>
        /// Get information about an OpenCL device.
        /// </summary>
        /// <param name="device">
        /// A device returned by <see cref="GetDeviceIds"/>. May be a device returned by <see cref="GetDeviceIds"/> or a sub-device created by <see cref="CreateSubDevices"/>. If device is a sub-device, the specific information for the
        /// sub-device will be returned.
        /// </param>
        /// <param name="parameterName">An enumeration constant that identifies the device information being queried.</param>
        /// <param name="parameterValueSize">Specifies the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size in bytes must be greater than or equal to the size of return type specified.</param>
        /// <param name="parameterValue">A pointer to memory location where appropriate values for a given <see cref="parameterName"/>. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">Returns the actual size in bytes of data being queried by <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not valid.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/> is
        /// not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceInfo")]
        public static extern Result GetDeviceInformation(
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clCreateSubDevices(cl_device_id                         /* in_device */,
        //                const cl_device_partition_property * /* properties */,
        //                cl_uint                              /* num_devices */,
        //                cl_device_id *                       /* out_devices */,
        //                cl_uint *                            /* num_devices_ret */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainDevice(cl_device_id /* device */) CL_API_SUFFIX__VERSION_1_2;

        /// <summary>
        /// Decrements the device reference count if device is a valid sub-device created by a call to <see cref="CreateSubDevices"/>. If device is a root level device i.e. a device returned by <see cref="GetDeviceIDs"/>, the device
        /// reference count remains unchanged.
        /// </summary>
        /// <param name="device">The device to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not a valid device object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>

        [DllImport("OpenCL", EntryPoint = "clReleaseDevice")]
        public static extern Result ReleaseDevice([In] IntPtr device);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clSetDefaultDeviceCommandQueue(cl_context           /* context */,
        //                            cl_device_id         /* device */,
        //                            cl_command_queue     /* command_queue */) CL_API_SUFFIX__VERSION_2_1;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetDeviceAndHostTimer(cl_device_id    /* device */,
        //                        cl_ulong*       /* device_timestamp */,
        //                        cl_ulong*       /* host_timestamp */) CL_API_SUFFIX__VERSION_2_1;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetHostTimer(cl_device_id /* device */,
        //            cl_ulong *   /* host_timestamp */)  CL_API_SUFFIX__VERSION_2_1;

        #endregion
    }
}