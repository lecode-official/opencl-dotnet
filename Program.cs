
#region Using Directives

using System;
using System.Linq;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents a test program, that is used to test the OpenCL native interop wrapper.
    /// </summary>
    public class Program
    {
        #region Public Static Methods

        /// <summary>
        /// This is the entrypoint to the application.
        /// </summary>
        /// <param name="args">The command line arguments that have been passed to the program.</param>
        public static void Main(string[] args)
        {
            // Gets the first available platform and selects the first device offered by the platform
            Platform platform = Platform.GetPlatforms().FirstOrDefault();
            Device device = platform.GetDevices(DeviceType.All).FirstOrDefault();

            // Prints out information about the selected device
            Console.WriteLine($"Using {device.Name} ({device.Vendor})");
        }

        #endregion
    }
}