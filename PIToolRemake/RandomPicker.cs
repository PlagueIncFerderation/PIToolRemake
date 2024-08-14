namespace PIToolRemake
{
    public class RandomPicker
    {
        private static Random _random = new();

        public static T PickRandomElement<T>(List<T> list)
        {
            int randomIndex = _random.Next(list.Count);
            return list[randomIndex];
        }
    }
}
