
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL memory object.
    /// </summary>
    public class MemoryObject : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="MemoryObject"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL memory object.</param>
        internal MemoryObject(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL memory object.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Contains the size of the contents of the memory object in bytes.
        /// </summary>
        private Nullable<long> size;

        /// <summary>
        /// Gets the size of the contents of the memory object in bytes.
        /// </summary>
        public long Size
        {
            get
            {
                if (!this.size.HasValue)
                {
                    if (Marshal.SizeOf<IntPtr>() == sizeof(long))
                        this.size = BitConverter.ToInt64(this.GetMemoryObjectInformation(MemoryObjectInfo.Size), 0);
                    else
                        this.size = (long)BitConverter.ToInt32(this.GetMemoryObjectInformation(MemoryObjectInfo.Size), 0);
                }
                return this.size.Value;
            }
        }

        /// <summary>
        /// Contains the flags with which the memory object was created.
        /// </summary>
        private Nullable<MemoryFlag> flags;

        /// <summary>
        /// Gets the flags with which the memory object was created.
        /// </summary>
        public MemoryFlag Flags
        {
            get
            {
                if (!this.flags.HasValue)
                    this.flags = (MemoryFlag)BitConverter.ToUInt64(this.GetMemoryObjectInformation(MemoryObjectInfo.Flags), 0);
                return this.flags.Value;
            }
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the OpenCL memory object.
        /// </summary>
        /// <param name="memoryObjectInfo">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetMemoryObjectInformation(MemoryObjectInfo memoryObjectInfo)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            UIntPtr returnValueSize;
            Result result = NativeMethods.GetMemoryObjectInfo(this.Handle, memoryObjectInfo, UIntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The memory object information could not be retrieved.", result);

            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToUInt32()];
            result = NativeMethods.GetMemoryObjectInfo(this.Handle, memoryObjectInfo, new UIntPtr((uint)output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The memory object information could not be retrieved.", result);

            // Returns the output
            return output;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the memory object has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the memory object.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the memory object has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL memory object
                NativeMethods.ReleaseMemoryObject(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the memory object has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="MemoryObject"/> instance.
        /// </summary>
        ~MemoryObject()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the memory object.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the memory object
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}