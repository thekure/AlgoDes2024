namespace KattisCSharp;

using System.Collections.Generic;

public class KDBranch : KDNode
{
    private readonly KDNode _left;
    private readonly KDNode _right;

    public KDBranch(List<Point> points, bool sortX) {
        if (sortX) SortByX(points);
        else SortByY(points);
        
        var leftList = points.GetRange(0, points.Count / 2);
        var rightList = points.GetRange(points.Count / 2, points.Count - points.Count / 2);
        

        if (points.Count > 50)
        {
            _left = new KDBranch(leftList, !sortX);
            _right = new KDBranch(rightList, !sortX);
        }
        else
        {
            _left = new KDLeaf(leftList);
            _right = new KDLeaf(rightList);
        }

        boundingBox = _left.GetBounds().MergeBoundingBoxes(_right.GetBounds());
    }

    private static void SortByX(List<Point> points) {
        points.Sort((p1, p2) => p1.X.CompareTo(p2.X));
    }
    
    private static void SortByY(List<Point> points) {
        points.Sort((p1, p2) => p1.Y.CompareTo(p2.Y));
    }

    public override BoundingBox GetBounds()
    {
        return boundingBox;
    }

    public override (Point closestPoint, bool unique) NearestNeighbourSearch(Point query, ref (Point closestPoint, bool unique) package)
    {
        
        var distToLeft = _left.GetBounds().GetDistanceToBox(query);
        var distToRight = _right.GetBounds().GetDistanceToBox(query);
        var distClosestToQuery = package.closestPoint.Distance(query);
        
        /*
         * The following checks to see which subtree to search first if both are
         * eligible. Searching the closest box first, enables the search to potentially
         * prune the other subtree after the search, reducing the amount of trees that
         * has to be searched.
         */
        var leftContainsQ = _left.GetBounds().ContainsPoint(query);
        var rightContainsQ = _right.GetBounds().ContainsPoint(query);

        switch (leftContainsQ)
        {
            case true when rightContainsQ:
            // Query is in both children: Search both no matter what.
                _left.NearestNeighbourSearch(query, ref package);
                _right.NearestNeighbourSearch(query, ref package);
                break;
            
            case true:
            // Query is only in the left child: Start by searching left, and then check
            // if right can be pruned. If not, search right after.
            {
                _left.NearestNeighbourSearch(query, ref package);
                distClosestToQuery = package.closestPoint.Distance(query);
                if (distToRight < distClosestToQuery)
                {
                    _right.NearestNeighbourSearch(query, ref package);
                }
                break;
            }
            default:
            {
                if (rightContainsQ)
                // Query is only in the right child: Start by searching right, and then check
                // if left can be pruned. If not, search left after.
                {
                    _right.NearestNeighbourSearch(query, ref package);
                    distClosestToQuery = package.closestPoint.Distance(query);
                    if (distToLeft < distClosestToQuery)
                    {
                        _left.NearestNeighbourSearch(query, ref package);
                    }
                }
                else
                // Query is in neither child: Search nearest child, but only if the distance to that child
                // is not greater than the distance to the currently found closest point.
                {
                    if (distToLeft < distToRight && distToLeft < distClosestToQuery)
                    {
                        _left.NearestNeighbourSearch(query, ref package);
                        distClosestToQuery = package.closestPoint.Distance(query);
                        if (distToRight < distClosestToQuery)
                        {
                            _right.NearestNeighbourSearch(query, ref package);
                        }
                    } else if (distToRight < distToLeft && distToRight < distClosestToQuery)
                    {
                        _right.NearestNeighbourSearch(query, ref package);
                        distClosestToQuery = package.closestPoint.Distance(query);
                        if (distToLeft < distClosestToQuery)
                        {
                            _left.NearestNeighbourSearch(query, ref package);
                        }
                    }
                }

                break;
            }
        }

        return package;
    }
}