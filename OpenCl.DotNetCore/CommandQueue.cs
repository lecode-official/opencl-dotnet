
#region Using Directives

using System;
using System.Runtime.InteropServices;
using OpenCl.DotNetCore.Interop;

#endregion

namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an OpenCL command queue.
    /// </summary>
    public class CommandQueue : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="CommandQueue"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL command queue.</param>
        internal CommandQueue(IntPtr handle)
        {
            this.Handle = handle;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the handle to the OpenCL command queue.
        /// </summary>
        internal IntPtr Handle { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the specified memory object associated with this command queue.
        /// </summary>
        /// <param name="memoryObject">The memory object that is to be read.</param>
        /// <param name="outputSize">The number of array elements that are to be returned.</param>
        /// <typeparam name="T">The type of the array that is to be returned.</typeparam>
        /// <returns>Returns the value of the memory object.</param>
        public T[] ReadMemoryObject<T>(MemoryObject memoryObject, int outputSize) where T : struct
        {
            // Tries to read the memory object
            IntPtr resultValuePointer = IntPtr.Zero;
            try
            {
                // Allocates enough memory for the result value
                int size = Marshal.SizeOf<T>() * outputSize;
                resultValuePointer = Marshal.AllocHGlobal(size);

                // Reads the memory object, by enqueuing the read operation to the command queue
                IntPtr waitEventPointer;
                Result result = NativeMethods.EnqueueReadBuffer(this.Handle, memoryObject.Handle, 1, UIntPtr.Zero, new UIntPtr((uint)size), resultValuePointer, 0, null, out waitEventPointer);
                
                // Checks if the read operation was queued successfuly, if not, an exception is thrown
                if (result != Result.Success)
                    throw new OpenClException("The memory object could not be read.", result);

                // Goes through the result and converts the content of the result to an array
                T[] resultValue = new T[outputSize];
                for (int i = 0; i < outputSize; i++)
                    resultValue[i] = Marshal.PtrToStructure<T>(IntPtr.Add(resultValuePointer, i * Marshal.SizeOf<T>()));
                
                // Returns the content of the memory object
                return resultValue;
            }
            finally
            {
                // Finally the allocated memory has to be freed
                if (resultValuePointer != IntPtr.Zero)
                    Marshal.FreeHGlobal(resultValuePointer);
            }
        }

        /// <summary>
        /// Enqueues a n-dimensional kernel to the command queue.
        /// </summary>
        /// <param name="kernel">The kernel that is to be enqueued.</param>
        /// <param name="workDimension">The dimensionality of the work.</param>
        /// <param name="workUnitsPerKernel">The number of work units per kernel.</param>
        /// <exception cref="OpenClException">If the kernel could not be enqueued, then an <see cref="OpenClException"/> is thrown.</exception>
        public void EnqueueNDRangeKernel(Kernel kernel, int workDimension, int workUnitsPerKernel)
        {
            // Enqueues the kernel
            IntPtr waitEventPointer;
            Result result = NativeMethods.EnqueueNDRangeKernel(this.Handle, kernel.Handle, (uint)workDimension, null, new IntPtr[] { new IntPtr(workUnitsPerKernel)}, null, 0, null, out waitEventPointer);

            // Checks if the kernel was enqueued successfully, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The kernel could not be enqueued.", result);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Creates a new command queue for the specified context and device.
        /// </summary>
        /// <param name="context">The context for which the command queue is to be created.</param>
        /// <param name="device">The devices for which the command queue is to be created.</param>
        /// <exception cref="OpenClException">If the command queue could not be created, then an <see cref="OpenClException"/> exception is thrown.</exception>
        /// <returns>Returns the created command queue.</returns>
        public static CommandQueue CreateCommandQueue(Context context, Device device)
        {
            // Creates the new command queue for the specified context and device
            Result result;
            IntPtr commandQueuePointer = NativeMethods.CreateCommandQueue(context.Handle, device.Handle, 0, out result);

            // Checks if the command queue creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The command queue could not be created.", result);

            // Creates the new command queue object from the pointer and returns it
            return new CommandQueue(commandQueuePointer);
        }

        #endregion
        
        #region IDisposable Implementation

        /// <summary>
        /// Contains a value that determines whether the command queue has alread been disposed of.
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// Disposes of the resources that have been acquired by the command queue.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Checks if the command queue has already been disposed of
            if (!this.isDisposed)
            {
                // Releases the OpenCL command queue
                NativeMethods.ReleaseCommandQueue(this.Handle);
                this.Handle = IntPtr.Zero;

                // Since the command queue has been disposed of, the is disposed flag is set to true, so that it is not called twice
                this.isDisposed = true;
            }
        }

        /// <summary>
        /// Destructs the <see cref="CommandQueue"/> instance.
        /// </summary>
        ~CommandQueue()
        {
            // Makes sure that unmanaged resources get disposed of eventually
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes of all resources acquired by the command queue.
        /// </summary>
        public void Dispose()
        {
            // Disposes of the resources acquired by the command queue
            this.Dispose(true);
            
            // Since the resources have already been disposed of, the destructor does not need to be called anymore
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}