namespace Microsoft.Streamye.Commons
{
    public class EnvironmentVariableUtility
    {
        /// <summary>
        /// Get environment variable value by name
        /// </summary>
        /// <param name="name"> environment variable name</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>EnvironmentVariable value</returns>
        public static string GetWithDefaultValue(string name, string defaultValue)
        {
            return System.Environment.GetEnvironmentVariable(name) ?? defaultValue;
        }
    }
}