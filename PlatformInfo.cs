
namespace OpenCl.DotNetCore
{
    /// <summary>
    /// Represents an enumeration for the different types of information that can be queried from an OpenCl platform.
    /// </summary>
    public enum PlatformInfo : uint
    {
        /// <summary>
        /// OpenCL profile string. Returns the profile name supported by the implementation. The profile name returned can be one
        /// of the following strings: FULL_PROFILE - if the implementation supports the OpenCL specification (functionality defined
        /// as part of the core specification and does not require any extensions to be supported). EMBEDDED_PROFILE - if the
        /// implementation supports the OpenCL embedded profile. The embedded profile is defined to be a subset for each version of
        /// OpenCL.
        /// </summary>
        Profile = 0x0900,

        /// <summary>
        /// OpenCL version string. Returns the OpenCL version supported by the implementation. This version string has the following
        /// format: "OpenCL[space][major_version.minor_version][space][platform-specific information].
        /// </summary>
        Version = 0x0901,

        /// <summary>
        /// Platform name string.
        /// </summary>
        Name = 0x0902,

        /// <summary>
        /// Platform vendor string.
        /// </summary>
        Vendor = 0x0903,

        /// <summary>
        /// Returns a space-separated list of extension names (the extension names themselves do not contain any spaces) supported
        /// by the platform. Extensions defined here must be supported by all devices associated with this platform.
        /// </summary>
        Extensions = 0x0904
    }
}