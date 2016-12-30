
#region Using Directives

using System;
using System.Runtime.InteropServices;
using OpenCl.DotNetCore.Interop;
using OpenCl.DotNetCore.Interop.Memory;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL memory object.
    /// TODO: Make MemoryObject abstract and derive Image and Buffer from it.
    /// </summary>
    public class MemoryObject : HandleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="MemoryObject"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL memory object.</param>
        internal MemoryObject(IntPtr handle)
            : base(handle)
        {
        }

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
                        this.size = InteropConverter.To<long>(this.GetMemoryObjectInformation(MemoryObjectInformation.Size));
                    else
                        this.size = (long)InteropConverter.To<int>(this.GetMemoryObjectInformation(MemoryObjectInformation.Size));
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
                    this.flags = (MemoryFlag)InteropConverter.To<ulong>(this.GetMemoryObjectInformation(MemoryObjectInformation.Flags));
                return this.flags.Value;
            }
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the OpenCL memory object.
        /// </summary>
        /// <param name="memoryObjectInformation">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetMemoryObjectInformation(MemoryObjectInformation memoryObjectInformation)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            UIntPtr returnValueSize;
            Result result = MemoryNativeApi.GetMemoryObjectInformation(this.Handle, memoryObjectInformation, UIntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The memory object information could not be retrieved.", result);

            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToUInt32()];
            result = MemoryNativeApi.GetMemoryObjectInformation(this.Handle, memoryObjectInformation, new UIntPtr((uint)output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The memory object information could not be retrieved.", result);

            // Returns the output
            return output;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the resources that have been acquired by the memory object.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected override void Dispose(bool disposing)
        {
            // Checks if the memory object has already been disposed of, if not, then the memory object is disposed of
            if (!this.IsDisposed)
                MemoryNativeApi.ReleaseMemoryObject(this.Handle);

            // Makes sure that the base class can execute its dispose logic
            base.Dispose(disposing);
        }

        #endregion
    }
}