
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Memory
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Memory API.
    /// </summary>
    public static class MemoryNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Creates a buffer object.
        /// </summary>
        /// <param name="context">A valid OpenCL context used to create the buffer object.</param>
        /// <param name="flags">
        /// A bit-field that is used to specify allocation and usage information such as the memory arena that should be used to allocate the buffer object and how it will be used. If value specified for <see cref="flags"/> is 0, the
        /// default is used which is <see cref="MemoryFlag.ReadWrite"/>.
        /// </param>
        /// <param name="size">The size in bytes of the buffer memory object to be allocated.</param>
        /// <param name="hostPointer">A pointer to the buffer data that may already be allocated by the application. The size of the buffer that <see cref="hostPointer"/> points to must be greater or equal than size bytes.</param>
        /// <param name="errorCode">Returns an appropriate error code. If <see cref="errorCode"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero buffer object and <see cref="errorCode"/> is set to <c>Result.Success</c> if the buffer object is created successfully. Otherwise, it returns a <c>null</c> value and an error value in
        /// <see cref="errorCode"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateBuffer")]
        public static extern IntPtr CreateBuffer(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U8)] MemoryFlag flags,
            [In] UIntPtr size,
            [In] IntPtr hostPointer,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );
        
        //extern CL_API_ENTRY cl_mem CL_API_CALL
        //clCreateSubBuffer(cl_mem                   /* buffer */,
        //                cl_mem_flags             /* flags */,
        //                cl_buffer_create_type    /* buffer_create_type */,
        //                const void *             /* buffer_create_info */,
        //                cl_int *                 /* errcode_ret */) CL_API_SUFFIX__VERSION_1_1;

        //extern CL_API_ENTRY cl_mem CL_API_CALL
        //clCreateImage(cl_context              /* context */,
        //            cl_mem_flags            /* flags */,
        //            const cl_image_format * /* image_format */,
        //            const cl_image_desc *   /* image_desc */, 
        //            void *                  /* host_ptr */,
        //            cl_int *                /* errcode_ret */) CL_API_SUFFIX__VERSION_1_2;

        //extern CL_API_ENTRY cl_mem CL_API_CALL
        //clCreatePipe(cl_context                 /* context */,
        //            cl_mem_flags               /* flags */,
        //            cl_uint                    /* pipe_packet_size */,
        //            cl_uint                    /* pipe_max_packets */,
        //            const cl_pipe_properties * /* properties */,
        //            cl_int *                   /* errcode_ret */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainMemObject(cl_mem /* memobj */) CL_API_SUFFIX__VERSION_1_0;

        /// <summary>
        /// Decrements the memory object reference count.
        /// </summary>
        /// <param name="memoryObject">Specifies the memory object to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="memoryObject"/> is not a valid memory object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseMemObject")]
        public static extern Result ReleaseMemoryObject([In] IntPtr memoryObject);

        /// <summary>
        /// Get information that is common to all memory objects (buffer and image objects).
        /// </summary>
        /// <param name="memoryObject">Specifies the memory object being queried.</param>
        /// <param name="parameterName">Specifies the information to query.</param>
        /// <param name="parameterValueSize">Used to specify the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size must be greater or equal to the size of the return type.</param>
        /// <param name="parameterValue">A pointer to memory where the appropriate result being queried is returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">The actual size in bytes of data copied to <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidMemoryObject</c> if <see cref="memoryObject"/> is not a valid memory object.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/>
        /// is not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetMemObjectInfo")]
        public static extern Result GetMemoryObjectInformation(
            [In] IntPtr memoryObject,
            [In] [MarshalAs(UnmanagedType.U4)] MemoryObjectInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetSupportedImageFormats(cl_context           /* context */,
        //                        cl_mem_flags         /* flags */,
        //                        cl_mem_object_type   /* image_type */,
        //                        cl_uint              /* num_entries */,
        //                        cl_image_format *    /* image_formats */,
        //                        cl_uint *            /* num_image_formats */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetMemObjectInfo(cl_mem           /* memobj */,
        //                cl_mem_info      /* param_name */, 
        //                size_t           /* param_value_size */,
        //                void *           /* param_value */,
        //                size_t *         /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetImageInfo(cl_mem           /* image */,
        //            cl_image_info    /* param_name */, 
        //            size_t           /* param_value_size */,
        //            void *           /* param_value */,
        //            size_t *         /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetPipeInfo(cl_mem           /* pipe */,
        //            cl_pipe_info     /* param_name */,
        //            size_t           /* param_value_size */,
        //            void *           /* param_value */,
        //            size_t *         /* param_value_size_ret */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clSetMemObjectDestructorCallback(cl_mem /* memobj */,
        //                                void (CL_CALLBACK * /*pfn_notify*/)( cl_mem /* memobj */, void* /*user_data*/),
        //                                void * /*user_data */ )             CL_API_SUFFIX__VERSION_1_1;

        #endregion

        #region Deprecated Public Methods

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_mem CL_API_CALL
        //clCreateImage2D(cl_context              /* context */,
        //                cl_mem_flags            /* flags */,
        //                const cl_image_format * /* image_format */,
        //                size_t                  /* image_width */,
        //                size_t                  /* image_height */,
        //                size_t                  /* image_row_pitch */, 
        //                void *                  /* host_ptr */,
        //                cl_int *                /* errcode_ret */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED cl_mem CL_API_CALL
        //clCreateImage3D(cl_context              /* context */,
        //                cl_mem_flags            /* flags */,
        //                const cl_image_format * /* image_format */,
        //                size_t                  /* image_width */, 
        //                size_t                  /* image_height */,
        //                size_t                  /* image_depth */, 
        //                size_t                  /* image_row_pitch */, 
        //                size_t                  /* image_slice_pitch */, 
        //                void *                  /* host_ptr */,
        //                cl_int *                /* errcode_ret */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        #endregion
    }
}