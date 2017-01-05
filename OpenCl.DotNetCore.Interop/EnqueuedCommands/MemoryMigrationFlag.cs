
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop.EnqueuedCommands
{
    /// <summary>
    /// Represents an enumeration for the different flags, that can be used when migrating memory.
    /// </summary>
    [Flags]
    public enum MemoryMigrationFlag : ulong
    {
        /// <summary>
        /// 
        /// </summary>
        Host = 1 << 0,

        /// <summary>
        /// 
        /// </summary>
        ContentUndefined = 1 << 1
    }
}