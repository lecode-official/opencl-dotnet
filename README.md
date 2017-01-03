
# OpenCL .NET

## Todo

- [x] Separate into three projects: Native Core, CLR Wrapper, Test Program
- [x] `HandleBase` class, which is the base class for all OpenCL classes that need a handle
- [x] Rename the arguments of the native methods
- [x] Rename Info to Information
- [x] Add compiler log to exception, when program cannot be build
- [x] Add method to compile multiple sources at once
- [x] Add method to compile from file/files
- [x] Add method to compile from Stream/Streams
- [x] Put the different APIs of the native wrapper into different classes
- [x] Create a class that converts `byte` arrays into CLR types
- [ ] Port the whole API surface area
    - [x] Command Queues API
    - [x] Contexts API
    - [x] Devices API
    - [ ] Enqueued Commands API
    - [x] Events API
    - [x] Extensions API
    - [x] Kernels API
    - [x] Memory API
    - [x] Platforms API
    - [x] Profiling API
    - [ ] Programs API
    - [x] Samplers API
    - [x] SVM Allocations API
- [ ] Finish API documentation
    - [ ] Command Queues API
    - [x] Contexts API
    - [ ] Devices API
    - [ ] Enqueued Commands API
    - [x] Events API
    - [x] Extensions API
    - [x] Kernels API
    - [ ] Memory API
    - [x] Platforms API
    - [x] Profiling API
    - [ ] Programs API
    - [ ] Samplers API
    - [ ] SVM Allocations API
- [ ] Implement the `equals` and the `==` operator, which compares the `Handle`
- [ ] Make `MemoryObject` abstract and derive `Image`, `Pipe`, and `Buffer` from it
- [ ] Add `async` methods for all native methods that have an `event`
- [ ] Mark everything in the Interop project with an attribute that contains the minimum version of OpenCL required
- [ ] Mark everything in the Interop project with the Obsolete attribute that have been deprecated in OpenCL
- [ ] Create an base class for event and then derive a user event from it, which is returned when calling CreateUserEvent (this should be done to ensure, that SetUserEventStatus can only be called on valid user events)