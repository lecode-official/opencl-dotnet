
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Contexts
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Contexts API.
    /// </summary>
    public static class ContextsNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Creates an OpenCL context.
        /// </summary>
        /// <param name="properties">
        /// Specifies a list of context property names and their corresponding values. Each property name is immediately followed by the corresponding desired value. The list is terminated with 0. <see cref="properties"/> can be <c>null</c>
        /// in which case the platform that is selected is implementation-defined.
        /// </param>
        /// <param name="numberOfDevices">The number of devices specified in the <see cref="devices"/> argument.</param>
        /// <param name="devices">
        /// A pointer to a list of unique devices returned by <see cref="GetDeviceIds"/> or sub-devices created by <see cref="CreateSubDevices"/> for a platform. Duplicate devices specified in <see cref="devices"/> are ignored.
        /// </param>
        /// <param name="notificationCallback">
        /// A callback function that can be registered by the application. This callback function will be used by the OpenCL implementation to report information on errors during context creation as well as errors that occur at runtime in
        /// this context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. If <see cref="notificationCallback"/>
        /// is <c>null</c>, no callback function is registered. The parameters to this callback function are:
        /// 
        /// errinfo is a pointer to an error string.
        /// 
        /// private_info and cb represent a pointer to binary data that is returned by the OpenCL implementation that can be used to log additional information helpful in debugging the error.
        /// 
        /// userData is a pointer to user supplied data.
        /// 
        /// Note: There are a number of cases where error notifications need to be delivered due to an error that occurs outside a context. Such notifications may not be delivered through the <see cref="notificationCallback"/> callback.
        /// Where these notifications go is implementation-defined.
        /// </param>
        /// <param name="userData">Passed as the userData argument when <see cref="notificationCallback"/> is called. <see cref="userData"/> can be <c>null</c>.</param>
        /// <param name="errorCode">Returns an appropriate error code. If <see cref="errorCode"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero context and <see cref="errorCode"/> is set to <c>Result.Success</c> if the context is created successfully. Otherwise, it returns a <c>null</c> value with an error value returned in
        /// <see cref="errorCode"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateContext")]
        public static extern IntPtr CreateContext(
            [In] IntPtr[] properties,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfDevices,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [In] IntPtr notificationCallback,
            [In] IntPtr userData,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        /// <summary>
        /// Decrement the context reference count.
        /// </summary>
        /// <param name="context">The context to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid OpenCL context.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseContext")]
        public static extern Result ReleaseContext([In] IntPtr context);

        #endregion
    }
}