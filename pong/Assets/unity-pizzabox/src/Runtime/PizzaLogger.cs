// Note: we could control this in the build process instead if we desired
#define LOGGING_ENABLED

using UnityEngine;

namespace Pizza.Runtime
{
    /// <summary>
    /// A wrapper for the unity logger.
    /// </summary>
    /// <remarks>
    /// The main purpose is so that informational and warning messages can be disabled when in unity editor.
    /// </remarks>
    public static class PizzaLogger
    {
        private const string LOGGER_ID_KEY = "LoggerID";

        static PizzaLogger()
        {
            // TODO: Decide if this is helpful. THe ideas was to support runtime logging, and having a unique ID per log.
            // Right now this featuture is incomplete anyways.
            EnsureLoggerIDExists();
        }

        private static void EnsureLoggerIDExists()
        {
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(LOGGER_ID_KEY)))
            {
                PlayerPrefs.SetString(LOGGER_ID_KEY, System.Guid.NewGuid().ToString());
            }
        }

        public static string GetLoggerID()
        {
            return PlayerPrefs.GetString(LOGGER_ID_KEY);
        }

        /* Note: 
		 * The Unity Debug.Log has a seperate function for logging object vs string.
		 * I assume calling the string version when possible is more performant. So
		 * we copy them and have one function for using the string and one for the object.
		*/

        public static void Log(string message)
        {
#if LOGGING_ENABLED
            Debug.Log(message);
#endif
        }

        public static void Log(string message, Object context)
        {
#if LOGGING_ENABLED
            Debug.Log(message, context);
#endif
        }

        public static void Log(object message)
        {
#if LOGGING_ENABLED
            Debug.Log(message);
#endif
        }

        public static void Log(object message, Object context)
        {
#if LOGGING_ENABLED
            Debug.Log(message, context);
#endif
        }

        public static void LogWarning(string message)
        {
#if LOGGING_ENABLED
            Debug.LogWarning(message);
#endif
        }

        public static void LogWarning(string message, Object context)
        {
#if LOGGING_ENABLED
            Debug.LogWarning(message, context);
#endif
        }

        public static void LogWarning(object message)
        {
#if LOGGING_ENABLED
            Debug.LogWarning(message);
#endif
        }

        public static void LogWarning(object message, Object context)
        {
#if LOGGING_ENABLED
            Debug.LogWarning(message, context);
#endif
        }

        public static void LogError(string message)
        {
#if LOGGING_ENABLED
            Debug.LogError(message);
#endif
        }

        public static void LogError(string message, Object context)
        {
#if LOGGING_ENABLED
            Debug.LogError(message, context);
#endif
        }

        public static void LogError(object message)
        {
#if LOGGING_ENABLED
            Debug.LogError(message);
#endif
        }

        public static void LogError(object message, Object context)
        {
#if LOGGING_ENABLED
            Debug.LogError(message, context);
#endif
        }

        public static void LogException(System.Exception exception)
        {
#if LOGGING_ENABLED
            Debug.LogException(exception);
#endif
        }

        public static void LogException(System.Exception exception, Object context)
        {
#if LOGGING_ENABLED
            Debug.LogException(exception, context);
#endif
        }

        public static void LogNotImplemented()
        {
            LogWarning("Not Implemented");
        }
    }
}