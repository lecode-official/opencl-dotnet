
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.EnqueuedCommands
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Enqueued Commands API.
    /// </summary>
    public static class EnqueuedCommandsNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Enqueue commands to read from a buffer object to host memory.
        /// </summary>
        /// <param name="commandQueue">Is a valid host command-queue in which the read command will be queued. commandQueue and buffer must be created with the same OpenCL context.</param>
        /// <param name="buffer">Refers to a valid buffer object.</param>
        /// <param name="blocking_read">Indicates if the read operations are blocking or non-blocking.</param>
        /// <param name="offset">The offset in bytes in the buffer object to read from.</param>
        /// <param name="size">The size in bytes of data being read.</param>
        /// <param name="ptr">The pointer to buffer in host memory where data is to be read into.</param>
        /// <param name="numberOfEventsinWaitList">The number of event in <see cref="eventWaitList"/>. If <see cref="eventWaitList"/> is <c>null</c>, then <see cref="numberOfEventsinWaitList"/ must be 0.</param>
        /// <param name="eventWaitList">
        /// Specify events that need to complete before this particular command can be executed. If <see cref="eventWaitList"/> is <c>null</c>, then this particular command does not wait on any event to complete.
        /// </param>
        /// <param name="waitEvent">
        /// Returns an event object that identifies this particular kernel-instance. Event objects are unique and can be used to identify a particular kernel execution instance later on. If event is <c>null</c>, no event will be created for
        /// this kernel execution instance and therefore it will not be possible for the application to query or queue a wait for this particular kernel execution instance.
        /// </param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clEnqueueReadBuffer")]
        public static extern Result EnqueueReadBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint blocking_read,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] IntPtr ptr,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        /// <summary>
        /// Enqueues a command to execute a kernel on a device.
        /// </summary>
        /// <param name="commandQueue">A valid host command-queue. The kernel will be queued for execution on the device associated with <see cref="commandQueue"/>.</param>
        /// <param name="kernel">A valid kernel object. The OpenCL context associated with <see cref="kernel"/> and <see cref="commandQueue"/> must be the same.</param>
        /// <param name="workDimension">The number of dimensions used to specify the global work-items and work-items in the work-group.</param>
        /// <param name="globalWorkOffset">
        /// Can be used to specify an array of <see cref="workDimension"/> unsigned values that describe the offset used to calculate the global ID of a work-item. If <see cref="globalWorkOffset"/> is <c>null</c>, the global IDs start at
        /// offset (0, 0, ... 0).
        /// </param>
        /// <param name="globalWorkSize">
        /// Points to an array of <see cref="workDimension"/> unsigned values that describe the number of global work-items in <see cref="workDimension"/> dimensions that will execute the kernel function. The total number of global
        /// work-items is computed as globalWorkSize[0] *...* globalWorkSize[workDimension - 1].
        /// </param>
        /// <param name="localWorkSize">
        /// Points to an array of <see cref="workDimension"/> unsigned values that describe the number of work-items that make up a work-group (also referred to as the size of the work-group) that will execute the kernel specified by
        /// <see cref="kernel"/>. The total number of work-items in a work-group is computed as localWorkSize[0] *... * localWorkSize[workDimension - 1].
        /// </param>
        /// <param name="numberOfEventsinWaitList">The number of event in <see cref="eventWaitList"/>. If <see cref="eventWaitList"/> is <c>null</c>, then <see cref="numberOfEventsinWaitList"/ must be 0.</param>
        /// <param name="eventWaitList">
        /// Specify events that need to complete before this particular command can be executed. If <see cref="eventWaitList"/> is <c>null</c>, then this particular command does not wait on any event to complete.
        /// </param>
        /// <param name="waitEvent">
        /// Returns an event object that identifies this particular kernel-instance. Event objects are unique and can be used to identify a particular kernel execution instance later on. If event is <c>null</c>, no event will be created for
        /// this kernel execution instance and therefore it will not be possible for the application to query or queue a wait for this particular kernel execution instance.
        /// </param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clEnqueueNDRangeKernel")]
        public static extern Result EnqueueNDRangeKernel(
            [In] IntPtr commandQueue,
            [In] IntPtr kernel,
            [In] [MarshalAs(UnmanagedType.U4)] uint workDimension,
            [In] IntPtr[] globalWorkOffset,
            [In] IntPtr[] globalWorkSize,
            [In] IntPtr[] localWorkSize,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        #endregion
    }
}