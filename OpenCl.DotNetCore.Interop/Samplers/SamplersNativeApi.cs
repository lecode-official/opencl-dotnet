
#region Using Directives

#endregion

namespace OpenCl.DotNetCore.Interop.Samplers
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Samplers API.
    /// </summary>
    public static class SamplersNativeApi
    {
        #region Public Static Methods

        //extern CL_API_ENTRY cl_sampler CL_API_CALL
        //clCreateSamplerWithProperties(cl_context                     /* context */,
        //                            const cl_sampler_properties *  /* normalized_coords */,
        //                            cl_int *                       /* errcode_ret */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainSampler(cl_sampler /* sampler */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clReleaseSampler(cl_sampler /* sampler */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetSamplerInfo(cl_sampler         /* sampler */,
        //                cl_sampler_info    /* param_name */,
        //                size_t             /* param_value_size */,
        //                void *             /* param_value */,
        //                size_t *           /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        #endregion

        #region Deprecated Static Methods

        //extern CL_API_ENTRY CL_EXT_PREFIX__VERSION_1_2_DEPRECATED cl_sampler CL_API_CALL
        //clCreateSampler(cl_context          /* context */,
        //                cl_bool             /* normalized_coords */,
        //                cl_addressing_mode  /* addressing_mode */,
        //                cl_filter_mode      /* filter_mode */,
        //                cl_int *            /* errcode_ret */) CL_EXT_SUFFIX__VERSION_1_2_DEPRECATED;

        #endregion
    }
}