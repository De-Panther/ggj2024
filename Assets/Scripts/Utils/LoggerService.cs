using UnityEngine;

namespace Utils
{
    public static class LoggerService
    {
        #region --- Public Methods ---
        
        public static void LogInfo(string message)
        {
            Debug.Log($"<color=#add8e6>INFO: {message}</color>"); // Light blue
        }

        public static void LogWarning(string message)
        {
            Debug.Log($"<color=yellow>WARNING: {message}</color>"); // Yellow
        }

        public static void LogError(string message)
        {
            Debug.Log($"<color=red>ERROR: {message}</color>"); // Red
        }
        
        #endregion
    }
}