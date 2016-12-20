
#region Using Directives

#endregion

namespace OpenCl.DotNetCore.Interop.SvmAllocations
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL SVM Allocations API.
    /// </summary>
    public static class SvmAllocationsNativeApi
    {
        #region Public Static Methods

        //extern CL_API_ENTRY void * CL_API_CALL
        //clSVMAlloc(cl_context       /* context */,
        //        cl_svm_mem_flags /* flags */,
        //        size_t           /* size */,
        //        cl_uint          /* alignment */) CL_API_SUFFIX__VERSION_2_0;

        //extern CL_API_ENTRY void CL_API_CALL
        //clSVMFree(cl_context        /* context */,
        //        void *            /* svm_pointer */) CL_API_SUFFIX__VERSION_2_0;

        #endregion
    }
}