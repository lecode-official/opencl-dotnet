
#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using OpenCl.DotNetCore.Interop;
using OpenCl.DotNetCore.Interop.Contexts;
using OpenCl.DotNetCore.Interop.Memory;
using OpenCl.DotNetCore.Interop.Programs;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL context.
    /// </summary>
    public class Context : HandleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Context"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL context.</param>
        /// <param name="devices">The devices for which the context was created.</param>
        internal Context(IntPtr handle, IEnumerable<Device> devices)
            : base(handle)
        {
            this.Devices = devices;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the devices for which the context was created.
        /// </summary>
        public IEnumerable<Device> Devices { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a program from the provided source codes. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="sources">The source codes from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromString(IEnumerable<string> sources)
        {
            // Loads the program from the specified source string
            Result result;
            IntPtr[] sourceList = sources.Select(source => Marshal.StringToHGlobalAnsi(source)).ToArray();
            uint[] sourceLengths = sources.Select(source => (uint)source.Length).ToArray();
            IntPtr programPointer = ProgramsNativeApi.CreateProgramWithSource(this.Handle, 1, sourceList, sourceLengths, out result);

            // Checks if the program creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The program could not be created.", result);

            // Builds (compiles and links) the program and checks if it was successful, if not, then an exception is thrown
            result = ProgramsNativeApi.BuildProgram(programPointer, 0, null, null, IntPtr.Zero, IntPtr.Zero);
            if (result != Result.Success)
            {
                // Cycles over all devices and retrieves the build log for each one, so that the errors that occurred can be added to the exception message (if any error occur during the retrieval, the exception is thrown without the log)
                Dictionary<string, string> buildLogs = new Dictionary<string, string>();
                foreach (Device device in this.Devices)
                {
                    // Retrieves the size of the return value in bytes, this is used to later get the full information
                    UIntPtr returnValueSize;
                    result = ProgramsNativeApi.GetProgramBuildInformation(programPointer, device.Handle, ProgramBuildInformation.Log, UIntPtr.Zero, null, out returnValueSize);
                    if (result != Result.Success)
                        throw new OpenClException("The program could not be compiled and linked.", result);
                    
                    // Allocates enough memory for the return value and retrieves it
                    byte[] output = new byte[returnValueSize.ToUInt32()];
                    result = ProgramsNativeApi.GetProgramBuildInformation(programPointer, device.Handle, ProgramBuildInformation.Log, new UIntPtr((uint)output.Length), output, out returnValueSize);
                    if (result != Result.Success)
                        throw new OpenClException("The program could not be compiled and linked.", result);

                    // Converts the output to a string, checks if the log is not empty, and adds it to the build logs
                    string buildLog = InteropConverter.To<string>(output).Trim();
                    if (!string.IsNullOrWhiteSpace(buildLog))
                        buildLogs.Add(device.Name, buildLog);
                }

                // Compiles the build logs into a formatted string and integrates it into the exception message
                string buildLogString = string.Join($"{Environment.NewLine}{Environment.NewLine}", buildLogs.Select(keyValuePair => $" Build log for device \"{keyValuePair.Key}\":{Environment.NewLine}{keyValuePair.Value}"));
                throw new OpenClException($"The program could not be compiled and linked.{Environment.NewLine}{Environment.NewLine}{buildLogString}", result);
            }

            // Creates the new program and returns it
            Program program = new Program(programPointer);
            return program;
        }

        /// <summary>
        /// Creates a program from the provided source code. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="source">The source code from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromString(string source) => this.CreateAndBuildProgramFromString(new List<string> { source });

        /// <summary>
        /// Creates a program from the provided source streams. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="streams">The source streams from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromStream(IEnumerable<Stream> streams)
        {
            // Uses a stream reader to read the all streams
            List<string> sourceList = new List<string>();
            foreach (Stream source in streams)
            {
                using (StreamReader stringReader = new StreamReader(source))
                    sourceList.Add(stringReader.ReadToEnd());
            }

            // Compiles the loaded strings
            return this.CreateAndBuildProgramFromString(sourceList);
        }

        /// <summary>
        /// Creates a program from the provided source stream. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="stream">The source stream from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromStream(Stream stream) => this.CreateAndBuildProgramFromStream(new List<Stream> { stream });

        /// <summary>
        /// Creates a program from the provided source files. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="fileNames">The source files from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromFile(IEnumerable<string> fileNames)
        {
            // Loads all the source code files and reads them in
            List<string> sourceList = fileNames.Select(fileName => File.ReadAllText(fileName)).ToList();

            // Compiles and returnes the program
            return this.CreateAndBuildProgramFromString(sourceList);
        }

        /// <summary>
        /// Creates a program from the provided source file. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="fileName">The source file from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromFile(string fileName) => this.CreateAndBuildProgramFromFile(new List<string> { fileName });

        /// <summary>
        /// Creates a new memory object with the specified flags and of the specified size.
        /// </summary>
        /// <param name="memoryFlags">The flags, that determines the how the memory object is created and how it can be accessed.</param>
        /// <param name="size">The size of memory that should be allocated for the memory object.</param>
        /// <exception cref="OpenClException">If the memory object could not be created, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created memory object.</returns>
        public MemoryObject CreateMemoryObject(OpenCl.DotNetCore.MemoryFlag memoryFlags, int size)
        {
            // Creates a new memory object of the specified size and with the specified memory flags
            Result result;
            IntPtr memoryObjectPointer = MemoryNativeApi.CreateBuffer(this.Handle, (Interop.Memory.MemoryFlag)memoryFlags, new UIntPtr((uint)size), IntPtr.Zero, out result);
            
            // Checks if the creation of the memory object was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The memory object could not be created.", result);

            // Creates the memory object from the pointer to the memory object and returns it
            MemoryObject memoryObject = new MemoryObject(memoryObjectPointer);
            return memoryObject;
        }

        /// <summary>
        /// Creates a new memory object with the specified flags. The size of memory allocated for the memory object is determined by <see cref="T"/> and the number of elements.
        /// </summary>
        /// <typeparam name="T">The size of the memory object will be determined by the structure specified in the type parameter.</typeparam>
        /// <param name="memoryFlags">The flags, that determines the how the memory object is created and how it can be accessed.</param>
        /// <exception cref="OpenClException">If the memory object could not be created, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created memory object.</returns>
        public MemoryObject CreateMemoryObject<T>(OpenCl.DotNetCore.MemoryFlag memoryFlags, int size) where T : struct => this.CreateMemoryObject(memoryFlags, Marshal.SizeOf<T>() * size);

        /// <summary>
        /// Creates a new memory object with the specified flags for the specified array. The size of memory 1allocated for the memory object is determined by <see cref="T"/> and the number of elements in the array.
        /// </summary>
        /// <typeparam name="T">The size of the memory object will be determined by the structure specified in the type parameter.</typeparam>
        /// <param name="memoryFlags">The flags, that determines the how the memory object is created and how it can be accessed.</param>
        /// <param name="value">The value that is to be copied over to the device.</param>
        /// <exception cref="OpenClException">If the memory object could not be created, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created memory object.</returns>
        public MemoryObject CreateMemoryObject<T>(OpenCl.DotNetCore.MemoryFlag memoryFlags, T[] value) where T : struct
        {
            // Tries to create the memory object, if anything goes wrong, then it is crucial to free the allocated memory
            IntPtr hostMemoryObjectPointer = IntPtr.Zero;
            try
            {
                // Determines the size of the specified value and creates a pointer that points to the data inside the structure
                int size = Marshal.SizeOf<T>() * value.Length;
                hostMemoryObjectPointer = Marshal.AllocHGlobal(size);
                for (int i = 0; i < value.Length; i++)
                    Marshal.StructureToPtr(value[i], IntPtr.Add(hostMemoryObjectPointer, i * Marshal.SizeOf<T>()), false);

                // Creates a new memory object for the specified value
                Result result;
                IntPtr memoryObjectPointer = MemoryNativeApi.CreateBuffer(this.Handle, (Interop.Memory.MemoryFlag)memoryFlags, new UIntPtr((uint)size), hostMemoryObjectPointer, out result);

                // Checks if the creation of the memory object was successful, if not, then an exception is thrown
                if (result != Result.Success)
                    throw new OpenClException("The memory object could not be created.", result);

                // Creates the memory object from the pointer to the memory object and returns it
                MemoryObject memoryObject = new MemoryObject(memoryObjectPointer);
                return memoryObject;
            }
            finally
            {
                // Deallocates the host memory allocated for the value
                if (hostMemoryObjectPointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(hostMemoryObjectPointer);
            }
        }

        #endregion
        
        #region Public Static Methods

        /// <summary>
        /// Creates a new context for the specified device.
        /// </summary>
        /// <param name="device">The device for which the context is to be created.</param>
        /// <exception cref="OpenClException">If the context could not be created, then an <see cref="OpenClException"/> exception is thrown.</exception>
        /// <returns>Returns the created context.</returns>
        public static Context CreateContext(Device device) => Context.CreateContext(new List<Device> { device });

        /// <summary>
        /// Creates a new context for the specified device.
        /// </summary>
        /// <param name="devices">The devices for which the context is to be created.</param>
        /// <exception cref="OpenClException">If the context could not be created, then an <see cref="OpenClException"/> exception is thrown.</exception>
        /// <returns>Returns the created context.</returns>
        public static Context CreateContext(IEnumerable<Device> devices)
        {
            // Creates the new context for the specified devices
            Result result;
            IntPtr contextPointer = ContextsNativeApi.CreateContext(null, (uint)devices.Count(), devices.Select(device => device.Handle).ToArray(), IntPtr.Zero, IntPtr.Zero, out result);

            // Checks if the device creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The context could not be created.", result);

            // Creates the new context object from the pointer and returns it
            return new Context(contextPointer, devices);
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the resources that have been acquired by the context.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected override void Dispose(bool disposing)
        {
            // Checks if the context has already been disposed of, if not, then the context is disposed of
            if (!this.IsDisposed)
                ContextsNativeApi.ReleaseContext(this.Handle);

            // Makes sure that the base class can execute its dispose logic
            base.Dispose(disposing);
        }

        #endregion
    }
}