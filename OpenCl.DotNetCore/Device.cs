
#region Using Directives

using System;
using System.Text;
using OpenCl.DotNetCore.Interop;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL device.
    /// </summary>
    public class Device : HandleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Device"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL device.</param>
        internal Device(IntPtr handle)
            : base(handle)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Contains the name of the device.
        /// </summary>
        private string name;

        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.name))
                    this.name = Encoding.ASCII.GetString(this.GetDeviceInformation(DeviceInformation.DeviceName)).Replace("\0", string.Empty);
                return this.name;
            }
        }

        /// <summary>
        /// Contains the name of the vendor of the device.
        /// </summary>
        private string vendor;

        /// <summary>
        /// Gets the name of the vendor of the device.
        /// </summary>
        public string Vendor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.vendor))
                    this.vendor = Encoding.ASCII.GetString(this.GetDeviceInformation(DeviceInformation.DeviceVendor)).Replace("\0", string.Empty);
                return this.vendor;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the device.
        /// </summary>
        /// <param name="deviceInformation">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetDeviceInformation(DeviceInformation deviceInformation)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            UIntPtr returnValueSize;
            Result result = NativeMethods.GetDeviceInformation(this.Handle, deviceInformation, UIntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The device information could not be retrieved.", result);
            
            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToUInt32()];
            result = NativeMethods.GetDeviceInformation(this.Handle, deviceInformation, new UIntPtr((uint)output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The device information could not be retrieved.", result);

            // Returns the output
            return output;
        }

        #endregion
        
        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the resources that have been acquired by the command queue.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected override void Dispose(bool disposing)
        {
            // Checks if the device has already been disposed of, if not, then the device is disposed of
            if (!this.IsDisposed)
                NativeMethods.ReleaseDevice(this.Handle);

            // Makes sure that the base class can execute its dispose logic
            base.Dispose(disposing);
        }

        #endregion
    }
}