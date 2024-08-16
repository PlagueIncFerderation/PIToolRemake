namespace PIToolRemake
{
    public class Scenario
    {
        public int ScenarioID { get; set; } = 0;
        public string ScenarioName { get; set; } = "";
        public float ScoreMultiplier { get; set; } = 0f;
        public float Constant { get; set; } = 0f;
        public string Author { get; set; } = "";
        public int Feature { get; set; } = 0;
        public string Package { get; set; } = "";
        public string ImageUrl => $"http://47.241.0.156:34567/WebAPI/public/icons/{ScenarioID}.png";
        public string CacheFilePath => Path.Combine(FileSystem.AppDataDirectory, $"{ScenarioID}.png");
    }

}
