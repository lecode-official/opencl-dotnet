
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL program.
    /// </summary>
    public class Program : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Program"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL program.</param>
        internal Program(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL program.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion
        
        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the program has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the program.
        /// </summary>
        /// <param name="disposing">
        /// Determines whether managed object or managed and unmanaged resources should be disposed of.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the program has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL program
                NativeMethods.ReleaseProgram(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the program has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="Program"/> instance.
        /// </summary>
        ~Program()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the program.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the program
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}