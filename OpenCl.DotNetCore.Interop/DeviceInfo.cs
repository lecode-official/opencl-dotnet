
#region Using Directives

using System;

#endregion

namespace OpenCl.DotNetCore.Interop
{
    /// <summary>
    /// Represents an enumeration that identifies the device information that can be queried from a device.
    /// </summary>
    public enum DeviceInfo : uint
    {
        /// <summary>
        /// 
        /// </summary>
        DeviceType = 0x1000,

        /// <summary>
        /// 
        /// </summary>
        DeviceVendorId = 0x1001,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxComputeUnits = 0x1002,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxWorkItemDimensions = 0x1003,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxWorkGroupSize = 0x1004,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxWorkItemSizes = 0x1005,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthChar = 0x1006,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthShort = 0x1007,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthInt = 0x1008,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthLong = 0x1009,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthFloat = 0x100A,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthDouble = 0x100B,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxClockFrequency = 0x100C,

        /// <summary>
        /// The default compute device address space size of the global address space specified as an unsigned integer value in bits. Currently supported values are 32 or 64 bits.
        /// </summary>
        DeviceAddressBits = 0x100D,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxReadImageArguments = 0x100E,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxWriteImageArguments = 0x100F,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxMemoryAllocationSize = 0x1010,

        /// <summary>
        /// 
        /// </summary>
        DeviceImage2DMaxWidth = 0x1011,

        /// <summary>
        /// 
        /// </summary>
        DeviceImage2DMaxHeight = 0x1012,

        /// <summary>
        /// 
        /// </summary>
        DeviceImage3DMaxWidth = 0x1013,

        /// <summary>
        /// 
        /// </summary>
        DeviceImage3DMaxHeight = 0x1014,

        /// <summary>
        /// 
        /// </summary>
        DeviceImage3DMaxDepth = 0x1015,

        /// <summary>
        /// 
        /// </summary>
        DeviceImageSupport = 0x1016,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxParameterSize = 0x1017,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxSamplers = 0x1018,

        /// <summary>
        /// 
        /// </summary>
        DeviceMemBaseAddressAlignment = 0x1019,

        /// <summary>
        /// 
        /// </summary>
        DeviceMinDataTypeAlignmentSize = 0x101A,

        /// <summary>
        /// 
        /// </summary>
        DeviceSingleFloatingPointConfiguration = 0x101B,

        /// <summary>
        /// 
        /// </summary>
        DeviceGlobalMemoryCacheType = 0x101C,

        /// <summary>
        /// 
        /// </summary>
        DeviceGlobalMemoryCachelineSize = 0x101D,

        /// <summary>
        /// 
        /// </summary>
        DeviceGlobalMemoryCacheSize = 0x101E,

        /// <summary>
        /// 
        /// </summary>
        DeviceGlobalMemorySize = 0x101F,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxConstantBufferSize = 0x1020,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxConstantArguments = 0x1021,

        /// <summary>
        /// 
        /// </summary>
        DeviceLocalMemoryType = 0x1022,

        /// <summary>
        /// 
        /// </summary>
        DeviceLocalMemorySize = 0x1023,

        /// <summary>
        /// 
        /// </summary>
        DeviceErrorCorrectionSupport = 0x1024,

        /// <summary>
        /// 
        /// </summary>
        DeviceProfilingTimerResolution = 0x1025,

        /// <summary>
        /// 
        /// </summary>
        DeviceEndianLittle = 0x1026,

        /// <summary>
        /// Is <c>true</c> if the device is available and <c>false</c> otherwise. A device is considered to be available if the device can be expected to successfully execute commands enqueued to the device.
        /// </summary>
        DeviceAvailable = 0x1027,

        /// <summary>
        /// Is <c>false</c> if the implementation does not have a compiler available to compile the program source. Is <c>true</c> if the compiler is available. This can be <c>false</c> for the embedded platform profile only.
        /// </summary>
        DeviceCompilerAvailable = 0x1028,

        /// <summary>
        /// 
        /// </summary>
        DeviceExecutionCapabilities = 0x1029,

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("DeviceQueueProperties is deprecated, please use DeviceQueueOnHostProperties.")]
        DeviceQueueProperties = 0x102A,

        /// <summary>
        /// 
        /// </summary>
        DeviceQueueOnHostProperties = 0x102A,

        /// <summary>
        /// 
        /// </summary>
        DeviceName = 0x102B,

