
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop.EnqueuedCommands
{
    /// <summary>
    /// Represents an enumeration for the different flags, that can be used when mapping device memory to host memory.
    /// </summary>
    [Flags]
    public enum MapFlag : ulong
    {
        /// <summary>
        /// 
        /// </summary>
        Read = 1 << 0,

        /// <summary>
        /// 
        /// </summary>
        Write = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        WriteInvalidateRegion = 1 << 2
    }
}