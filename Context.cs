
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
        /// <exception cref="OpenClException">
        /// If the program could not be created, compiled, or linked, then an <see cref="OpenClException"/> is thrown.
        /// </exception>
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

        #endregion
        
        #region Public Static Methods

        /// <summary>
        /// Creates a new context for the specified device.
        /// </summary>
        /// <param name="device">The device for which the context is to be created.</param>
        /// <exception cref="OpenClException">
        /// If the context could not be created, then an <see cref="OpenClException"/> exception is thrown.
        /// </exception>
        /// <returns>Returns the created context.</returns>
        public static Context CreateContext(Device device) => Context.CreateContext(new List<Device> { device });

        /// <summary>
        /// Creates a new context for the specified device.
        /// </summary>
        /// <param name="devices">The devices for which the context is to be created.</param>
        /// <exception cref="OpenClException">
        /// If the context could not be created, then an <see cref="OpenClException"/> exception is thrown.
        /// </exception>
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
        /// <param name="disposing">
        /// Determines whether managed object or managed and unmanaged resources should be disposed of.
        /// </param>
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