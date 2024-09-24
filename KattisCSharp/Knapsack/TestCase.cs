namespace KattisCSharp.Knapsack;

public class TestCase
{
    internal int cap { get; }
    internal int num_objects { get; }
    internal int[] values { get; }
    internal int[] weights { get; }

    public TestCase(int cap, int num_objects, int[] values, int[] weights)
    {
        this.cap = cap;
        this.num_objects = num_objects;
        this.values = values;
        this.weights = weights;
    }
}