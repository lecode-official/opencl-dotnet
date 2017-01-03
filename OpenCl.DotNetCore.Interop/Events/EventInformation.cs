
namespace OpenCl.DotNetCore.Interop.Events
{
    /// <summary>
    /// Represents an enumeration that identifies the event information that can be queried from an event.
    /// </summary>
    public enum EventInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        CommandQueue = 0x11D0,

        /// <summary>
        /// 
        /// </summary>
        CommandType = 0x11D1,

        /// <summary>
        /// 
        /// </summary>
        ReferenceCount = 0x11D2,

        /// <summary>
        /// 
        /// </summary>
        CommandExecutionStatus = 0x11D3,

        /// <summary>
        /// 
        /// </summary>
        Context = 0x11D4
    }
}