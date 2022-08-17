namespace IssueTrackerAPI.Utils
{
    public class RandomMaker
    {
        private static Random _random = new Random();
        public static int Next(int value)
        {
            return _random.Next(value);
        }
    }
}
