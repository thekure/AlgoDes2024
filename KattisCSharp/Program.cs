/*
using KattisCSharp;
using static System.Console;

var input = InputHandler.ReadInputFromStandardInput();
var points = InputHandler.ParsePoints(input);
var tree = new KDTree(points);

var point1 = new Point(-999999, 1111);
var point2 = new Point(-999999, 1111);
double dist = 999999;

foreach (var point in points)
{ 
    var result = tree.NearestNeighbourSearch(point);
    var newDist = point.Distance(result);
    if (newDist < dist)
    {
        point1 = point;
        point2 = result;
        dist = newDist;
    }
}

WriteLine(point1);
WriteLine(point2);
*/
