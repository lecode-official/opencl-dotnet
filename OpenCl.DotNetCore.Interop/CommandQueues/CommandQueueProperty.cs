
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop.CommandQueues
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
        OutOfOrderExecutionModeEnable = 1 << 0,

        /// <summary>
        /// Enables or disables profiling of commands in the command-queue. If set, the profiling of commands is enabled. Otherwise profiling of commands is disabled.
        /// </summary>
        ProfilingEnable = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        OnDevice = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        OnDeviceDefault = 1 << 3
    }
}