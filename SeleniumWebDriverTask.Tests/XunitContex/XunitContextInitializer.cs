using System.Runtime.CompilerServices;
using static Xunit.XunitContext;

public static class XunitContextInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        XunitContext.EnableExceptionCapture();
    }
}
