
namespace OpenCl.DotNetCore.Interop.Memory
{
    /// <summary>
    /// Represents an enumeration for the different types of information that can be queried from an pipe object.
    /// </summary>
    public enum PipeInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        PacketSize = 0x1120,

        /// <summary>
        /// 
        /// </summary>
        MaximumNumberOfPackets = 0x1121
    }
}