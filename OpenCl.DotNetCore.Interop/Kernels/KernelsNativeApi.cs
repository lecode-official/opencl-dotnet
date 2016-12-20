
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Kernels
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Kernels API.
    /// </summary>
    public static class KernelsNativeApi
    {
        #region Public Static Methods

        /// <summary>
        /// Creates a kernel object.
        /// </summary>
        /// <param name="program">A <see cref="program"/> object with a successfully built executable.</param>
        /// <param name="kernelName">A function name in the program declared with the __kernel qualifier.</param>
        /// <param name="errorCode">Returns an appropriate error code. If <see cref="errorCode"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero kernel object and <see cref="errorCode"/> is set to <c>Result.Success</c> if the kernel object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errorCode"/>:
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.InvalidProgramExecutable</c> if there is no successfully built executable for <see cref="program"/>.
        /// 
        /// <c>Result.InvalidKernelName</c> if the function definition for __kernel function given by <see cref="kernelName"/> such as the number of arguments, the argument types are not the same for all devices for which the program
        /// executable has been built.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="kernelName"/> is <c>null</c>.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateKernel")]
        public static extern IntPtr CreateKernel(
            [In] IntPtr program,
            [In] [MarshalAs(UnmanagedType.LPStr)] string kernelName,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        /// <summary>
        /// Decrements the kernel reference count.
        /// </summary>
        /// <param name="kernel">The kernel to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid kernel object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseKernel")]
        public static extern Result ReleaseKernel([In] IntPtr kernel);

        /// <summary>
        /// Set the argument value for a specific argument of a kernel.
        /// </summary>
        /// <param name="kernel">A valid kernel object.</param>
        /// <param name="argumentIndex">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="argumentSize">
        /// Specifies the size of the argument value. If the argument is a memory object, the size is the size of the memory object. For arguments declared with the local qualifier, the size specified will be the size in bytes of the buffer
        /// that must be allocated for the local argument. If the argument is of type sampler_t, the <see cref="argumentSize"/> value must be equal to sizeof(cl_sampler). If the argument is of type queue_t, the <see cref="argumentSize"/>
        /// value must be equal to sizeof(cl_commandQueue). For all other arguments, the size will be the size of argument type.
        /// </param>
        /// <param name="argumentValue">
        /// A pointer to data that should be used as the argument value for argument specified by <see cref="argumentIndex"/>. The argument data pointed to by <see cref="argumentValue"/> is copied and the <see cref="argumentValue"/> pointer
        /// can therefore be reused by the application after <see cref="SetKernelArgument"/> returns. The argument value specified is the value used by all API calls that enqueue kernel (<see cref="EnqueueNDRangeKernel"/>) until the argument
        /// value is changed by a call to <see cref="SetKernelArgument"/> for kernel.
        /// </param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clSetKernelArg")]
        public static extern Result SetKernelArgument(
            [In] IntPtr kernel,
            [In] [MarshalAs(UnmanagedType.U4)] uint argumentIndex,
            [In] UIntPtr argumentSize,
            [In] IntPtr argumentValue
        );

        /// <summary>
        /// Returns information about the kernel object.
        /// </summary>
        /// <param name="kernel">Specifies the kernel object being queried.</param>
        /// <param name="parameterName">Specifies the information to query.</param>
        /// <param name="parameterValueSize">Used to specify the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size must be greater or equal to the size of the return type.</param>
        /// <param name="parameterValue">A pointer to memory where the appropriate result being queried is returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">The actual size in bytes of data copied to <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidKernel</c> if <see cref="kernel"/> is not a valid kernel object.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/>
        /// is not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetKernelInfo")]
        public static extern Result GetKernelInformation(
            [In] IntPtr kernel,
            [In] [MarshalAs(UnmanagedType.U4)] KernelInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        /// <summary>
        /// Returns information about the arguments of a kernel.
        /// </summary>
        /// <param name="kernel">Specifies the kernel object being queried.</param>
        /// <param name="argumentIndex">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="parameterName">Specifies the argument information to query.</param>
        /// <param name="parameterValueSize">Used to specify the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size must be greater or equal to the size of the return type.</param>
        /// <param name="parameterValue">A pointer to memory where the appropriate result being queried is returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">The actual size in bytes of data copied to <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidArgumentIndex</c> if <see cref="argumentIndex"/> is not a valid argument index.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/>
        /// is not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.KernelArgumentInfoNotAvailable</c> if the argument information is not available for <see cref="kernel"/>.
        /// 
        /// <c>Result.InvalidKernel</c> if <see cref="kernel"/> is not a valid kernel object.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetKernelArgInfo")]
        public static extern Result GetKernelArgumentInformation(
            [In] IntPtr kernel,
            [In] [MarshalAs(UnmanagedType.U4)] uint argumentIndex,
            [In] [MarshalAs(UnmanagedType.U4)] KernelArgumentInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        #endregion
    }
}