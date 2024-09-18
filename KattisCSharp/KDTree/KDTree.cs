namespace KattisCSharp.KDTree;

public class KDTree
{
    private readonly KDNode _root;

    public KDTree(List<Point> points)
    {
        _root = new KDBranch(points, sortX: true);
    }
    
    public Point NearestNeighbourSearch(Point query) 
    {
        var closestPoint = new Point(99999.0, 99999.0);
        const bool unique = false;
        var package = (closestPoint, unique);
        _root.NearestNeighbourSearch(query, ref package);
        return package.closestPoint;
    }
}