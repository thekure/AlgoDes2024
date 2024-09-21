namespace KattisCSharp.KDTree;

public class KDLeaf : KDNode
{
    private readonly List<Point> _points;
    public KDLeaf(List<Point> points)
    {
        _points = points;
        boundingBox = new BoundingBox(_points);
    }

    public override BoundingBox GetBounds()
    {
        return boundingBox;
    }

    public override (Point closestPoint, bool unique) NearestNeighbourSearch(Point query, ref (Point closestPoint, bool unique) package)
    {
        foreach (var point in _points)
        {
            var distToQ = point.Distance(query);
            var curDist = package.closestPoint.Distance(query);
            if (!(distToQ < curDist)) continue;
            package = distToQ switch
            {
                0 when !package.unique => (package.closestPoint, true),
                0 when package.unique => (point, package.unique),
                _ => (point, package.unique)
            };
        }
        return package;
    }
}