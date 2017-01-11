
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Memory
{
    /// <summary>
    /// Represents an OpenCL buffer.
    /// </summary>
    public class Buffer : MemoryObject
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Buffer"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL buffer.</param>
        public Buffer(IntPtr handle)
            : base(handle)
        {
        }

        #endregion
    }
}