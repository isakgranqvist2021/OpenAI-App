namespace RecGen.Config;

public class Environment
{
    public string? OpenApiSecret { get; set; } = null;

    public static void Load()
    {

        string output = System.IO.File.ReadAllText(".env").Trim();
        string[] parts = output.Split("\n");

        for (int i = 0; i < parts.Length; i++)
        {
            string[] keyValue = parts[i].Split("=");

            System.Environment.SetEnvironmentVariable(
                keyValue[0].Trim(),
                keyValue[1].Trim()
            );
        }
    }
}