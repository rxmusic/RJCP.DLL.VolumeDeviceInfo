﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

#if !SIGN
// Required for Unit Testing and Logging
[assembly: InternalsVisibleTo("RJCP.IO.VolumeDeviceInfoTest")]
[assembly: InternalsVisibleTo("VolumeInfo")]
#endif