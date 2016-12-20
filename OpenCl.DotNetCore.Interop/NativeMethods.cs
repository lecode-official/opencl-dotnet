
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop
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
        /// <param name="numberOfEntries">The number of platform entries that can be added to <see cref="platforms"/>. If <see cref="platforms"/> is not <c>null</c>, the <see cref="numberOfEntries"/> must be greater than zero.</param>
        /// <param name="platforms">
        /// Returns a list of OpenCL platforms found. The platform values returned in <see cref="platforms"/> can be used to identify a specific OpenCL platform. If <see cref="platforms"/> argument is <c>null</c>, this argument is ignored.
        /// The number of OpenCL platforms returned is the mininum of the value specified by <see cref="numberOfEntries"/> or the number of OpenCL platforms available.
        /// </param>
        /// <param name="numberOfPlatforms">Returns the number of OpenCL platforms available. If <see cref="numberOfPlatforms"/> is <c>null</c>, this argument is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. If the cl_khr_icd extension is enabled, <see cref="GetPlatformIds"/> returns <c>Result.Success</c> if the function is executed successfully and there are a
        /// non zero number of platforms available. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="numberOfEntries"/> is equal to zero and <see cref="platforms"/> is not <c>null</c>, or if both <see cref="numberOfPlatforms"/> and <see cref="platforms"/> are <c>null</c>.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// 
        /// <c>Result.PlatformNotFoundKhr</c> if the cl_khr_icd extension is enabled and no platforms are found.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformIDs")]
        public static extern Result GetPlatformIds(
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEntries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint numberOfPlatforms
        );

        /// <summary>
        /// Get specific information about the OpenCL platform.
        /// </summary>
        /// <param name="platform">The platform ID returned by <see cref="GetPlatformIds"/> or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>, the behavior is implementation-defined.</param>
        /// <param name="parameterName">An enumeration constant that identifies the platform information being queried.</param>
        /// <param name="parameterValueSize">Specifies the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size in bytes must be greater than or equal to size of return type specified above.</param>
        /// <param name="parameterValue">A pointer to memory location where appropriate values for a given <see cref="parameterValue"/> will be returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">Returns the actual size in bytes of data being queried by <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following: (The OpenCL specification does not describe the order of precedence for error codes returned by API calls)
        /// 
        /// <c>Result.InvalidPlatform</c> if platform is not a valid platform.!--
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/ is
        /// not a <c>null</c> value.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformInfo")]
        public static extern Result GetPlatformInformation(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] PlatformInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        #endregion

        #region Device API Methods

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

        #endregion

        #region Context API Methods

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
        /// Builds (compiles and links) a program executable from the program source or binary.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="numberOfDevices">The number of devices listed in <see cref="deviceList"/>.</param>
        /// <param name="deviceList">
        /// A pointer to a list of devices associated with <see cref="program"/>. If <see cref="deviceList"/> is a <c>null</c> value, the program executable is built for all devices associated with <see cref="program"/> for which a source
        /// or binary has been loaded. If <see cref="deviceList"/> is a non-<c>null</c> value, the program executable is built for devices specified in this list for which a source or binary has been loaded.
        /// </param>
        /// <param name="options">A pointer to a null-terminated string of characters that describes the build options to be used for building the program executable. Certain options are ignored when program is created with IL.</param>
        /// <param name="notificationCallback">
        /// A function pointer to a notification routine. The notification routine is a callback function that an application can register and which will be called when the program executable has been built (successfully or unsuccessfully).
        /// If <see cref="notificationCallback"/> is not <c>null</c>, <see cref="BuildProgram"/> does not need to wait for the build to complete and can return immediately once the build operation can begin. The build operation can begin if
        /// the context, program whose sources are being compiled and linked, list of devices and build options specified are all valid and appropriate host and device resources needed to perform the build are available. If
        /// <see cref="notificationCallback"/> is <c>null</c>, <see cref="BuildProgram"/> does not return until the build has completed. This callback function may be called asynchronously by the OpenCL implementation. It is the
        /// application’s responsibility to ensure that the callback function is thread-safe.
        /// </param>
        /// <param name="userData">Passed as an argument when <see cref="notificationCallback"/> is called. <see cref="userData"/> can be <c>null</c>.</param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clBuildProgram")]
        public static extern Result BuildProgram(
            [In] IntPtr program,
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfDevices,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] deviceList,
            [In] [MarshalAs(UnmanagedType.LPStr)] string options,
            [In] IntPtr notificationCallback,
            [In] IntPtr userData
        );

        /// <summary>
        /// Decrements the program reference count.
        /// </summary>
        /// <param name="program">The program to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseProgram")]
        public static extern Result ReleaseProgram([In] IntPtr program);

        #endregion

        #region Program Object API Methods

        /// <summary>
        /// Creates a program object for a context, and loads the source code specified by the text strings in the <see cref="strings"/> array into the program object.
        /// </summary>
        /// <param name="context">Must be a valid OpenCL context.</param>
        /// <param name="count">The number of source code strings that are provided.</param>
        /// <param name="strings">An array of <see cref="count"/> pointers to optionally null-terminated character strings that make up the source code.</param>
        /// <param name="lengths">
        /// An array with the number of chars in each string (the string length). If an element in <see cref="lengths"/> is zero, its accompanying string is null-terminated. If lengths is <c>null</c>, all strings in the strings argument are
        /// considered null-terminated. Any length value passed in that is greater than zero excludes the null terminator in its count.
        /// </param>
        /// <param name="errorCode">Returns an appropriate error code. If errorCode is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero program object and <see cref="errorCode"/> is set to <c>Result.Success</c> if the program object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errorCode"/>:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="context"/> is not a valid context.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="count"/> is zero or if strings or any entry in strings is <c>null</c>.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateProgramWithSource")]
        public static extern IntPtr CreateProgramWithSource(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U4)] uint count,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] strings,
            [In] [MarshalAs(UnmanagedType.LPArray)] uint[] lengths,
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

        /// <summary>
        /// Returns build information for each device in the program object.
        /// </summary>
        /// <param name="program">Specifies the program object being queried.</param>
        /// <param name="device">Specifies the device for which build information is being queried. <see cref="device"/> must be a valid device associated with <see cref="program"/>.</param>
        /// <param name="parameterName">Specifies the information to query.</param>
        /// <param name="parameterValueSize">Used to specify the size in bytes of memory pointed to by <see cref="parameterValue"/>. This size must be greater or equal to the size of the return type.</param>
        /// <param name="parameterValue">A pointer to memory where the appropriate result being queried is returned. If <see cref="parameterValue"/> is <c>null</c>, it is ignored.</param>
        /// <param name="parameterValueSizeReturned">The actual size in bytes of data copied to <see cref="parameterValue"/>. If <see cref="parameterValueSizeReturned"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not a valid device object.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="parameterName"/> is not one of the supported values or if size in bytes specified by <see cref="parameterValueSize"/> is less than size of return type and <see cref="parameterValue"/>
        /// is not a <c>null</c> value or if <see cref="parameterName"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetProgramBuildInfo")]
        public static extern Result GetProgramBuildInformation(
            [In] IntPtr program,
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] ProgramBuildInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        #endregion

        #region Kernel Object API Methods

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

        #region Command Queue API Methods

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
        [DllImport("OpenCL", EntryPoint = "clCreateCommandQueue")]
        public static extern IntPtr CreateCommandQueue(
            [In] IntPtr context,
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U8)] CommandQueueProperty properties,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

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

        #endregion

        #region Memory Object API Methods

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

        #region Enqueued Commands API Methods
        
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