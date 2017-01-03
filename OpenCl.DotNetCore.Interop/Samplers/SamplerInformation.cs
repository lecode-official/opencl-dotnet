
namespace OpenCl.DotNetCore.Interop.Samplers
{
    /// <summary>
    /// Represents an enumeration for the different types of information that can be queried from a sampler object.
    /// </summary>
    public enum SamplerInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        ReferenceCount = 0x1150,

        /// <summary>
        /// 
        /// </summary>
        Context = 0x1151,
        
        /// <summary>
        /// 
        /// </summary>
        NormalizedCoordinates = 0x1152,
        
        /// <summary>
        /// 
        /// </summary>
        AddressingMode = 0x1153,
        
        /// <summary>
        /// 
        /// </summary>
        FilterMode = 0x1154,
        
        /// <summary>
        /// 
        /// </summary>
        MipFilterMode = 0x1155,
        
        /// <summary>
        /// 
        /// </summary>
        LodMinimum = 0x1156,
        
        /// <summary>
        /// 
        /// </summary>
        LodMaximum = 0x1157
    }
}