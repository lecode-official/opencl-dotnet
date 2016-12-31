
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
- [ ] Finish documentation
- [ ] Implement the `equals` and the `==` operator, which compares the `Handle`
- [ ] Make `MemoryObject` abstract and derive `Image` and `Buffer` from it
- [ ] Add `async` methods for all native methods that have an `event`