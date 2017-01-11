
#region Using Directives

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using OpenCl.DotNetCore.Contexts;
using OpenCl.DotNetCore.Devices;
using OpenCl.DotNetCore.Events;
using OpenCl.DotNetCore.Interop;
using OpenCl.DotNetCore.Interop.CommandQueues;
using OpenCl.DotNetCore.Interop.EnqueuedCommands;
using OpenCl.DotNetCore.Kernels;
using OpenCl.DotNetCore.Memory;

#endregion

namespace OpenCl.DotNetCore.CommandQueues
{
    /// <summary>
    /// Represents an OpenCL command queue.
    /// </summary>
    public class CommandQueue : HandleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="CommandQueue"/> instance.
        /// </summary>
        /// <param name="handle">The handle to the OpenCL command queue.</param>
        internal CommandQueue(IntPtr handle)
            : base(handle)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the specified memory object associated with this command queue.
        /// </summary>
        /// <param name="memoryObject">The memory object that is to be read.</param>
        /// <param name="outputSize">The number of array elements that are to be returned.</param>
        /// <typeparam name="T">The type of the array that is to be returned.</typeparam>
        /// <returns>Returns the value of the memory object.</param>
        public T[] EnqueueReadBuffer<T>(MemoryObject memoryObject, int outputSize) where T : struct
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
                Result result = EnqueuedCommandsNativeApi.EnqueueReadBuffer(this.Handle, memoryObject.Handle, 1, UIntPtr.Zero, new UIntPtr((uint)size), resultValuePointer, 0, null, out waitEventPointer);
                
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
        /// <returns>Returns an event for the enqueued command.</returns>
        public WaitEvent EnqueueNDRangeKernel(Kernel kernel, int workDimension, int workUnitsPerKernel)
        {
            // Enqueues the kernel
            IntPtr waitEventPointer;
            Result result = EnqueuedCommandsNativeApi.EnqueueNDRangeKernel(this.Handle, kernel.Handle, (uint)workDimension, null, new IntPtr[] { new IntPtr(workUnitsPerKernel)}, null, 0, null, out waitEventPointer);

            // Checks if the kernel was enqueued successfully, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The kernel could not be enqueued.", result);

            return new WaitEvent(waitEventPointer);
        }

        /// <summary>
        /// Enqueues a n-dimensional kernel to the command queue.
        /// </summary>
        /// <param name="kernel">The kernel that is to be enqueued.</param>
        /// <param name="workDimension">The dimensionality of the work.</param>
        /// <param name="workUnitsPerKernel">The number of work units per kernel.</param>
        /// <exception cref="OpenClException">If the kernel could not be enqueued, then an <see cref="OpenClException"/> is thrown.</exception>
        /// <returns>Returns an event for the enqueued command.</returns>
        public Task<WaitEvent> EnqueueNDRangeKernelAsync(Kernel kernel, int workDimension, int workUnitsPerKernel)
        {
            // Creates a new task completion source, which is used to signal when the command has completed
            TaskCompletionSource<WaitEvent> taskCompletionSource = new TaskCompletionSource<WaitEvent>();

            // Enqueues the kernel
            IntPtr waitEventPointer;
            Result result = EnqueuedCommandsNativeApi.EnqueueNDRangeKernel(this.Handle, kernel.Handle, (uint)workDimension, null, new IntPtr[] { new IntPtr(workUnitsPerKernel)}, null, 0, null, out waitEventPointer);

            // Checks if the kernel was enqueued successfully, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The kernel could not be enqueued.", result);

            // Subscribes to the completed event of the wait event that was returned, when the command finishes, the task completion source is resolved
            WaitEvent waitEvent = new WaitEvent(waitEventPointer);
            waitEvent.OnCompleted += (sender, e) =>
            {
                try
                {
                    if (waitEvent.CommandExecutionStatus == CommandExecutionStatus.Error)
                        taskCompletionSource.TrySetException(new OpenClException($"The command completed with the error code {waitEvent.CommandExecutionStatusCode}."));
                    else
                        taskCompletionSource.TrySetResult(waitEvent);
                }
                catch (Exception exception)
                {
                    taskCompletionSource.TrySetException(exception);
                }
            };
            return taskCompletionSource.Task;
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
            IntPtr commandQueuePointer = CommandQueuesNativeApi.CreateCommandQueue(context.Handle, device.Handle, 0, out result);

            // Checks if the command queue creation was successful, if not, then an exception is thrown
            if (result != Result.Success)
                throw new OpenClException("The command queue could not be created.", result);

            // Creates the new command queue object from the pointer and returns it
            return new CommandQueue(commandQueuePointer);
        }

        #endregion
        
        #region IDisposable Implementation

        /// <summary>
        /// Disposes of the resources that have been acquired by the command queue.
        /// </summary>
        /// <param name="disposing">Determines whether managed object or managed and unmanaged resources should be disposed of.</param>
        protected override void Dispose(bool disposing)
        {
            // Checks if the command queue has already been disposed of, if not, then the command queue is disposed of
            if (!this.IsDisposed)
                CommandQueuesNativeApi.ReleaseCommandQueue(this.Handle);

            // Makes sure that the base class can execute its dispose logic
            base.Dispose(disposing);
        }

        #endregion
    }
}