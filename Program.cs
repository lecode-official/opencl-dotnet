
#region Using Directives

using System;
using System.Collections.Generic;

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
            // Gets all available platforms
            IEnumerable<Platform> platforms = Platform.GetPlatforms();

            // Gets the information about all available platforms
            foreach (Platform platform in platforms)
                Console.WriteLine(platform.Name);
        }

        #endregion
    }
}