using UnityEditor;
using UnityEngine.CrashReportHandler;

namespace Pizza.Runtime.Editor
{
    [InitializeOnLoad]
    public static class EditorCrashHandler
    {
        static EditorCrashHandler()
        {
            // If this is the Unity Editor, then disable logging exceptions.
            // My hope, is that this stops logging editor errors to our cloud diagnostics (and discord)

            if (CrashReportHandler.enableCaptureExceptions == true)
            {
                CrashReportHandler.enableCaptureExceptions = false;
                PizzaLogger.Log("CrashReportHandler have disabled capturing exceptions.");
            }
        }
    }
}