        /// <summary>
        /// 
        /// </summary>
        DeviceVendor = 0x102C,

        /// <summary>
        /// 
        /// </summary>
        DriverVersion = 0x102D,

        /// <summary>
        /// 
        /// </summary>
        DeviceProfile = 0x102E,

        /// <summary>
        /// 
        /// </summary>
        DeviceVersion = 0x102F,

        /// <summary>
        /// 
        /// </summary>
        DeviceExtensions = 0x1030,

        /// <summary>
        /// 
        /// </summary>
        DevicePlatform = 0x1031,

        /// <summary>
        /// 
        /// </summary>
        DeviceDoubleFloatingPointConfiguration = 0x1032,

        /// <summary>
        /// 
        /// </summary>
        DeviceHalfFloatingPointConfiguration = 0x1033,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredVectorWidthHalf = 0x1034,

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("DeviceHostUnifiedMemory is deprecated.")]
        DeviceHostUnifiedMemory = 0x1035,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthChar = 0x1036,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthShort = 0x1037,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthInt = 0x1038,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthLong = 0x1039,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthFloat = 0x103A,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthDouble = 0x103B,

        /// <summary>
        /// 
        /// </summary>
        DeviceNativeVectorWidthHalf = 0x103C,

        /// <summary>
        /// 
        /// </summary>
        DeviceOpenClCVersion = 0x103D,

        /// <summary>
        /// 
        /// </summary>
        DeviceLinkerAvailable = 0x103E,

        /// <summary>
        /// A semi-colon separated list of built-in kernels supported by the device. An empty string is returned if no built-in kernels are supported by the device.
        /// </summary>
        DeviceBuiltInKernels = 0x103F,

        /// <summary>
        /// 
        /// </summary>
        DeviceImageMaxBufferSize = 0x1040,

        /// <summary>
        /// 
        /// </summary>
        DeviceImageMaxArraySize = 0x1041,

        /// <summary>
        /// 
        /// </summary>
        DeviceParentDevice = 0x1042,

        /// <summary>
        /// 
        /// </summary>
        DevicePartitionMaxSubDevices = 0x1043,

        /// <summary>
        /// 
        /// </summary>
        DevicePartitionProperties = 0x1044,

        /// <summary>
        /// 
        /// </summary>
        DevicePartitionAffinityDomain = 0x1045,

        /// <summary>
        /// 
        /// </summary>
        DevicePartitionType = 0x1046,

        /// <summary>
        /// 
        /// </summary>
        DeviceReferenceCount = 0x1047,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredInteropUserSync = 0x1048,

        /// <summary>
        /// 
        /// </summary>
        DevicePrintfBufferSize = 0x1049,

        /// <summary>
        /// 
        /// </summary>
        DeviceImagePitchAlignment = 0x104A,

        /// <summary>
        /// 
        /// </summary>
        DeviceImageBaseAddressAlignment = 0x104B,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxReadWriteImageArgs = 0x104C,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxGlobalVariableSize = 0x104D,

        /// <summary>
        /// 
        /// </summary>
        DeviceQueueOnDeviceProperties = 0x104E,

        /// <summary>
        /// 
        /// </summary>
        DeviceQueueOnDevicePreferredSize = 0x104F,

        /// <summary>
        /// 
        /// </summary>
        DeviceQueueOnDeviceMaxSize = 0x1050,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxOnDeviceQueues = 0x1051,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxOnDeviceEvents = 0x1052,

        /// <summary>
        /// 
        /// </summary>
        DeviceSvmCapabilities = 0x1053,

        /// <summary>
        /// 
        /// </summary>
        DeviceGlobalVariablePreferredTotalSize = 0x1054,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxPipeArguments = 0x1055,

        /// <summary>
        /// 
        /// </summary>
        DevicePipeMaxActiveReservations = 0x1056,

        /// <summary>
        /// 
        /// </summary>
        DevicePipeMaxPacketSize = 0x1057,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredPlatformAtomicAlignment = 0x1058,

        /// <summary>
        /// 
        /// </summary>
        DevicePreferredLocalAtomicAlignment = 0x105A,

        /// <summary>
        /// 
        /// </summary>
        DeviceIntermediateLanguageVersion = 0x105B,

        /// <summary>
        /// 
        /// </summary>
        DeviceMaxNumberOfSubGroups = 0x105C,

        /// <summary>
        /// 
        /// </summary>
        DeviceSubGroupIndependentForwardProgress = 0x105D
    }
}