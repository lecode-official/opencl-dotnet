
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL kernel.
    /// </summary>
    public class Kernel : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Kernel"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL kernel.</param>
        internal Kernel(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL kernel.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Sets the specified argument to the specified value.
        /// </summary>
        /// <param name="index">The index of the parameter.</param>
        /// <param name="memoryObject">The memory object that contains the value to which the kernel argument is to be set.</param>
        public void SetKernelArgument(int index, MemoryObject memoryObject)
        {
            // Checks if the index is positive, if not, then an exception is thrown
            if (index < 0)
                throw new IndexOutOfRangeException($"The specified index {index} is invalid. The index of the argument must always be greater or equal to 0.");

            // Sets the kernel argument and checks if it was successful, if not, then an exception is thrown
            Result result = NativeMethods.SetKernelArgument(this.Handle, (uint)index, new UIntPtr((uint)Marshal.SizeOf(memoryObject.Handle)), memoryObject.Handle);
            if (result != Result.Success)
                throw new OpenClException($"The kernel argument with the index {index} could not be set.", result);
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the kernel has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the kernel.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the kernel has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL kernel
                NativeMethods.ReleaseKernel(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the kernel has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="Kernel"/> instance.
        /// </summary>
        ~Kernel()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the kernel.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the kernel
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}