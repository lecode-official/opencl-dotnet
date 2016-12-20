
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop
{
    /// <summary>
    /// Represents an enumeration for the command queue properties.
    /// </summary>
    [Flags]
    public enum CommandQueueProperty : ulong
    {
        /// <summary>
        /// Determines whether the commands queued in the command-queue are executed in-order or out-of-order. If set, the commands in the command-queue are executed out-of-order. Otherwise, commands are executed in-order.
        /// </summary>
        QueueOutOfOrderExecutionModeEnable = 1 << 0,

        /// <summary>
        /// Enables or disables profiling of commands in the command-queue. If set, the profiling of commands is enabled. Otherwise profiling of commands is disabled.
        /// </summary>
        QueueProfilingEnable = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        QueueOnDevice = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        QueueOnDeviceDefault = 1 << 3
    }
}