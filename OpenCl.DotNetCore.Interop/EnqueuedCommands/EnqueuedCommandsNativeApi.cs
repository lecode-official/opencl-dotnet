
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
        /// <param name="blockingRead">Indicates if the read operations are blocking or non-blocking.</param>
        /// <param name="offset">The offset in bytes in the buffer object to read from.</param>
        /// <param name="size">The size in bytes of data being read.</param>
        /// <param name="pointer">The pointer to buffer in host memory where data is to be read into.</param>
        /// <param name="numberOfEventsInWaitList">The number of event in <see cref="eventWaitList"/>. If <see cref="eventWaitList"/> is <c>null</c>, then <see cref="numberOfEventsInWaitList"/ must be 0.</param>
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
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingRead,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueReadBufferRect")]
        public static extern Result EnqueueREadBufferRectangle(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingRead,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] bufferOffset,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] hostOffset,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr bufferRowPitch,
            [In] UIntPtr bufferSlicePitch,
            [In] UIntPtr hostRowPitch,
            [In] UIntPtr hostSlicePitch,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueWriteBuffer")]
        public static extern Result EnqueueWriteBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingWrite,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );
        
        [DllImport("OpenCL", EntryPoint = "clEnqueueWriteBufferRect")]
        public static extern Result EnqueueWriteBufferRectangle(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingWrite,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] bufferOffset,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] hostOffset,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr bufferRowPitch,
            [In] UIntPtr bufferSlicePitch,
            [In] UIntPtr hostRowPitch,
            [In] UIntPtr hostSlicePitch,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueFillBuffer")]
        public static extern Result EnqueueFillBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] IntPtr pattern,
            [In] UIntPtr patternSize,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueCopyBuffer")]
        public static extern Result EnqueueCopyBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr sourceBuffer,
            [In] IntPtr destinationBuffer,
            [In] UIntPtr sourceOffset,
            [In] UIntPtr destinationOffset,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueCopyBufferRect")]
        public static extern Result EnqueueCopyBufferRectangle(
            [In] IntPtr commandQueue,
            [In] IntPtr sourceBuffer,
            [In] IntPtr destinationBuffer,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] sourceOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] destinationOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr sourceRowPitch,
            [In] UIntPtr sourceSlicePitch,
            [In] UIntPtr destinationRowPitch,
            [In] UIntPtr destinationSlicePitch,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueReadImage")]
        public static extern Result EnqueueReadImage(
            [In] IntPtr commandQueue,
            [In] IntPtr image,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingRead,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] origin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr rowPitch,
            [In] UIntPtr slicePitch,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueWriteImage")]
        public static extern Result EnqueueWriteImage(
            [In] IntPtr commandQueue,
            [In] IntPtr image,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingWrite,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] origin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr inputRowPitch,
            [In] UIntPtr inputSlicePitch,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueFillImage")]
        public static extern Result EnqueueFillImage(
            [In] IntPtr commandQueue,
            [In] IntPtr image,
            [In] IntPtr fillColor,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] origin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueCopyImage")]
        public static extern Result EnqueueCopyImage(
            [In] IntPtr commandQueue,
            [In] IntPtr sourceImage,
            [In] IntPtr destinationImage,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] sourceOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] destinationOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueCopyImageToBuffer")]
        public static extern Result EnqueueCopyImageToBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr sourceImage,
            [In] IntPtr destinationBuffer,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] sourceOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr destinationOffset,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueCopyBufferToImage")]
        public static extern Result EnqueueCopyBufferToImage(
            [In] IntPtr commandQueue,
            [In] IntPtr sourceBuffer,
            [In] IntPtr destinationImage,
            [In] UIntPtr sourceOffset,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] destinationOrigin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueMapBuffer")]
        public static extern IntPtr EnqueueMapBuffer(
            [In] IntPtr commandQueue,
            [In] IntPtr buffer,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingMap,
            [In] [MarshalAs(UnmanagedType.U8)] MapFlags mapFlags,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueMapImage")]
        public static extern IntPtr EnqueueMapImage(
            [In] IntPtr commandQueue,
            [In] IntPtr image,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingMap,
            [In] [MarshalAs(UnmanagedType.U8)] MapFlags mapFlags,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] origin,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] region,
            [In] UIntPtr imageRowPitch,
            [In] UIntPtr imageSlicePitch,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueUnmapMemObject")]
        public static extern Result EnqueueUnmapMemoryObject(
            [In] IntPtr commandQueue,
            [In] IntPtr memoryObject,
            [In] IntPtr mappedPointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueMigrateMemObjects")]
        public static extern Result EnqueueMigrateMemorysObjects(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfMemoryObjects,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] memoryObjects,
            [In] [MarshalAs(UnmanagedType.U8)] MemoryMigrationFlag memoryMigrationFlags,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
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
        /// <param name="numberOfEventsInWaitList">The number of event in <see cref="eventWaitList"/>. If <see cref="eventWaitList"/> is <c>null</c>, then <see cref="numberOfEventsInWaitList"/ must be 0.</param>
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueNativeKernel")]
        public static extern Result EnqueueNativeKernel(
            [In] IntPtr commandQueue,
            [In] IntPtr userFunction,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] arguments,
            [In] UIntPtr argumentSize,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfMemoryObjects,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] memoryObjects,
            [In] IntPtr argumentsMemoryLocation,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueMarkerWithWaitList")]
        public static extern Result EnqueueMarkerWithWaitList(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueBarrierWithWaitList")]
        public static extern Result EnqueueBarrierWithWaitList(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMFree")]
        public static extern Result EnqueueSvmFree(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfSvmPointers,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svmPointers,
            [In] IntPtr svmFreePointersCallback,
            [In] IntPtr userData,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMMemcpy")]
        public static extern Result EnqueuesSvmMemoryCopy(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingCopy,
            [In] IntPtr destinationPointer,
            [In] IntPtr sourcePointer,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMMemFill")]
        public static extern Result EnqueueSvmMemoryFill(
            [In] IntPtr commandQueue,
            [In] IntPtr svmPointer,
            [In] IntPtr pattern,
            [In] UIntPtr patternSize,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMMap")]
        public static extern Result EnqueueSvmMap(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingMap,
            [In] [MarshalAs(UnmanagedType.U8)] MapFlags mapFlags,
            [In] IntPtr svmPointer,
            [In] UIntPtr size,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMUnmap")]
        public static extern Result EnqueueSvmUnmap(
            [In] IntPtr commandQueue,
            [In] IntPtr svmPointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueSVMMigrateMem")]
        public static extern Result EnqueueSvmMigrateMemory(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfSvmPointers,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] svmPointers,
            [In] [MarshalAs(UnmanagedType.LPArray)] UIntPtr[] sizes,
            [In] [MarshalAs(UnmanagedType.U8)] MemoryMigrationFlag memoryMigrationFlags,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        #endregion

        #region Public Deprecated Methods

        [DllImport("OpenCL", EntryPoint = "clEnqueueMarker")]
        [Obsolete("This is a deprecated OpenCL 1.1 method, please use EnqueueMarkerWithWaitList instead.")]
        public static extern Result EnqueueMarker(
            [In] IntPtr commandQueue,
            [In] IntPtr waitEvent
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueWaitForEvents")]
        [Obsolete("This is a deprecated OpenCL 1.1 method, please use EnqueueMarkerWithWaitList instead.")]
        public static extern Result EnqueueWaitForEvents(
            [In] IntPtr commandQueue,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueBarrier")]
        [Obsolete("This is a deprecated OpenCL 1.1 method, please use EnqueueBarrierWithWaitList instead.")]
        public static extern Result EnqueueBarrier(
            [In] IntPtr commandQueue
        );

        [DllImport("OpenCL", EntryPoint = "clEnqueueTask")]
        [Obsolete("This is a deprecated OpenCL 1.2 method.")]
        public static extern Result EnqueueEnqueueTaskBarrier(
            [In] IntPtr commandQueue,
            [In] IntPtr kernel,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsInWaitList,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );

        #endregion
    }
}