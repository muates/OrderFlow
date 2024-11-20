namespace SharedLibrary.Helper;

public static class ValidationHelper
{
    public static void ValidateNotNullOrEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null or empty");
    }
}