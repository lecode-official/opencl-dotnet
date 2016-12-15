
#region Using Directives

using System;

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
            // Gets all available platforms and prints out information about them
            foreach (Platform platform in Platform.GetPlatforms())
            {
                // Prints out the information about the platform
                Console.WriteLine($"Name: {platform.Name}");
                Console.WriteLine($"Vendor: {platform.Vendor}");
                Console.WriteLine("Version:");
                Console.WriteLine($"    Major Version: {platform.Version.MajorVersion}");
                Console.WriteLine($"    Minor Version: {platform.Version.MinorVersion}");
                Console.WriteLine($"    Platform-Specific Information: {platform.Version.PlatformSpecificInformation}");
                Console.WriteLine($"Profile: {platform.Profile}");
                Console.WriteLine("Extensions:");
                foreach (string extension in platform.Extensions)
                    Console.WriteLine($"    - {extension}");

                // Gets all devices of the platform and prints out information about them
                Console.WriteLine("Devices:");
                foreach (Device device in platform.GetDevices(DeviceType.All))
                    Console.WriteLine($"    - {device.Name} ({device.Vendor})");
            }
        }

        #endregion
    }
}