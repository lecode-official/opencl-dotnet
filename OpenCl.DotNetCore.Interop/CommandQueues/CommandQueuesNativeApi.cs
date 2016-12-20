
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.CommandQueues
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Command Queues API.
    /// </summary>
    public static class CommandQueuesNativeApi
    {
        #region Public Static methods

        //extern CL_API_ENTRY cl_command_queue CL_API_CALL
        //clCreateCommandQueueWithProperties(cl_context               /* context */,
        //                                cl_device_id             /* device */,
        //                                const cl_queue_properties *    /* properties */,
        //                                cl_int *                 /* errcode_ret */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainCommandQueue(cl_command_queue /* command_queue */) CL_API_SUFFIX__VERSION_1_0;

        /// <summary>
        /// Decrements the commandQueue reference count.
        /// </summary>
        /// <param name="commandQueue">Specifies the command-queue to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid command queue.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseCommandQueue")]
        public static extern Result ReleaseCommandQueue([In] IntPtr commandQueue);

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetCommandQueueInfo(cl_command_queue      /* command_queue */,
        //                    cl_command_queue_info /* param_name */,
        //                    size_t                /* param_value_size */,
        //                    void *                /* param_value */,
        //                    size_t *              /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clFlush(cl_command_queue /* command_queue */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clFinish(cl_command_queue /* command_queue */) CL_API_SUFFIX__VERSION_1_0;

        #endregion

        #region Deprecated Public methods

        /// <summary>
        /// Create a command-queue on a specific device.
        /// </summary>
        /// <param name="context">Must be a valid OpenCL context.</param>
        /// <param name="device">
        /// Must be a device associated with <see cref="context"/>. It can either be in the list of devices specified when <see cref="context"/> is created using <see cref="CreateContext"/> or have the same device type as the device type
        /// specified when the context is created using <see cref="CreateContextFromType"/>.
        /// </param>
        /// <param name="properties">
        /// Specifies a list of properties for the command-queue. This is a bit-field described in below. Only command-queue properties specified below can be set in properties; otherwise the value specified in properties is considered to be
        /// not valid.
        /// 
        /// <c>Result.QueueOutOfOrderExecutionModeEnable</c>: Determines whether the commands queued in the command-queue are executed in-order or out-of-order. If set, the commands in the command-queue are executed out-of-order. Otherwise,
        /// commands are executed in-order.
        /// 
        /// <c>Result.QueueProfilingEnable</c>: Enable or disable profiling of commands in the command-queue. If set, the profiling of commands is enabled. Otherwise profiling of commands is disabled.
        /// 
        /// </param>
        /// <param name="errorCode">Returns an appropriate error code. If <see cref="errorCode"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero command-queue and <see cref="errorCode"/> is set to <c>Result.Success</c> if the command-queue is created successfully. Otherwise, it returns a <c>null</c> value with an error values returned in
        /// <see cref="errorCode"/>.
        /// </returns>
        [Obsolete("This is a deprecated OpenCL 1.1 method, please use CreateCommandQueueWithProperties instead.")]
        [DllImport("OpenCL", EntryPoint = "clCreateCommandQueue")]
        public static extern IntPtr CreateCommandQueue(
            [In] IntPtr context,
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U8)] CommandQueueProperty properties,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        #endregion
    }
}