# A benchmark between two approaches in converting CamelCase to snake_case

The first implementation is obtained from [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/blob/cdf10151d507d497a3f9a71d36d544b199f73435/Src/Newtonsoft.Json/Utilities/StringUtils.cs).

The second implementation is obtained from this [Gist](https://gist.github.com/vkobel/d7302c0076c64c95ef4b).

## instructions

- `dotnet build -configuration Release` to build the benchmark
- `dotnet run`