
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
            [In] [MarshalAs(UnmanagedType.U4)] uint blockingRead,
            [In] UIntPtr offset,
            [In] UIntPtr size,
            [In] IntPtr pointer,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
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
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEventsinWaitList,
            [In] IntPtr[] eventWaitList,
            [Out] out IntPtr waitEvent
        );
        
        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueWriteImage(cl_command_queue    /* command_queue */,
        //                    cl_mem              /* image */,
        //                    cl_bool             /* blocking_write */, 
        //                    const size_t *      /* origin[3] */,
        //                    const size_t *      /* region[3] */,
        //                    size_t              /* input_row_pitch */,
        //                    size_t              /* input_slice_pitch */, 
        //                    const void *        /* ptr */,
        //                    cl_uint             /* num_events_in_wait_list */,
        //                    const cl_event *    /* event_wait_list */,
        //                    cl_event *          /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueFillImage(cl_command_queue   /* command_queue */,
        //                cl_mem             /* image */, 
        //                const void *       /* fill_color */, 
        //                const size_t *     /* origin[3] */, 
        //                const size_t *     /* region[3] */, 
        //                cl_uint            /* num_events_in_wait_list */, 
        //                const cl_event *   /* event_wait_list */, 
        //                cl_event *         /* event */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyImage(cl_command_queue     /* command_queue */,
        //                cl_mem               /* src_image */,
        //                cl_mem               /* dst_image */, 
        //                const size_t *       /* src_origin[3] */,
        //                const size_t *       /* dst_origin[3] */,
        //                const size_t *       /* region[3] */, 
        //                cl_uint              /* num_events_in_wait_list */,
        //                const cl_event *     /* event_wait_list */,
        //                cl_event *           /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyImageToBuffer(cl_command_queue /* command_queue */,
        //                        cl_mem           /* src_image */,
        //                        cl_mem           /* dst_buffer */, 
        //                        const size_t *   /* src_origin[3] */,
        //                        const size_t *   /* region[3] */, 
        //                        size_t           /* dst_offset */,
        //                        cl_uint          /* num_events_in_wait_list */,
        //                        const cl_event * /* event_wait_list */,
        //                        cl_event *       /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueCopyBufferToImage(cl_command_queue /* command_queue */,
        //                        cl_mem           /* src_buffer */,
        //                        cl_mem           /* dst_image */, 
        //                        size_t           /* src_offset */,
        //                        const size_t *   /* dst_origin[3] */,
        //                        const size_t *   /* region[3] */, 
        //                        cl_uint          /* num_events_in_wait_list */,
        //                        const cl_event * /* event_wait_list */,
        //                        cl_event *       /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY void * CL_API_CALL
        //clEnqueueMapBuffer(cl_command_queue /* command_queue */,
        //                cl_mem           /* buffer */,
        //                cl_bool          /* blocking_map */, 
        //                cl_map_flags     /* map_flags */,
        //                size_t           /* offset */,
        //                size_t           /* size */,
        //                cl_uint          /* num_events_in_wait_list */,
        //                const cl_event * /* event_wait_list */,
        //                cl_event *       /* event */,
        //                cl_int *         /* errcode_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY void * CL_API_CALL
        //clEnqueueMapImage(cl_command_queue  /* command_queue */,
        //                cl_mem            /* image */, 
        //                cl_bool           /* blocking_map */, 
        //                cl_map_flags      /* map_flags */, 
        //                const size_t *    /* origin[3] */,
        //                const size_t *    /* region[3] */,
        //                size_t *          /* image_row_pitch */,
        //                size_t *          /* image_slice_pitch */,
        //                cl_uint           /* num_events_in_wait_list */,
        //                const cl_event *  /* event_wait_list */,
        //                cl_event *        /* event */,
        //                cl_int *          /* errcode_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueUnmapMemObject(cl_command_queue /* command_queue */,
        //                        cl_mem           /* memobj */,
        //                        void *           /* mapped_ptr */,
        //                        cl_uint          /* num_events_in_wait_list */,
        //                        const cl_event *  /* event_wait_list */,
        //                        cl_event *        /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueMigrateMemObjects(cl_command_queue       /* command_queue */,
        //                        cl_uint                /* num_mem_objects */,
        //                        const cl_mem *         /* mem_objects */,
        //                        cl_mem_migration_flags /* flags */,
        //                        cl_uint                /* num_events_in_wait_list */,
        //                        const cl_event *       /* event_wait_list */,
        //                        cl_event *             /* event */) CL_API_SUFFIX__VERSION_1_2;

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

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueNativeKernel(cl_command_queue  /* command_queue */,
        //                    void (CL_CALLBACK * /*user_func*/)(void *), 
        //                    void *            /* args */,
        //                    size_t            /* cb_args */, 
        //                    cl_uint           /* num_mem_objects */,
        //                    const cl_mem *    /* mem_list */,
        //                    const void **     /* args_mem_loc */,
        //                    cl_uint           /* num_events_in_wait_list */,
        //                    const cl_event *  /* event_wait_list */,
        //                    cl_event *        /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueMarkerWithWaitList(cl_command_queue  /* command_queue */,
        //                            cl_uint           /* num_events_in_wait_list */,
        //                            const cl_event *  /* event_wait_list */,
        //                            cl_event *        /* event */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueBarrierWithWaitList(cl_command_queue  /* command_queue */,
        //                            cl_uint           /* num_events_in_wait_list */,
        //                            const cl_event *  /* event_wait_list */,
        //                            cl_event *        /* event */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMFree(cl_command_queue  /* command_queue */,
        //                cl_uint           /* num_svm_pointers */,
        //                void *[]          /* svm_pointers[] */,
        //                void (CL_CALLBACK * /*pfn_free_func*/)(cl_command_queue /* queue */,
        //                                                        cl_uint          /* num_svm_pointers */,
        //                                                        void *[]         /* svm_pointers[] */,
        //                                                        void *           /* user_data */),
        //                void *            /* user_data */,
        //                cl_uint           /* num_events_in_wait_list */,
        //                const cl_event *  /* event_wait_list */,
        //                cl_event *        /* event */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMMemcpy(cl_command_queue  /* command_queue */,
        //                cl_bool           /* blocking_copy */,
        //                void *            /* dst_ptr */,
        //                const void *      /* src_ptr */,
        //                size_t            /* size */,
        //                cl_uint           /* num_events_in_wait_list */,
        //                const cl_event *  /* event_wait_list */,
        //                cl_event *        /* event */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMMemFill(cl_command_queue  /* command_queue */,
        //                    void *            /* svm_ptr */,
        //                    const void *      /* pattern */,
        //                    size_t            /* pattern_size */,
        //                    size_t            /* size */,
        //                    cl_uint           /* num_events_in_wait_list */,
        //                    const cl_event *  /* event_wait_list */,
        //                    cl_event *        /* event */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMMap(cl_command_queue  /* command_queue */,
        //                cl_bool           /* blocking_map */,
        //                cl_map_flags      /* flags */,
        //                void *            /* svm_ptr */,
        //                size_t            /* size */,
        //                cl_uint           /* num_events_in_wait_list */,
        //                const cl_event *  /* event_wait_list */,
        //                cl_event *        /* event */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMUnmap(cl_command_queue  /* command_queue */,
        //                void *            /* svm_ptr */,
        //                cl_uint           /* num_events_in_wait_list */,
        //                const cl_event *  /* event_wait_list */,
        //                cl_event *        /* event */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clEnqueueSVMMigrateMem(cl_command_queue         /* command_queue */,
        //                    cl_uint                  /* num_svm_pointers */,
        //                    const void **            /* svm_pointers */,
        //                    const size_t *           /* sizes */,
        //                    cl_mem_migration_flags   /* flags */,
        //                    cl_uint                  /* num_events_in_wait_list */,
        //                    const cl_event *         /* event_wait_list */,
        //                    cl_event *               /* event */) CL_API_SUFFIX__VERSION_2_1;

        #endregion

        #region Public Deprecated Methods

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueMarker(cl_command_queue    /* command_queue */,
        //                cl_event *          /* event */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;
            
        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueWaitForEvents(cl_command_queue /* command_queue */,
        //                        cl_uint          /* num_events */,
        //                        const cl_event * /* event_list */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_int CL_API_CALL
        //clEnqueueBarrier(cl_command_queue /* command_queue */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_2_DEPRECATED cl_int CL_API_CALL
        //clEnqueueTask(cl_command_queue  /* command_queue */,
        //            cl_kernel         /* kernel */,
        //            cl_uint           /* num_events_in_wait_list */,
        //            const cl_event *  /* event_wait_list */,
        //            cl_event *        /* event */) CL_EXT_SUFFIX__VERSION_1_2_DEPRECATED;

        #endregion
    }
}