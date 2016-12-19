
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL context.
    /// </summary>
    public class Context : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Context"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL context.</param>
        private Context(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL context.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a program from the provided source code. The program is created, compiled, and linked.
        /// </summary>
        /// <param name="source">The source code from which the program is to be created.</param>
        /// <exception cref="OpenClException">If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created program.</returns>
        public Program CreateAndBuildProgramFromString(string source)
        {
            // Loads the program from the specified source string
            Result result;
            IntPtr[] sourceList = new IntPtr[] { Marshal.StringToHGlobalAnsi(source) };
            uint[] sourceLengths = new uint[] { (uint)source.Length };
            IntPtr programPointer = NativeMethods.CreateProgramWithSource(
                this.Handle,
                1,
                sourceList,
                sourceLengths,
                out result
            );

            // Checks if the program creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The program could not be created.", result);

            // Creates the new program
            Program program = new Program(programPointer);

            // Builds (compiles and links) the program and checks if it was successful, if not, then an exception is thrown
            result = NativeMethods.BuildProgram(program.Handle, 0, null, null, IntPtr.Zero, IntPtr.Zero);
            if (result != Result.Success)
                throw new OpenClException("The program could not be compiled and linked.", result);

            // Returns the created program
            return program;
        }

        /// <summary>
        /// Creates a new memory object with the specified flags and of the specified size.
        /// </summary>
        /// <param name="memoryFlags">The flags, that determines the how the memory object is created and how it can be accessed.</param>
        /// <param name="size">The size of memory that should be allocated for the memory object.</param>
        /// <exception cref="OpenClException">If the memory object could not be created, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created memory object.</returns>
        public MemoryObject CreateMemoryObject(MemoryFlag memoryFlags, int size)
        {
            // Creates a new memory object of the specified size and with the specified memory flags
            Result result;
            IntPtr memoryObjectPointer = NativeMethods.CreateBuffer(this.Handle, memoryFlags, new IntPtr(size), IntPtr.Zero, out result);
            
            // Checks if the creation of the memory object was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The memory object could not be created.", result);

            // Creates the memory object from the pointer to the memory object and returns it
            MemoryObject memoryObject = new MemoryObject(memoryObjectPointer, size);
            return memoryObject;
        }

        /// <summary>
        /// Creates a new memory object with the specified flags. The size of memory allocated for the memory object is determined by <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The size of the memory object will be determined by the structure specified in the type parameter.</typeparam>
        /// <param name="memoryFlags">The flags, that determines the how the memory object is created and how it can be accessed.</param>
        /// <exception cref="OpenClException">If the memory object could not be created, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the created memory object.</returns>
        public MemoryObject CreateMemoryObject<T>(MemoryFlag memoryFlags) where T : struct => this.CreateMemoryObject(memoryFlags, Marshal.SizeOf<T>());

        public MemoryObject CreateMemoryObject<T>(MemoryFlag memoryFlags, T value) where T : struct
        {
            // Tries to create the memory object, if anything goes wrong, then it is crucial to free the allocated memory
            IntPtr hostMemoryObjectPointer = IntPtr.Zero;
            try
            {
                // Determines the size of the specified value and creates a pointer that points to the data inside the structure
                IntPtr size = new IntPtr(Marshal.SizeOf<T>());
                hostMemoryObjectPointer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(value, hostMemoryObjectPointer, false);

                // Creates a new memory object for the specified value
                Result result;
                IntPtr memoryObjectPointer = NativeMethods.CreateBuffer(this.Handle, memoryFlags, size, hostMemoryObjectPointer, out result);

                // Checks if the creation of the memory object was successful, if not, then an exception is thrown
                if (result != Result.Success)
                    throw new OpenClException("The memory object could not be created.", result);

                // Creates the memory object from the pointer to the memory object and returns it
                MemoryObject memoryObject = new MemoryObject(memoryObjectPointer, size.ToInt32());
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
            IntPtr contextPointer = NativeMethods.CreateContext(
                null,
                (uint)devices.Count(),
                devices.Select(device => device.Handle).ToArray(),
                IntPtr.Zero,
                IntPtr.Zero,
                out result);

            // Checks if the device creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The context could not be created.", result);

            // Creates the new context object from the pointer and returns it
            return new Context(contextPointer);
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the context has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the context.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the context has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL context
                NativeMethods.ReleaseContext(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the context has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="Context"/> instance.
        /// </summary>
        ~Context()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the context.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the context
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}