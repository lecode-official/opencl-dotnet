
#region Using Directives

#endregion

namespace OpenCl.DotNetCore.Interop.Events
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Events API.
    /// </summary>
    public static class EventsNativeApi
    {
        #region Public Static Methods

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clWaitForEvents(cl_uint             /* num_events */,
        //                const cl_event *    /* event_list */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clGetEventInfo(cl_event         /* event */,
        //            cl_event_info    /* param_name */,
        //            size_t           /* param_value_size */,
        //            void *           /* param_value */,
        //            size_t *         /* param_value_size_ret */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_event CL_API_CALL
        //clCreateUserEvent(cl_context    /* context */,
        //                cl_int *      /* errcode_ret */) CL_API_SUFFIX__VERSION_1_1;               

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clRetainEvent(cl_event /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clReleaseEvent(cl_event /* event */) CL_API_SUFFIX__VERSION_1_0;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clSetUserEventStatus(cl_event   /* event */,
        //                    cl_int     /* execution_status */) CL_API_SUFFIX__VERSION_1_1;

        //extern CL_API_ENTRY cl_int CL_API_CALL
        //clSetEventCallback( cl_event    /* event */,
        //                    cl_int      /* command_exec_callback_type */,
        //                    void (CL_CALLBACK * /* pfn_notify */)(cl_event, cl_int, void *),
        //                    void *      /* user_data */) CL_API_SUFFIX__VERSION_1_1;

        #endregion
    }
}