
namespace OpenCl.DotNetCore.Interop.Memory
{
    /// <summary>
    /// Represents an enumeration for the different types of information that can be queried from an image object.
    /// </summary>
    public enum ImageInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        Format = 0x1110,

        /// <summary>
        /// 
        /// </summary>
        ElementSize = 0x1111,
        
        /// <summary>
        /// 
        /// </summary>
        RowPitch = 0x1112,
        
        /// <summary>
        /// 
        /// </summary>
        SlicePitch = 0x1113,
        
        /// <summary>
        /// 
        /// </summary>
        Width = 0x1114,
        
        /// <summary>
        /// 
        /// </summary>
        Height = 0x1115,
        
        /// <summary>
        /// 
        /// </summary>
        Depth = 0x1116,
        
        /// <summary>
        /// 
        /// </summary>
        ArraySize = 0x1117,
        
        /// <summary>
        /// 
        /// </summary>
        Buffer = 0x1118,
        
        /// <summary>
        /// 
        /// </summary>
        NumberOfMipLevels = 0x1119,
        
        /// <summary>
        /// 
        /// </summary>
        NumberOfSamples = 0x111A
    }
}