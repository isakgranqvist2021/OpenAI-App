using System.Text.RegularExpressions;

namespace OpenAIApp.Config;

public class Environment
{
    public string? OpenApiSecret { get; set; } = null;

    private static Regex matcher = new Regex(
        @"([0-9A-Za-z_]*)(={1})([0-9A-Za-z_=?\-\:\/].*)"
    );

    public static void Load()
    {
        var output = System.IO.File.ReadAllText(".env").Trim();
        var matches = matcher.Matches(output);

        foreach (Match match in matches)
        {
            GroupCollection groups = match.Groups;
            System.Environment.SetEnvironmentVariable(
                groups[1].Value,
                groups[3].Value
            );
        }
    }
}