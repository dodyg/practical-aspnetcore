using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@$"
<html>
<body>
    <a href=""/direction?directions=(left, right, straight, right, left)"">Click for Directions</a>
</body>
</html>
", "text/html"));

app.MapGet("/direction", (DrivingDirection directions) => Results.Text(@$"
<html>
<body>
    <h1>Directions</h1>
    { String.Join(", ", directions.Directions)}
</body>
</html>
", "text/html"));

app.Run();

public enum Direction 
{
    Left, 
    Right,
    Straight
}

public class DrivingDirection
{
    public List<Direction> Directions { get; set; } = new();

    public static ValueTask<DrivingDirection?> BindAsync(HttpContext context, ParameterInfo parameter)
    {    
        DrivingDirection? result;
        try
        {
            // format is (Left, Right, Straight)
            var trimmedValue = context.Request.Query["directions"].ToString().TrimStart('(').TrimEnd(')');
            var segments = trimmedValue?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); 

            if (segments is null)
                return ValueTask.FromResult<DrivingDirection?>(null);

            result = new();
            foreach(var s in segments)
            {
                if (Enum.TryParse(typeof(Direction), s, true, out var dir))
                {
                    result.Directions.Add((Direction)dir!);
                }
            }

            return ValueTask.FromResult<DrivingDirection?>(result);
        }
        catch
        {
            return ValueTask.FromResult<DrivingDirection?>(null);
        }
    } 
}