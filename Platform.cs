
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
            this.handle = handle;
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Contains the handle to the OpenCL platform.
        /// </summary>
        private IntPtr handle;

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
                    this.name = this.GetPlatformInformation(PlatformInfo.Name);
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
                    this.vendor = this.GetPlatformInformation(PlatformInfo.Vendor);
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
                    this.version = new Version(this.GetPlatformInformation(PlatformInfo.Version));
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
                    string profileName = this.GetPlatformInformation(PlatformInfo.Profile);
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
                    this.extensions = this.GetPlatformInformation(PlatformInfo.Extensions).Split(' ').ToList();
                return this.extensions;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retrieves the specified information about the OpenCL platform.
        /// </summary>
        /// <param name="platformInfo">The kind of information that is to be retrieved.</param>
        /// <exception cref="OpenClException">
        /// If the information could not be retrieved, then an <see cref="OpenClException"/> exception is thrown.
        /// </exception>
        /// <returns>Returns the specified information.</returns>
        private string GetPlatformInformation(PlatformInfo platformInfo)
        {
            // Retrieves the size of the return value in bytes, this is used to later get the full information
            IntPtr returnValueSize;
            Result result = NativeMethods.GetPlatformInfo(this.handle, platformInfo, IntPtr.Zero, null, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The platform information could not be retrieved.", result);
            
            // Allocates enough memory for the return value and retrieves it
            byte[] output = new byte[returnValueSize.ToInt32() + 1];
            result = NativeMethods.GetPlatformInfo(this.handle, platformInfo, new IntPtr(output.Length), output, out returnValueSize);
            if (result != Result.Success)
                throw new OpenClException("The platform information could not be retrieved.", result);

            // The return value is an ASCII encoded byte array, so it is decoded to a string and returned
            return Encoding.ASCII.GetString(output);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Gets all the available platforms.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// If the platforms could not be queried, then an <see cref="InvalidOperationException"/> exception is thrown.
        /// </exception>
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
                throw new OpenClException("The number of platforms could not be retrieved.", result);

            // Converts the pointers to platform structures
            List<Platform> platforms = new List<Platform>();
            foreach (IntPtr platformPointer in platformPointers)
                yield return new Platform(platformPointer);
        }

        #endregion
    }
}