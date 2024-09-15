namespace KattisCSharp;

public abstract class KDNode
{
    protected BoundingBox boundingBox;
    public abstract BoundingBox GetBounds();
    public abstract (Point closestPoint, bool unique) NearestNeighbourSearch(Point query, ref (Point closestPoint, bool unique) package);
}