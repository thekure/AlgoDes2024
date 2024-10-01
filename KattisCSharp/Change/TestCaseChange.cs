namespace KattisCSharp.Change;

public class TestCaseChange
{
    internal int price { get; }
    internal int num_moneys { get; }
    internal int[] denoms { get; }

    public TestCaseChange(int price, int num_moneys, int[] denoms)
    {
        this.price = price;
        this.num_moneys = num_moneys;
        this.denoms = denoms;
    }
}