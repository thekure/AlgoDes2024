using System.Globalization;
using static System.Console;
using static System.StringSplitOptions;

namespace KattisCSharp.KDTree;

public static class InputHandler
{
    public static string ReadInputFromStandardInput()
    {
        using var reader = new StreamReader(OpenStandardInput());
        return reader.ReadToEnd();
    }
    
    public static List<Point> ParsePoints(string input)
    {
        var points = new List<Point>();

        // Split the input into lines
        var lines = input.Split(new[] { '\n' }, RemoveEmptyEntries);

        // The first line contains the number of points (not used in this case)
        if (lines.Length <= 1)
        {
            return points;
        }

        // Process each line after the first
        for (var i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(new[] { ' ' }, RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                if (double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x) &&
                    double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y))
                {
                    points.Add(new Point(x, y));
                }
                else
                {
                    WriteLine($"Error parsing coordinates: {lines[i]}");
                }
            }
            else
            {
                WriteLine($"Invalid coordinate format: {lines[i]}");
            }
        }

        return points;
    }
}