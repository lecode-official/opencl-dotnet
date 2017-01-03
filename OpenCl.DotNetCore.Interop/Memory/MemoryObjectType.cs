
namespace OpenCl.DotNetCore.Interop.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public enum MemoryObjectType : uint
    {
        /// <summary>
        /// 
        /// </summary>
        Buffer = 0x10F0,

        /// <summary>
        /// 
        /// </summary>
        Image2D = 0x10F1,

        /// <summary>
        /// 
        /// </summary>
        Image3D = 0x10F2,

        /// <summary>
        /// 
        /// </summary>
        Image2DArray = 0x10F3,

        /// <summary>
        /// 
        /// </summary>
        Image1D = 0x10F4,

        /// <summary>
        /// 
        /// </summary>
        Image1DArray = 0x10F5,

        /// <summary>
        /// 
        /// </summary>
        Image1DBuffer = 0x10F6,

        /// <summary>
        /// 
        /// </summary>
        Pipe = 0x10F7
    }
}