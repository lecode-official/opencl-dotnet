
#region Using Directives

using System;
using System.Linq;
using OpenCl.DotNetCore.Interop;

#endregion

namespace OpenCl.DotNetCore.Tests
{
    /// <summary>
    /// Represents a test program, that is used to test the OpenCL native interop wrapper.
    /// </summary>
    public class Test
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
                    __kernel void matvec_mult(__global float4* matrix,
                                              __global float4* vector,
                                              __global float* result) {
                        int i = get_global_id(0);
                        result[i] = dot(matrix[i], vector[0]);
                    }";

                // Creates a program and then the kernel from it
                using (Program program = context.CreateAndBuildProgramFromString(code))
                {
                    using (Kernel kernel = program.CreateKernel("matvec_mult"))
                    {
                        // Creates the memory objects for the input arguments of the kernel
                        MemoryObject matrix = context.CreateMemoryObject(MemoryFlag.ReadOnly | MemoryFlag.CopyHostPointer, new float[]
                        {
                             0f,  2f,  4f,  6f,
                             8f, 10f, 12f, 14f,
                            16f, 18f, 20f, 22f,
                            24f, 26f, 28f, 30f
                        });
                        MemoryObject vector = context.CreateMemoryObject(MemoryFlag.ReadOnly | MemoryFlag.CopyHostPointer, new float[] { 0f, 3f, 6f, 9f });
                        MemoryObject result = context.CreateMemoryObject<float>(MemoryFlag.WriteOnly, 4);

                        // Tries to execute the kernel
                        try
                        {
                            // Sets the arguments of the kernel
                            kernel.SetKernelArgument(0, matrix);
                            kernel.SetKernelArgument(1, vector);
                            kernel.SetKernelArgument(2, result);
                            
                            // Creates a command queue, executes the kernel, and retrieves the result
                            using (CommandQueue commandQueue = CommandQueue.CreateCommandQueue(context, device))
                            {
                                commandQueue.EnqueueNDRangeKernel(kernel, 1, 4);
                                float[] resultArray = commandQueue.ReadMemoryObject<float>(result, 4);
                                Console.WriteLine($"Result: ({string.Join(", ", resultArray)})");
                            }
                        }
                        catch (OpenClException exception)
                        {
                            Console.WriteLine(exception.Message);
                        }

                        // Disposes of the memory objects
                        matrix.Dispose();
                        vector.Dispose();
                        result.Dispose();
                    }
                }
            }
        }

        #endregion
    }
}