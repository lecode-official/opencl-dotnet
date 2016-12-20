
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

        #endregion
    }
}