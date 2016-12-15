
namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an enumeration for the result codes that are returned by the native OpenCL functions.
    /// </summary>
    public enum Result : int
    {
        Success = 0,
        DeviceNotFound = -1,
        DeviceNotAvailable = -2,
        CompilerNotAvailable = -3,
        MemObjectAllocationFailure = -4,
        OutOfResources = -5,
        OutOfHostMemory = -6,
        ProfilingInfoNotAvailable = -7,
        MemCopyOverlap = -8,
        ImageFormatMismatch = -9,
        ImageFormatNotSUPPORTED = -10,
        BuildProgramFailure = -11,
        MapFailure = -12,
        InvalidValue = -30,
        InvalidDeviceType = -31,
        InvalidPlatform = -32,
        InvalidDevice = -33,
        InvalidContext = -34,
        InvalidQueueProperties = -35,
        InvalidCommandQueue = -36,
        InvalidHostPtr = -37,
        InvalidMemObject = -38,
        InvalidImageFormatDescriptor = -39,
        InvalidImageSize = -40,
        InvalidSampler = -41,
        InvalidBinary = -42,
        InvalidBuildOptions = -43,
        InvalidProgram = -44,
        InvalidProgramExecutable = -45,
        InvalidKernelName = -46,
        InvalidKernelDefinition = -47,
        InvalidKernel = -48,
        InvalidArgIndex = -49,
        InvalidArgValue = -50,
        InvalidArgSize = -51,
        InvalidKernelArgs = -52,
        InvalidWorkDimension = -53,
        InvalidWorkGroupSize = -54,
        InvalidWorkItemSize = -55,
        InvalidGlobalOffset = -56,
        InvalidEventWaitList = -57,
        InvalidEvent = -58,
        InvalidOperation = -59,
        InvalidGLObject = -60,
        InvalidBufferSize = -61,
        InvalidMipLevel = -62,
        InvalidGlobalWorkSize = -63,

        InvalidProperty = -64,

        InvalidImageDescriptor = -65,

        InvalidCompilerOptions = -66,
        
        InvalidLinkerOptions = -67,
        
        InvalidDevicePartitionCount = -68,
        
        InvalidPipeSize = -69,

        InvalidDeviceQueue = -70
    }
}