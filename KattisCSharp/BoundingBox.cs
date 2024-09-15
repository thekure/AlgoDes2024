using static System.Math;

namespace KattisCSharp;

using System.Collections.Generic;
using System.Linq;

public class BoundingBox
{
    readonly double MinX;
    readonly double MinY;
    readonly double MaxX;
    readonly double MaxY;

    public BoundingBox(List<Point> points)
    {
        MinX = points.Min(p => p.X);
        MinY = points.Min(p => p.Y);
        MaxX = points.Max(p => p.X);
        MaxY = points.Max(p => p.Y);
    }
    
    // Creates an imaginary rectangle around all points contained in the list it is associated with.
    private BoundingBox(double MinX, double MinY, double MaxX, double MaxY) 
    {
        this.MinX = MinX;
        this.MinY = MinY;
        this.MaxX = MaxX;
        this.MaxY = MaxY;
    }
    
    // Checks to see whether 2 boxes cover overlapping territory.
    public bool BoxesOverlap(BoundingBox other) 
    {
        var yOverlap = MinY <= other.MaxY && other.MinY <= MaxY;
        var xOverlap = MinX <= other.MaxX && other.MinX <= MaxX;
        return xOverlap && yOverlap;
    }
    
    public bool ContainsPoint(Point query) {
        var withinX = (MinX <= query.X && query.X <= MaxX);
        var withinY = (MinY <= query.Y && query.Y <= MaxY);
        return withinX && withinY;
    }
    
    public BoundingBox MergeBoundingBoxes(BoundingBox other) 
    {
        var newMinX = Min(MinX, other.MinX);
        var newMinY = Min(MinY, other.MinY);
        var newMaxX = Max(MaxX, other.MaxX);
        var newMaxY = Max(MaxY, other.MaxY);
        return new BoundingBox(newMinX, newMinY, newMaxX, newMaxY);
    }

    public double GetDistanceToBox(Point query)
    {
        // Checks if the point is inside the box.
        if (ContainsPoint(query)) return 0.0;
        
        /*
         * There are 8 possible cases
         * If you have a boundingbox with vertical and horizontal lines
         * between each corner you have a grid.
         * There cases tests if the queryPoint is located in any of the "grid" sections
         */
        
        // Upper Left:
        if (query.X < MinX && query.Y < MinY)
        {
            return query.Distance(MinX, MinY);
        }
        
        // Upper Middle:
        if (MinX <= query.X && query.X <= MaxX && query.Y <= MinY) {
            return Abs(Abs(query.Y) - Abs(MinY));
        }
        
        // Upper Right:
        if (MaxX < query.X && query.Y < MinY)
        {
            return query.Distance(MaxX, MinY);
        }
        
        // Middle-Left:
        if (query.X <= MinX && MinY <= query.Y && query.Y <= MaxY) {
            return Abs(MinX - query.X);
        }
        
        // Middle-Right
        if (MaxX <= query.X && MinY <= query.Y && query.Y <= MaxY) {
            return Abs(query.X - MaxX);
        }
        
        // Lower Left
        if (query.X < MinX && query.Y > MaxY)
        {
            return query.Distance(MinX, MaxY);
        }
        
        // Lower Middle
        if (MinX <= query.X && query.X <= MaxX && query.Y >= MaxY) {
            return Abs(Abs(query.Y) - Abs(MaxY));
        }
        
        // Lower Right
        return query.Distance(MaxX, MaxY);
    }
    

    
    public override string ToString()
    {
        return $"MinX: {MinX}, MinY: {MinY}, MaxX: {MaxX}, MaxY: {MaxY}";
    }
}