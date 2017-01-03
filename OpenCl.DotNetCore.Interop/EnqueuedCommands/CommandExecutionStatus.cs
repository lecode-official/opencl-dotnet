
namespace OpenCl.DotNetCore.Interop.EnqueuedCommands
{
    /// <summary>
    /// Represents an enumeration for the status of the execution of a command.
    /// </summary>
    public enum CommandExecutionStatus : int
    {
        /// <summary>
        /// 
        /// </summary>
        Complete = 0x0,

        /// <summary>
        /// 
        /// </summary>
        Running = 0x1,

        /// <summary>
        /// 
        /// </summary>
        Submitted = 0x2,

        /// <summary>
        /// 
        /// </summary>
        Queued = 0x3
    }
}