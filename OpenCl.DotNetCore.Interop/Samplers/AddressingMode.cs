
namespace OpenCl.DotNetCore.Interop.Samplers
{
    /// <summary>
    /// Represents an enumeration for the different addressing modes that can be used for samplers.
    /// </summary>
    public enum AddressingMode : uint
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0x1130,

        /// <summary>
        /// 
        /// </summary>
        ClampToEdge = 0x1131,

        /// <summary>
        /// 
        /// </summary>
        Clamp = 0x1132,

        /// <summary>
        /// 
        /// </summary>
        Repeat = 0x1133,

        /// <summary>
        /// 
        /// </summary>
        MirroredRepeat = 0x1134
    }
}