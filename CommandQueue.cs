
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL command queue.
    /// </summary>
    public class CommandQueue : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="CommandQueue"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL command queue.</param>
        internal CommandQueue(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL command queue.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates a new command queue for the specified context and device.
        /// </summary>
        /// <param name="context">The context for which the command queue is to be created.</param>
        /// <param name="device">The devices for which the command queue is to be created.</param>
        /// <exception cref="OpenClException">
        /// If the command queue could not be created, then an <see cref="OpenClException"/> exception is thrown.
        /// </exception>
        /// <returns>Returns the created command queue.</returns>
        public static CommandQueue CreateCommandQueue(Context context, Device device)
        {
            // Creates the new command queue for the specified context and device
            Result result;
            IntPtr commandQueuePointer = NativeMethods.CreateCommandQueue(
                context.Handle,
                device.Handle,
                0,
                out result);

            // Checks if the command queue creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The command queue could not be created.", result);

            // Creates the new command queue object from the pointer and returns it
            return new CommandQueue(commandQueuePointer);
        }

        #endregion
        
        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the command queue has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the command queue.
        /// </summary>
        /// <param name="disposing">
        /// Determines whether managed object or managed and unmanaged resources should be disposed of.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the command queue has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL command queue
                NativeMethods.ReleaseCommandQueue(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the command queue has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="CommandQueue"/> instance.
        /// </summary>
        ~CommandQueue()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the command queue.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the command queue
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}