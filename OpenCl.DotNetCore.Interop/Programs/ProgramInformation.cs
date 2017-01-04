
namespace OpenCl.DotNetCore.Interop.Programs
{
    /// <summary>
    /// Represents an enumeration for the different types of information that can be queried from a program.
    /// </summary>
    public enum ProgramInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        ReferenceCount = 0x1160,

        /// <summary>
        /// 
        /// </summary>
        Context = 0x1161,
        
        /// <summary>
        /// 
        /// </summary>
        NumberOfDevices = 0x1162,
        
        /// <summary>
        /// 
        /// </summary>
        Devices = 0x1163,
        
        /// <summary>
        /// 
        /// </summary>
        Source = 0x1164,
        
        /// <summary>
        /// 
        /// </summary>
        BinarySizes = 0x1165,
        
        /// <summary>
        /// 
        /// </summary>
        Binaries = 0x1166,
        
        /// <summary>
        /// 
        /// </summary>
        NumberOfKernels = 0x1167,
        
        /// <summary>
        /// 
        /// </summary>
        KernelNames = 0x1168,
        
        /// <summary>
        /// 
        /// </summary>
        Il = 0x1169
    }
}