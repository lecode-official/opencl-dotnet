
#region Using Directives

#endregion

namespace OpenCl.DotNetCore.Interop.Extensions
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Extensions API.
    /// </summary>
    public static class ExtensionsNativeApi
    {
        #region Public Static Methods

        //extern CL_API_ENTRY void * CL_API_CALL 
        //clGetExtensionFunctionAddressForPlatform(cl_platform_id /* platform */,
        //                                        const char *   /* func_name */) CL_API_SUFFIX__VERSION_1_2;

        #endregion

        #region Deprecated Public Methods

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_1_DEPRECATED void * CL_API_CALL
        //clGetExtensionFunctionAddress(const char * /* func_name */) CL_EXT_SUFFIX__VERSION_1_1_DEPRECATED;

        #endregion
    }
}