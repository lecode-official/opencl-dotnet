
#region Using Directives

using System;
using System.Text;

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
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL device.
        /// </summary>
        internal IntPtr Handle { get; private set; }

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
                    this.name = Encoding.ASCII.GetString(this.GetDeviceInformation(DeviceInfo.DeviceName));
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
                    this.vendor = Encoding.ASCII.GetString(this.GetDeviceInformation(DeviceInfo.DeviceVendor));
                return this.vendor;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the device.
        /// </summary>
        /// <param name="deviceInfo">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">
        /// If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.
        /// </exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetDeviceInformation(DeviceInfo deviceInfo)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            IntPtr returnValueSize;
            Result result = NativeMethods.GetDeviceInfo(this.Handle, deviceInfo, IntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The device information could not be retrieved.", result);
            
            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToInt32() + 1];
            result = NativeMethods.GetDeviceInfo(this.Handle, deviceInfo, new IntPtr(output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The device information could not be retrieved.", result);

            // Returns the output
            return output;
        }

        #endregion
    }
}