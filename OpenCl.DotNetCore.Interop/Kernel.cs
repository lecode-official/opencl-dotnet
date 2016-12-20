
#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace OpenCl.DotNetCore.Interop
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

        #region Public Properties

        /// <summary>
        /// Contains the function name of the OpenCL kernel.
        /// </summary>
        private string functionName;

        /// <summary>
        /// Gets the function name of the OpenCL kernel.
        /// </summary>
        public string FunctionName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.functionName))
                    this.functionName = Encoding.ASCII.GetString(this.GetKernelInformation(KernelInfo.FunctionName)).Replace("\0", string.Empty);
                return this.functionName;
            }
        }

        /// <summary>
        /// Contains the number of arguments, that the kernel function has.
        /// </summary>
        private Nullable<int> numberOfArguments;

        /// <summary>
        /// Gets the number of arguments, that the kernel function has.
        /// </summary>
        public int NumberOfArguments
        {
            get
            {
                if (!this.numberOfArguments.HasValue)
                {
                    byte[] rawNumberOfArguments = this.GetKernelInformation(KernelInfo.NumberOfArguments);
                    uint retrievedNumberOfArguments = BitConverter.ToUInt32(rawNumberOfArguments, 0);
                    this.numberOfArguments = (int)retrievedNumberOfArguments;
                }
                return this.numberOfArguments.Value;
            }
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the OpenCL kernel.
        /// </summary>
        /// <param name="kernelInfo">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetKernelInformation(KernelInfo kernelInfo)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            UIntPtr returnValueSize;
            Result result = NativeMethods.GetKernelInfo(this.Handle, kernelInfo, UIntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The kernel information could not be retrieved.", result);
            
            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToUInt32()];
            result = NativeMethods.GetKernelInfo(this.Handle, kernelInfo, new UIntPtr((uint)output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The kernel information could not be retrieved.", result);

            // Returns the output
            return output;
        }

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

            // The set kernel argument method needs a pointer to the pointer, therefore the pointer is pinned, so that the garbage collector can not move it in memory
            GCHandle garbageCollectorHandle = GCHandle.Alloc(memoryObject.Handle, GCHandleType.Pinned);
            try
            {
                // Sets the kernel argument and checks if it was successful, if not, then an exception is thrown
                Result result = NativeMethods.SetKernelArgument(this.Handle, (uint)index, new UIntPtr((uint)Marshal.SizeOf(memoryObject.Handle)), garbageCollectorHandle.AddrOfPinnedObject());
                if (result != Result.Success)
                    throw new OpenClException($"The kernel argument with the index {index} could not be set.", result);
            }
            finally
            {
                garbageCollectorHandle.Free();
            }
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