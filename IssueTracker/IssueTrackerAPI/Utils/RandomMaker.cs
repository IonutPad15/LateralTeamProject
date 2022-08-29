namespace IssueTrackerAPI.Utils;

public class RandomMaker
{
    private static readonly Random Random = new Random();
    public static int Next(int value)
    {
        return Random.Next(value);
    }
}
