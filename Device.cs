
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL device.
    /// </summary>
    public class Device
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Device"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL device.</param>
        internal Device(IntPtr handle)
        {
            this.handle = handle;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Contains the handle to the OpenCL device.
        /// </summary>
        private IntPtr handle;

        #endregion
    }
}