using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

WebApplication app = WebApplication.Create();

app.MapGet("/", () => Results.Text(@$"
<html>
<body>
    <a href=""/direction?directions=(left, right, straight, right, left)"">Click for Directions</a>
</body>
</html>
", "text/html"));

app.MapGet("/direction", ([FromQuery]DrivingDirection directions) => Results.Text(@$"
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

    public static bool TryParse(string value, out DrivingDirection? result)
    {
        try
        {
            // format is (Left, Right, Straight)
            var trimmedValue = value?.TrimStart('(').TrimEnd(')');
            var segments = trimmedValue?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); 

            result = new();
            foreach(var s in segments)
            {
                if (Enum.TryParse(typeof(Direction), s, true, out var dir))
                {
                    result.Directions.Add((Direction)dir);
                }
            }

            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    } 
}