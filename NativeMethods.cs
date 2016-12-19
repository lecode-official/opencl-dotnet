
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
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
        /// <param name="num_entries">The number of platform entries that can be added to <see cref="platforms"/>. If <see cref="platforms"/> is not <c>null</c>, the <see cref="num_entries"/> must be greater than zero.</param>
        /// <param name="platforms">
        /// Returns a list of OpenCL platforms found. The platform values returned in <see cref="platforms"/> can be used to identify a specific OpenCL platform. If <see cref="platforms"/> argument is <c>null</c>, this argument is ignored. The
        /// number of OpenCL platforms returned is the mininum of the value specified by <see cref="num_entries"/> or the number of OpenCL platforms available.
        /// </param>
        /// <param name="num_platforms">Returns the number of OpenCL platforms available. If <see cref="num_platforms"/> is <c>null</c>, this argument is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. If the cl_khr_icd extension is enabled, <see cref="GetPlatformIds"/> returns <c>Result.Success</c> if the function is executed successfully and there are a non
        /// zero number of platforms available. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="platforms"/> is not <c>null</c>, or if both <see cref="num_platforms"/> and <see cref="platforms"/> are <c>null</c>.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// 
        /// <c>Result.PlatformNotFoundKhr</c> if the cl_khr_icd extension is enabled and no platforms are found.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformIDs")]
        public static extern Result GetPlatformIds(
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] platforms,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_platforms
        );

        /// <summary>
        /// Get specific information about the OpenCL platform.
        /// </summary>
        /// <param name="platform">The platform ID returned by <see cref="GetPlatformIds"/> or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>, the behavior is implementation-defined.</param>
        /// <param name="param_name">An enumeration constant that identifies the platform information being queried.</param>
        /// <param name="param_value_size">Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than or equal to size of return type specified above.</param>
        /// <param name="param_value">A pointer to memory location where appropriate values for a given <see cref="param_value"/> will be returned. If <see cref="param_value"/> is <c>null</c>, it is ignored.</param>
        /// <param name="param_value_size_ret">Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following: (The OpenCL specification does not describe the order of precedence for error codes returned by API calls)
        /// 
        /// <c>Result.InvalidPlatform</c> if platform is not a valid platform.!--
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/ is not a
        /// <c>null</c> value.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetPlatformInfo")]
        public static extern Result GetPlatformInfo(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] PlatformInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret
        );

        #endregion

        #region Device API Methods

        /// <summary>
        /// Obtain the list of devices available on a platform.
        /// </summary>
        /// <param name="platform">Refers to the platform ID returned by <see cref="GetPlatformIds"/< or can be <c>null</c>. If <see cref="platform"/> is <c>null</c>, the behavior is implementation-defined.</param>
        /// <param name="device_type">A bitfield that identifies the type of OpenCL device. The <see cref="device_type"/> can be used to query specific OpenCL devices or all OpenCL devices available.</param>
        /// <param name="num_entries">The number of device entries that can be added to <see cref="devices"/>. If <see cref="devices"/> is not <c>null</c>, the <see cref="num_entries"/> must be greater than zero.</param>
        /// <param name="devices">
        /// A list of OpenCL devices found. The device values returned in <see cref="devices"/> can be used to identify a specific OpenCL device. If <see cref="devices"/> argument is <c>null</c>, this argument is ignored. The number of OpenCL
        /// devices returned is the mininum of the value specified by <see cref="num_entries"/> or the number of OpenCL devices whose type matches <see cref="device_type"/>.
        /// </param>
        /// <param name="num_devices">The number of OpenCL devices available that match <see cref="device_type". If <see cref="num_devices"/> is <c>null</c>, this argument is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise it returns one of the following errors:
        /// 
        /// <c>Result.InvalidPlatform</c> if <see cref="platform"/> is not a valid platform.
        /// 
        /// <c>Result.InvalidDeviceType</c> if <see cref="device_type"/> is not a valid value.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="num_entries"/> is equal to zero and <see cref="devices"/> is not <c>null</c> or if both <see cref="num_devices"/> and <see cref="devices"/> are <c>null</c>.
        /// 
        /// <c>Result.DeviceNotFound</c> if no OpenCL devices that matched <see cref="device_type"/> were found.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceIDs")]
        public static extern Result GetDeviceIds(
            [In] IntPtr platform,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceType device_type,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_entries,
            [Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [Out] [MarshalAs(UnmanagedType.U4)] out uint num_devices
        );

        /// <summary>
        /// Get information about an OpenCL device.
        /// </summary>
        /// <param name="device">
        /// A device returned by <see cref="GetDeviceIds"/>. May be a device returned by <see cref="GetDeviceIds"/> or a sub-device created by <see cref="CreateSubDevices"/>. If device is a sub-device, the specific information for the
        /// sub-device will be returned.
        /// </param>
        /// <param name="param_name">An enumeration constant that identifies the device information being queried.</param>
        /// <param name="param_value_size">Specifies the size in bytes of memory pointed to by <see cref="param_value"/>. This size in bytes must be greater than or equal to the size of return type specified.</param>
        /// <param name="param_value">A pointer to memory location where appropriate values for a given <see cref="param_name"/>. If <see cref="param_value"/> is <c>null</c>, it is ignored.</param>
        /// <param name="param_value_size_ret">Returns the actual size in bytes of data being queried by <see cref="param_value"/>. If <see cref="param_value_size_ret"/> is <c>null</c>, it is ignored.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns the following:
        /// 
        /// <c>Result.InvalidDevice</c> if <see cref="device"/> is not valid.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="param_name"/> is not one of the supported values or if size in bytes specified by <see cref="param_value_size"/> is less than size of return type and <see cref="param_value"/> is not a
        /// <c>null</c> value or if <see cref="param_name"/> is a value that is available as an extension and the corresponding extension is not supported by the device.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clGetDeviceInfo")]
        public static extern Result GetDeviceInfo(
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U4)] DeviceInfo param_name,
            [In] IntPtr param_value_size,
            [Out] byte[] param_value,
            [Out] out IntPtr param_value_size_ret
        );

        #endregion

        #region Context API Methods

        /// <summary>
        /// Creates an OpenCL context.
        /// </summary>
        /// <param name="properties">
        /// Specifies a list of context property names and their corresponding values. Each property name is immediately followed by the corresponding desired value. The list is terminated with 0. <see cref="properties"/> can be <c>null</c> in
        /// which case the platform that is selected is implementation-defined.
        /// </param>
        /// <param name="num_devices">The number of devices specified in the <see cref="devices"/> argument.</param>
        /// <param name="devices">
        /// A pointer to a list of unique devices returned by <see cref="GetDeviceIds"/> or sub-devices created by <see cref="CreateSubDevices"/> for a platform. Duplicate devices specified in <see cref="devices"/> are ignored.
        /// </param>
        /// <param name="pfn_notify">
        /// A callback function that can be registered by the application. This callback function will be used by the OpenCL implementation to report information on errors during context creation as well as errors that occur at runtime in this
        /// context. This callback function may be called asynchronously by the OpenCL implementation. It is the application's responsibility to ensure that the callback function is thread-safe. If <see cref="pfn_notify"/> is <c>null</c>, no
        /// callback function is registered. The parameters to this callback function are:
        /// 
        /// errinfo is a pointer to an error string.
        /// 
        /// private_info and cb represent a pointer to binary data that is returned by the OpenCL implementation that can be used to log additional information helpful in debugging the error.
        /// 
        /// user_data is a pointer to user supplied data.
        /// 
        /// Note: There are a number of cases where error notifications need to be delivered due to an error that occurs outside a context. Such notifications may not be delivered through the <see cref="pfn_notify"/> callback. Where these
        /// notifications go is implementation-defined.
        /// </param>
        /// <param name="user_data">Passed as the user_data argument when <see cref="pfn_notify"/> is called. <see cref="user_data"/> can be <c>null</c>.</param>
        /// <param name="errcode_ret">Returns an appropriate error code. If <see cref="errcode_ret"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero context and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the context is created successfully. Otherwise, it returns a <c>null</c> value with an error value returned in
        /// <see cref="errcode_ret"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateContext")]
        public static extern IntPtr CreateContext(
            [In] IntPtr[] properties,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_devices,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] devices,
            [In] IntPtr pfn_notify,
            [In] IntPtr user_data,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
        );

        /// <summary>
        /// Builds (compiles and links) a program executable from the program source or binary.
        /// </summary>
        /// <param name="program">The program object.</param>
        /// <param name="num_devices">The number of devices listed in <see cref="device_list"/>.</param>
        /// <param name="device_list">
        /// A pointer to a list of devices associated with <see cref="program"/>. If <see cref="device_list"/> is a <c>null</c> value, the program executable is built for all devices associated with <see cref="program"/> for which a source or
        /// binary has been loaded. If <see cref="device_list"/> is a non-<c>null</c> value, the program executable is built for devices specified in this list for which a source or binary has been loaded.
        /// </param>
        /// <param name="options">A pointer to a null-terminated string of characters that describes the build options to be used for building the program executable. Certain options are ignored when program is created with IL.</param>
        /// <param name="pfn_notify">
        /// A function pointer to a notification routine. The notification routine is a callback function that an application can register and which will be called when the program executable has been built (successfully or unsuccessfully). If
        /// <see cref="pfn_notify"/> is not <c>null</c>, <see cref="BuildProgram"/> does not need to wait for the build to complete and can return immediately once the build operation can begin. The build operation can begin if the context,
        /// program whose sources are being compiled and linked, list of devices and build options specified are all valid and appropriate host and device resources needed to perform the build are available. If <see cref="pfn_notify"/> is
        /// <c>null</c>, <see cref="BuildProgram"/> does not return until the build has completed. This callback function may be called asynchronously by the OpenCL implementation. It is the application’s responsibility to ensure that the
        /// callback function is thread-safe.
        /// </param>
        /// <param name="user_data">Passed as an argument when <see cref="pfn_notify"/> is called. <see cref="user_data"/> can be <c>null</c>.</param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clBuildProgram")]
        public static extern Result BuildProgram(
            [In] IntPtr program,
            [In] [MarshalAs(UnmanagedType.U4)] uint num_devices,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] device_list,
            [In] [MarshalAs(UnmanagedType.LPStr)] string options,
            [In] IntPtr pfn_notify,
            [In] IntPtr user_data
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
        /// <param name="errcode_ret">Returns an appropriate error code. If errcode_ret is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero program object and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the program object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errcode_ret"/>:
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
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
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

        #region Kernel Object API Methods

        /// <summary>
        /// Creates a kernel object.
        /// </summary>
        /// <param name="program">A <see cref="program"/> object with a successfully built executable.</param>
        /// <param name="kernel_name">A function name in the program declared with the __kernel qualifier.</param>
        /// <param name="errcode_ret">Returns an appropriate error code. If <see cref="errcode_ret"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero kernel object and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the kernel object is created successfully. Otherwise, it returns a <c>null</c> value with one of the following error values
        /// returned in <see cref="errcode_ret"/>:
        /// 
        /// <c>Result.InvalidProgram</c> if <see cref="program"/> is not a valid program object.
        /// 
        /// <c>Result.InvalidProgramExecutable</c> if there is no successfully built executable for <see cref="program"/>.
        /// 
        /// <c>Result.InvalidKernelName</c> if the function definition for __kernel function given by <see cref="kernel_name"/> such as the number of arguments, the argument types are not the same for all devices for which the program
        /// executable has been built.
        /// 
        /// <c>Result.InvalidValue</c> if <see cref="kernel_name"/> is <c>null</c>.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateKernel")]
        public static extern IntPtr CreateKernel(
            [In] IntPtr program,
            [In] [MarshalAs(UnmanagedType.LPStr)] string kernel_name,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
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
        /// <param name="arg_index">The argument index. Arguments to the kernel are referred by indices that go from 0 for the leftmost argument to n - 1, where n is the total number of arguments declared by a kernel.</param>
        /// <param name="arg_size">
        /// Specifies the size of the argument value. If the argument is a memory object, the size is the size of the memory object. For arguments declared with the local qualifier, the size specified will be the size in bytes of the buffer
        /// that must be allocated for the local argument. If the argument is of type sampler_t, the <see cref="arg_size"/> value must be equal to sizeof(cl_sampler). If the argument is of type queue_t, the <see cref="arg_size"/> value must
        /// be equal to sizeof(cl_command_queue). For all other arguments, the size will be the size of argument type.
        /// </param>
        /// <param name="arg_value">
        /// A pointer to data that should be used as the argument value for argument specified by <see cref="arg_index"/>. The argument data pointed to by <see cref="arg_value"/> is copied and the <see cref="arg_value"/> pointer can therefore
        /// be reused by the application after <see cref="SetKernelArgument"/> returns. The argument value specified is the value used by all API calls that enqueue kernel (<see cref="EnqueueNDRangeKernel"/>) until the argument value is changed
        /// by a call to <see cref="SetKernelArgument"/> for kernel.
        /// </param>
        /// <returns>Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns an error.</returns>
        [DllImport("OpenCL", EntryPoint = "clSetKernelArg")]
        public static extern Result SetKernelArgument(
            [In] IntPtr kernel,
            [In] [MarshalAsAttribute(UnmanagedType.U4)] uint arg_index,
            [In] IntPtr arg_size,
            [In] IntPtr arg_value
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
        /// <param name="errcode_ret">Returns an appropriate error code. If <see cref="errcode_ret"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero command-queue and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the command-queue is created successfully. Otherwise, it returns a <c>null</c> value with an error values returned in
        /// <see cref="errcode_ret"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateCommandQueue")]
        public static extern IntPtr CreateCommandQueue(
            [In] IntPtr context,
            [In] IntPtr device,
            [In] [MarshalAs(UnmanagedType.U8)] CommandQueueProperty properties,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
        );

        /// <summary>
        /// Decrements the command_queue reference count.
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
        /// A bit-field that is used to specify allocation and usage information such as the memory arena that should be used to allocate the buffer object and how it will be used. If value specified for <see cref="flags"/> is 0, the default is
        /// used which is <see cref="MemoryFlag.ReadWrite"/>.
        /// </param>
        /// <param name="size">The size in bytes of the buffer memory object to be allocated.</param>
        /// <param name="host_ptr">A pointer to the buffer data that may already be allocated by the application. The size of the buffer that <see cref="host_ptr"/> points to must be greater or equal than size bytes.</param>
        /// <param name="errcode_ret">Returns an appropriate error code. If <see cref="errcode_ret"/> is <c>null</c>, no error code is returned.</param>
        /// <returns>
        /// Returns a valid non-zero buffer object and <see cref="errcode_ret"/> is set to <c>Result.Success</c> if the buffer object is created successfully. Otherwise, it returns a <c>null</c> value and an error value in
        /// <see cref="errcode_ret"/>.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clCreateBuffer")]
        public static extern IntPtr CreateBuffer(
            [In] IntPtr context,
            [In] [MarshalAs(UnmanagedType.U8)] MemoryFlag flags,
            [In] IntPtr size,
            [Out] IntPtr host_ptr,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errcode_ret
        );

        /// <summary>
        /// Decrements the memory object reference count.
        /// </summary>
        /// <param name="memobj">Specifies the memory object to release.</param>
        /// <returns>
        /// Returns <c>Result.Success</c> if the function is executed successfully. Otherwise, it returns one of the following errors:
        /// 
        /// <c>Result.InvalidContext</c> if <see cref="memobj"/> is not a valid memory object.
        /// 
        /// <c>Result.OutOfResources</c> if there is a failure to allocate resources required by the OpenCL implementation on the device.
        /// 
        /// <c>Result.OutOfHostMemory</c> if there is a failure to allocate resources required by the OpenCL implementation on the host.
        /// </returns>
        [DllImport("OpenCL", EntryPoint = "clReleaseMemObject")]
        public static extern Result ReleaseMemoryObject([In] IntPtr memobj);

        #endregion
    }
}