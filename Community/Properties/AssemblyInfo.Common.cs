using System.Reflection;

[assembly: AssemblyCompany("M.B.W. te Paske")]
[assembly: AssemblyCopyright("Copyright @ 2015 M.B.W. te Paske")]

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif

[assembly: AssemblyVersion("1.0.0.15082")]
[assembly: AssemblyFileVersion("1.0.0.15082")]

#if BETA
[assembly: AssemblyInformationalVersion("1.0.0-Beta")]
#endif