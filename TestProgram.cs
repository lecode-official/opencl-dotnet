
#region Using Directives

using System;
using System.Linq;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents a test program, that is used to test the OpenCL native interop wrapper.
    /// </summary>
    public class TestProgram
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

            // Creats a new context for the selected device
            using (Context context = Context.CreateContext(device))
            {
                // Creates the kernel code, which multiplies a matrix with a vector
                string code = @"
                    __kernel void matvec_mult(__global float4* matrix, __global float4* vector, __global float* result) {
                        int i = get_global_id(0);
                        result[i] = dot(matrix[i], vector[0]);
                    }";

                // Creates a program and then the kernel from it, which is to be executed
                using (Program program = context.CreateAndBuildProgramFromString(code))
                {
                    using (Kernel kernel = program.CreateKernel("matvec_mult"))
                    {

                    }
                }
            }
        }

        #endregion
    }
}