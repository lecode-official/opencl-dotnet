
#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL platform.
    /// </summary>
    public class Platform
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="Platform"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL platform.</param>
        private Platform(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL platform.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Contains the name of the OpenCL platform.
        /// </summary>
        private string name;

        /// <summary>
        /// Gets the name of the OpenCL platform.
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.name))
                    this.name = Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.Name));
                return this.name;
            }
        }

        /// <summary>
        /// Contains the name of the vendor of the OpenCL platform.
        /// </summary>
        private string vendor;

        /// <summary>
        /// Gets the name of the vendor of the OpenCL platform.
        /// </summary>
        public string Vendor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.vendor))
                    this.vendor = Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.Vendor));
                return this.vendor;
            }
        }

        /// <summary>
        /// Contains the version of the OpenCL platform.
        /// </summary>
        private Version version;

        /// <summary>
        /// Gets the version of the OpenCL platform.
        /// </summary>
        public Version Version
        {
            get
            {
                if (this.version == null)
                    this.version = new Version(Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.Version)));
                return this.version;
            }
        }

        /// <summary>
        /// Contains the profile supported by the OpenCL platform.
        /// </summary>
        private Nullable<Profile> profile;

        /// <summary>
        /// Gets the profile supported by the OpenCL platform.
        /// </summary>
        public Profile Profile
        {
            get
            {
                if (!this.profile.HasValue)
                {
                    string profileName = Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.Profile));
                    if (profileName == "FULL_PROFILE")
                        this.profile = Profile.Full;
                    else
                        this.profile = Profile.Embedded;
                }
                return this.profile.Value;
            }
        }

        /// <summary>
        /// Contains the extensions supported by the OpenCL platform.
        /// </summary>
        private IEnumerable<string> extensions;

        /// <summary>
        /// Gets the extensions support by the OpenCL platform.
        /// </summary>
        public IEnumerable<string> Extensions
        {
            get
            {
                if (this.extensions == null)
                    this.extensions = Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.Extensions)).Split(' ').ToList();
                return this.extensions;
            }
        }

        /// <summary>
        /// Contains the the resolution of the host timer in nanoseconds.
        /// </summary>
        private Nullable<long> platformHostTimerResolution;

        /// <summary>
        /// Gets the the resolution of the host timer in nanoseconds.
        /// </summary>
        public long PlatformHostTimerResolution
        {
            get
            {
                if (!this.platformHostTimerResolution.HasValue)
                {
                    byte[] rawPlatformInformation = this.GetPlatformInformation(PlatformInfo.PlatformHostTimerResolution);
                    ulong retrievedPlatformHostTimerResolution = BitConverter.ToUInt64(rawPlatformInformation, 0);
                    this.platformHostTimerResolution = (long)retrievedPlatformHostTimerResolution;
                }
                return this.platformHostTimerResolution.Value;
            }
        }

        /// <summary>
        /// Contains the function name suffix used to identify extension functions to be directed to this platform by the ICD Loader.
        /// </summary>
        private string platformIcdSuffix;

        /// <summary>
        /// Gets the function name suffix used to identify extension functions to be directed to this platform by the ICD Loader.
        /// </summary>
        public string PlatformIcdSuffix
        {
            get
            {
                if (this.platformIcdSuffix == null)
                    this.platformIcdSuffix = Encoding.ASCII.GetString(this.GetPlatformInformation(PlatformInfo.PlatformIcdSuffix));
                return this.platformIcdSuffix;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the OpenCL platform.
        /// </summary>
        /// <param name="platformInfo">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the information could not be retrieved, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns the specified information.</returns>
        private byte[] GetPlatformInformation(PlatformInfo platformInfo)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            UIntPtr returnValueSize;
            Result result = NativeMethods.GetPlatformInfo(this.Handle, platformInfo, UIntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The platform information could not be retrieved.", result);

            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToUInt32()];
            result = NativeMethods.GetPlatformInfo(this.Handle, platformInfo, new UIntPtr((uint)output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The platform information could not be retrieved.", result);

            // Returns the output
            return output;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all devices of the platform that match the specified device type.
        /// </summary>
        /// <param name="deviceType">The type of devices that is to be retrieved.</param>
        /// <exception cref="OpenClException">If the devices could not be retrieved, then a <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns a list of all devices that matched the specified device type.</returns>
        public IEnumerable<Device> GetDevices(DeviceType deviceType)
        {
            // Gets the number of available devices of the specified type
            uint numberOfAvailableDevices;
            Result result = NativeMethods.GetDeviceIds(this.Handle, deviceType, 0, null, out numberOfAvailableDevices);
            if (result != Result.Success)
                throw new OpenClException("The number of available devices could not be queried.", result);

            // Gets the pointers to the devices of the specified type
            IntPtr[] devicePointers = new IntPtr[numberOfAvailableDevices];
            result = NativeMethods.GetDeviceIds(this.Handle, deviceType, numberOfAvailableDevices, devicePointers, out numberOfAvailableDevices);
            if (result != Result.Success)
                throw new OpenClException("The devices could not be retrieved.", result);

            // Converts the pointer to device objects
            foreach (IntPtr devicePointer in devicePointers)
                yield return new Device(devicePointer);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets all the available platforms.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the platforms could not be queried, then an <see cref="InvalidOperationException"/> is thrown.</exception>
        /// <returns>Returns a list with all the availabe platforms.</returns>
        public static IEnumerable<Platform> GetPlatforms()
        {
            // Gets the number of available platforms
            uint numberOfAvailablePlatforms;
            Result result = NativeMethods.GetPlatformIds(0, null, out numberOfAvailablePlatforms);
            if (result != Result.Success)
                throw new OpenClException("The number of platforms could not be queried.", result);
            
            // Gets pointers to all the platforms
            IntPtr[] platformPointers = new IntPtr[numberOfAvailablePlatforms];
            result = NativeMethods.GetPlatformIds(numberOfAvailablePlatforms, platformPointers, out numberOfAvailablePlatforms);
            if (result != Result.Success)
                throw new OpenClException("The platforms could not be retrieved.", result);

            // Converts the pointers to platform objects
            foreach (IntPtr platformPointer in platformPointers)
                yield return new Platform(platformPointer);
        }

        #endregion
    }
}