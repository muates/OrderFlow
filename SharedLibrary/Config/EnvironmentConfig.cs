using DotNetEnv;

namespace SharedLibrary.Config;

public static class EnvironmentConfig
{
    public static void LoadEnv()
    {
        try
        {
            var projectDir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
            
            var envFilePath = Path.Combine(projectDir, "Docker", ".env");
            
            if (File.Exists(envFilePath))
            {
                Env.Load(envFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}