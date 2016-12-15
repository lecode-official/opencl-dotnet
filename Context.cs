
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

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
            IntPtr contextPointer = NativeMethods.CreateContext(null,
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
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}