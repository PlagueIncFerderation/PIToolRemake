namespace PIToolRemake
{
    public class ScenarioScoreOfOnePlayer(int scenarioID, int score)
    {
        public int ScenarioID { get; private set; } = scenarioID;
        public string ScenarioName => MauiProgram.ScenarioDictionary.TryGetValue(ScenarioID, out var item) ? item.Name : string.Empty;
        public int Score {  get; private set; }= score;
        public float ScenarioConstant => MauiProgram.ScenarioDictionary.TryGetValue(ScenarioID, out var item) ? item.Constant : 0;
        public float IndividualPotential => CalculateSinglePTT(Score, ScenarioConstant);
        public string ScenarioFeatureString => ScenarioName + $"[{ScenarioConstant}]";
        public string CacheFilePath => MauiProgram.ScenarioDictionary.TryGetValue(ScenarioID, out var item) ? item.CacheFilePath : String.Empty;

        private static float CalculateSinglePTT(int score, float constant)
        {
            var ptt = score switch
            {
                >= 75000 => constant + 2,
                >= 55000 => constant + (score - 55000) / 10000,
                >= 30000 => constant - 5 + (score - 30000) / 5000,
                _ => constant - 8 + score / 10000,
            };
            return ptt > 0 && constant >= 1.0 ? ptt : 0;
        }
    }
}
