
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
    public class Context
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
    }
}