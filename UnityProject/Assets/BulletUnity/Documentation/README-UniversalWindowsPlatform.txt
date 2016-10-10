This version of the libbulletc library requires Unity 5.3.4 or higher.

Building for Universal Windows Platform generates errors about unsafe code. The presence of -unsafe in the SMCP.rsp file is supposed to allow unsafe code
but does not seem to work on Universal Windows Platform.

A workaround is to compile the code in: /Plugins/BulletUnity/BulletSharp into a .dll and include that in the project instead of the C# files.