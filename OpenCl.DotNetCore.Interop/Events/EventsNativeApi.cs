
#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace OpenCl.DotNetCore.Interop.Events
{
    /// <summary>
    /// Represents a wrapper for the native methods of the OpenCL Events API.
    /// </summary>
    public static class EventsNativeApi
    {
        #region Public Static Methods

        [DllImport("OpenCL", EntryPoint = "clWaitForEvents")]
        public static extern Result WaitForEvents(
            [In] [MarshalAs(UnmanagedType.U4)] uint numberOfEvents,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] eventList
        );

        [DllImport("OpenCL", EntryPoint = "clGetEventInfo")]
        public static extern Result GetEventInformation(
            [In] IntPtr eventPointer,
            [In] [MarshalAs(UnmanagedType.U4)] EventInformation parameterName,
            [In] UIntPtr parameterValueSize,
            [Out] byte[] parameterValue,
            [Out] out UIntPtr parameterValueSizeReturned
        );

        [DllImport("OpenCL", EntryPoint = "clCreateUserEvent")]
        public static extern IntPtr CreateUserEvent(
            [In] IntPtr context,
            [Out] [MarshalAs(UnmanagedType.I4)] out Result errorCode
        );

        [DllImport("OpenCL", EntryPoint = "clRetainEvent")]
        public static extern Result RetainEvent(
            [In] IntPtr eventPointer
        );

        [DllImport("OpenCL", EntryPoint = "clReleaseEvent")]
        public static extern Result ReleaseEvent(
            [In] IntPtr eventPointer
        );

        [DllImport("OpenCL", EntryPoint = "clSetUserEventStatus")]
        public static extern Result SetUserEventStatus(
            [In] IntPtr eventPointer,
            [In] [MarshalAs(UnmanagedType.I4)] int executionStatus
        );

        [DllImport("OpenCL", EntryPoint = "clSetEventCallback")]
        public static extern Result SetEventCallback(
            [In] IntPtr eventPointer,
            [In] [MarshalAs(UnmanagedType.I4)] int commandExecutionCallbackType,
            [In] IntPtr notificationCallback,
            [In] IntPtr userData
        );
        
        #endregion
    }
}