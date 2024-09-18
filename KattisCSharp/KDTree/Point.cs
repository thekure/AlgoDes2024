using System.Globalization;
using static System.Math;

namespace KattisCSharp.KDTree;

public class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y){
        X = x;
        Y = y;
    }

    public double Distance(Point other){
        return Sqrt(Pow(X - other.X, 2) + Pow(Y - other.Y, 2));
    }
    
    public double Distance(double otherX, double otherY){
        return Sqrt(Pow(X - otherX, 2) + Pow(Y - otherY, 2));
    }

    public override string ToString(){
        return $"{X.ToString(CultureInfo.InvariantCulture)} {Y.ToString(CultureInfo.InvariantCulture)}";
    }
}