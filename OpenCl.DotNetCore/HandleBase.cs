
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents the abstract base class for all OpenCL objects, that are represented by a handle.
    /// </summary>
    public abstract class HandleBase : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="HandleBase"/> instance.
        /// </summary>
        /// <param name="handle">The handle that represents the OpenCL object.</param>
        public HandleBase(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL object.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets a value that determines whether the object has alread been disposed of.
        /// </summary>
        protected bool IsDisposed { get; private set; }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the resources that have been acquired.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the object has alread been disposed of
            if (!this.IsDisposed)
            {
                // Sets the handle to null
                this.Handle = IntPtr.Zero;

                // Since the context has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.IsDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="HandleBase"/> instance.
        /// </summary>
        ~HandleBase()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all acquired resources.
        /// </summary>
        public void Dispose()
        {
            // Disposes of all acquired resources
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}