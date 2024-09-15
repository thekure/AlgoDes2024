using static System.Globalization.CultureInfo;
using static System.Globalization.NumberStyles;

namespace KattisCSharp;

using System;
using System.Collections.Generic;
using System.Linq;

public class ClosestPair
{
    public struct Point
    {
        public double X, Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Point other)
        {
            return Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        public override string ToString()
        {
            return $"{X.ToString("F2")} {Y.ToString("F2")}";
        }
    }

    // Tuple to store closest pair points and their distance
    public static (Point p1, Point p2, double dist) ClosestPairDistance(Point[] points)
    {
        int n = points.Length;

        // Sort points by X coordinate
        var pointsSortedByX = points.OrderBy(p => p.X).ToArray();

        // Sort points by Y coordinate
        var pointsSortedByY = points.OrderBy(p => p.Y).ToArray();

        // Call recursive divide and conquer function
        return ClosestPairRecursive(pointsSortedByX, pointsSortedByY, n);
    }

    private static (Point p1, Point p2, double dist) ClosestPairRecursive(Point[] pointsSortedByX, Point[] pointsSortedByY, int n)
    {
        if (n <= 3)
        {
            return BruteForce(pointsSortedByX, n);
        }

        int mid = n / 2;
        Point midPoint = pointsSortedByX[mid];

        // Divide points into left and right halves
        var leftHalf = pointsSortedByX.Take(mid).ToArray();
        var rightHalf = pointsSortedByX.Skip(mid).Take(n - mid).ToArray();

        // Divide pointsSortedByY as well
        var leftByY = new List<Point>();
        var rightByY = new List<Point>();

        foreach (var point in pointsSortedByY)
        {
            if (point.X <= midPoint.X)
                leftByY.Add(point);
            else
                rightByY.Add(point);
        }

        // Recursive calls for left and right halves
        var closestLeft = ClosestPairRecursive(leftHalf, leftByY.ToArray(), mid);
        var closestRight = ClosestPairRecursive(rightHalf, rightByY.ToArray(), n - mid);

        // Find the smaller distance and corresponding points
        var closestPair = (closestLeft.dist < closestRight.dist) ? closestLeft : closestRight;

        // Create a strip that contains points near the dividing line
        var strip = new List<Point>();
        foreach (var point in pointsSortedByY)
        {
            if (Math.Abs(point.X - midPoint.X) < closestPair.dist)
                strip.Add(point);
        }

        // Find the closest points in the strip
        var closestInStrip = StripClosest(strip.ToArray(), closestPair.dist);

        // Return the minimum distance and points
        return (closestInStrip.dist < closestPair.dist) ? closestInStrip : closestPair;
    }

    private static (Point p1, Point p2, double dist) BruteForce(Point[] points, int n)
    {
        double minDist = double.MaxValue;
        Point closestP1 = new Point();
        Point closestP2 = new Point();

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double dist = points[i].Distance(points[j]);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestP1 = points[i];
                    closestP2 = points[j];
                }
            }
        }

        return (closestP1, closestP2, minDist);
    }

    private static (Point p1, Point p2, double dist) StripClosest(Point[] strip, double d)
    {
        double minDist = d;
        Point closestP1 = new Point();
        Point closestP2 = new Point();

        for (int i = 0; i < strip.Length; i++)
        {
            for (int j = i + 1; j < strip.Length && (strip[j].Y - strip[i].Y) < minDist; j++)
            {
                double dist = strip[i].Distance(strip[j]);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestP1 = strip[i];
                    closestP2 = strip[j];
                }
            }
        }

        return (closestP1, closestP2, minDist);
    }

    public static void Main()
    {
        // Input reading
        int n = int.Parse(Console.ReadLine());
        var points = new Point[n];
        
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split();
            double.TryParse(input[0], Float, InvariantCulture, out var x);
            double.TryParse(input[1], Float, InvariantCulture, out var y);
            points[i] = new Point(x, y);
        }

        // Solve the closest pair problem
        var result = ClosestPairDistance(points);

        // Output the two closest points, one per line
        Console.WriteLine(result.p1);
        Console.WriteLine(result.p2);
    }
}
