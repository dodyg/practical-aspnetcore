using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

var config = new ManualConfig()
.WithOptions(ConfigOptions.DisableOptimizationsValidator)
.AddValidator(JitOptimizationsValidator.DontFailOnError)
.AddLogger(ConsoleLogger.Default)
.AddColumnProvider(DefaultColumnProviders.Instance);

BenchmarkRunner.Run<SnakeCaseConverter>(config);

// Implementation from https://github.com/JamesNK/Newtonsoft.Json/blob/cdf10151d507d497a3f9a71d36d544b199f73435/Src/Newtonsoft.Json/Utilities/StringUtils.cs
// Modified to use span
internal static class StringUtils
{
    internal enum SnakeCaseState
    {
        Start,
        Lower,
        Upper,
        NewWord
    }

    public static string ToSnakeCaseNewtonsoft(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return s;
        }

        StringBuilder sb = new StringBuilder();
        SnakeCaseState state = SnakeCaseState.Start;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                if (state != SnakeCaseState.Start)
                {
                    state = SnakeCaseState.NewWord;
                }
            }
            else if (char.IsUpper(s[i]))
            {
                switch (state)
                {
                    case SnakeCaseState.Upper:
                        bool hasNext = (i + 1 < s.Length);
                        if (i > 0 && hasNext)
                        {
                            char nextChar = s[i + 1];
                            if (!char.IsUpper(nextChar) && nextChar != '_')
                            {
                                sb.Append('_');
                            }
                        }
                        break;
                    case SnakeCaseState.Lower:
                    case SnakeCaseState.NewWord:
                        sb.Append('_');
                        break;
                }

                char c;
#if HAVE_CHAR_TO_LOWER_WITH_CULTURE
                    c = char.ToLower(s[i], CultureInfo.InvariantCulture);
#else
                c = char.ToLowerInvariant(s[i]);
#endif
                sb.Append(c);

                state = SnakeCaseState.Upper;
            }
            else if (s[i] == '_')
            {
                sb.Append('_');
                state = SnakeCaseState.Start;
            }
            else
            {
                if (state == SnakeCaseState.NewWord)
                {
                    sb.Append('_');
                }

                sb.Append(s[i]);
                state = SnakeCaseState.Lower;
            }
        }

        return sb.ToString();
    }

    public static string ToSnakeCaseLinq(string s)
    {
        return string.Concat(s.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
}

[MemoryDiagnoser]
public class SnakeCaseConverter
{
    [Benchmark]
    public string ConvertToSnakeCaseNewtonsoft()
    {
        return StringUtils.ToSnakeCaseNewtonsoft("SocialSecurityNumber");
    }

    [Benchmark]
    public string ConvertToSnakeCaseLinq()
    {
        return StringUtils.ToSnakeCaseLinq("SocialSecurityNumber");
    }
}
