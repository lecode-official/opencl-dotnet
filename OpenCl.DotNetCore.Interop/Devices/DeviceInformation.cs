
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop.Devices
{
    /// <summary>
    /// Represents an enumeration that identifies the device information that can be queried from a device.
    /// </summary>
    public enum DeviceInformation : uint
    {
        /// <summary>
        /// 
        /// </summary>
        Type = 0x1000,

        /// <summary>
        /// 
        /// </summary>
        VendorId = 0x1001,

        /// <summary>
        /// 
        /// </summary>
        MaxComputeUnits = 0x1002,

        /// <summary>
        /// 
        /// </summary>
        MaxWorkItemDimensions = 0x1003,

        /// <summary>
        /// 
        /// </summary>
        MaxWorkGroupSize = 0x1004,

        /// <summary>
        /// 
        /// </summary>
        MaxWorkItemSizes = 0x1005,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthChar = 0x1006,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthShort = 0x1007,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthInt = 0x1008,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthLong = 0x1009,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthFloat = 0x100A,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthDouble = 0x100B,

        /// <summary>
        /// 
        /// </summary>
        MaxClockFrequency = 0x100C,

        /// <summary>
        /// The default compute device address space size of the global address space specified as an unsigned integer value in bits. Currently supported values are 32 or 64 bits.
        /// </summary>
        AddressBits = 0x100D,

        /// <summary>
        /// 
        /// </summary>
        MaxReadImageArguments = 0x100E,

        /// <summary>
        /// 
        /// </summary>
        MaxWriteImageArguments = 0x100F,

        /// <summary>
        /// 
        /// </summary>
        MaxMemoryAllocationSize = 0x1010,

        /// <summary>
        /// 
        /// </summary>
        Image2DMaxWidth = 0x1011,

        /// <summary>
        /// 
        /// </summary>
        Image2DMaxHeight = 0x1012,

        /// <summary>
        /// 
        /// </summary>
        Image3DMaxWidth = 0x1013,

        /// <summary>
        /// 
        /// </summary>
        Image3DMaxHeight = 0x1014,

        /// <summary>
        /// 
        /// </summary>
        Image3DMaxDepth = 0x1015,

        /// <summary>
        /// 
        /// </summary>
        ImageSupport = 0x1016,

        /// <summary>
        /// 
        /// </summary>
        MaxParameterSize = 0x1017,

        /// <summary>
        /// 
        /// </summary>
        MaxSamplers = 0x1018,

        /// <summary>
        /// 
        /// </summary>
        MemBaseAddressAlignment = 0x1019,

        /// <summary>
        /// 
        /// </summary>
        MinDataTypeAlignmentSize = 0x101A,

        /// <summary>
        /// 
        /// </summary>
        SingleFloatingPointConfiguration = 0x101B,

        /// <summary>
        /// 
        /// </summary>
        GlobalMemoryCacheType = 0x101C,

        /// <summary>
        /// 
        /// </summary>
        GlobalMemoryCachelineSize = 0x101D,

        /// <summary>
        /// 
        /// </summary>
        GlobalMemoryCacheSize = 0x101E,

        /// <summary>
        /// 
        /// </summary>
        GlobalMemorySize = 0x101F,

        /// <summary>
        /// 
        /// </summary>
        MaxConstantBufferSize = 0x1020,

        /// <summary>
        /// 
        /// </summary>
        MaxConstantArguments = 0x1021,

        /// <summary>
        /// 
        /// </summary>
        LocalMemoryType = 0x1022,

        /// <summary>
        /// 
        /// </summary>
        LocalMemorySize = 0x1023,

        /// <summary>
        /// 
        /// </summary>
        ErrorCorrectionSupport = 0x1024,

        /// <summary>
        /// 
        /// </summary>
        ProfilingTimerResolution = 0x1025,

        /// <summary>
        /// 
        /// </summary>
        EndianLittle = 0x1026,

        /// <summary>
        /// Is <c>true</c> if the device is available and <c>false</c> otherwise. A device is considered to be available if the device can be expected to successfully execute commands enqueued to the .
        /// </summary>
        Available = 0x1027,

        /// <summary>
        /// Is <c>false</c> if the implementation does not have a compiler available to compile the program source. Is <c>true</c> if the compiler is available. This can be <c>false</c> for the embedded platform profile only.
        /// </summary>
        CompilerAvailable = 0x1028,

        /// <summary>
        /// 
        /// </summary>
        ExecutionCapabilities = 0x1029,

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("QueueProperties is deprecated, please use QueueOnHostProperties.")]
        QueueProperties = 0x102A,

        /// <summary>
        /// 
        /// </summary>
        QueueOnHostProperties = 0x102A,

        /// <summary>
        /// 
        /// </summary>
        Name = 0x102B,

        /// <summary>
        /// 
        /// </summary>
        Vendor = 0x102C,

        /// <summary>
        /// 
        /// </summary>
        DriverVersion = 0x102D,

        /// <summary>
        /// 
        /// </summary>
        Profile = 0x102E,

        /// <summary>
        /// 
        /// </summary>
        Version = 0x102F,

        /// <summary>
        /// 
        /// </summary>
        Extensions = 0x1030,

        /// <summary>
        /// 
        /// </summary>
        Platform = 0x1031,

        /// <summary>
        /// 
        /// </summary>
        DoubleFloatingPointConfiguration = 0x1032,

        /// <summary>
        /// 
        /// </summary>
        HalfFloatingPointConfiguration = 0x1033,

        /// <summary>
        /// 
        /// </summary>
        PreferredVectorWidthHalf = 0x1034,

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("HostUnifiedMemory is deprecated.")]
        HostUnifiedMemory = 0x1035,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthChar = 0x1036,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthShort = 0x1037,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthInt = 0x1038,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthLong = 0x1039,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthFloat = 0x103A,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthDouble = 0x103B,

        /// <summary>
        /// 
        /// </summary>
        NativeVectorWidthHalf = 0x103C,

        /// <summary>
        /// 
        /// </summary>
        OpenClCVersion = 0x103D,

        /// <summary>
        /// 
        /// </summary>
        LinkerAvailable = 0x103E,

        /// <summary>
        /// A semi-colon separated list of built-in kernels supported by the device. An empty string is returned if no built-in kernels are supported by the device.
        /// </summary>
        BuiltInKernels = 0x103F,

        /// <summary>
        /// 
        /// </summary>
        ImageMaxBufferSize = 0x1040,

        /// <summary>
        /// 
        /// </summary>
        ImageMaxArraySize = 0x1041,

        /// <summary>
        /// 
        /// </summary>
        ParentDevice = 0x1042,

        /// <summary>
        /// 
        /// </summary>
        PartitionMaxSubDevices = 0x1043,

        /// <summary>
        /// 
        /// </summary>
        PartitionProperties = 0x1044,

        /// <summary>
        /// 
        /// </summary>
        PartitionAffinityDomain = 0x1045,

        /// <summary>
        /// 
        /// </summary>
        PartitionType = 0x1046,

        /// <summary>
        /// 
        /// </summary>
        ReferenceCount = 0x1047,

        /// <summary>
        /// 
        /// </summary>
        PreferredInteropUserSync = 0x1048,

        /// <summary>
        /// 
        /// </summary>
        PrintfBufferSize = 0x1049,

        /// <summary>
        /// 
        /// </summary>
        ImagePitchAlignment = 0x104A,

        /// <summary>
        /// 
        /// </summary>
        ImageBaseAddressAlignment = 0x104B,

        /// <summary>
        /// 
        /// </summary>
        MaxReadWriteImageArgs = 0x104C,

        /// <summary>
        /// 
        /// </summary>
        MaxGlobalVariableSize = 0x104D,

        /// <summary>
        /// 
        /// </summary>
        QueueOnDeviceProperties = 0x104E,

        /// <summary>
        /// 
        /// </summary>
        QueueOnDevicePreferredSize = 0x104F,

        /// <summary>
        /// 
        /// </summary>
        QueueOnDeviceMaxSize = 0x1050,

        /// <summary>
        /// 
        /// </summary>
        MaxOnDeviceQueues = 0x1051,

        /// <summary>
        /// 
        /// </summary>
        MaxOnDeviceEvents = 0x1052,

        /// <summary>
        /// 
        /// </summary>
        SvmCapabilities = 0x1053,

        /// <summary>
        /// 
        /// </summary>
        GlobalVariablePreferredTotalSize = 0x1054,

        /// <summary>
        /// 
        /// </summary>
        MaxPipeArguments = 0x1055,

        /// <summary>
        /// 
        /// </summary>
        PipeMaxActiveReservations = 0x1056,

        /// <summary>
        /// 
        /// </summary>
        PipeMaxPacketSize = 0x1057,

        /// <summary>
        /// 
        /// </summary>
        PreferredPlatformAtomicAlignment = 0x1058,

        /// <summary>
        /// 
        /// </summary>
        PreferredLocalAtomicAlignment = 0x105A,

        /// <summary>
        /// 
        /// </summary>
        IntermediateLanguageVersion = 0x105B,

        /// <summary>
        /// 
        /// </summary>
        MaxNumberOfSubGroups = 0x105C,

        /// <summary>
        /// 
        /// </summary>
        SubGroupIndependentForwardProgress = 0x105D
    }
}