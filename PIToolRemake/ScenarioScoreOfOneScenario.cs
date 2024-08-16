using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIToolRemake
{
    public class ScenarioScoreOfOneScenario(int userID, int score, int ranking)
    {
        public int UserID { get; private set; } = userID;
        public int Score { get; private set; } = score;
        public int Ranking { get; private set; } = ranking;
        public static float Constant { private get; set; }
        public float Potential => CalculateSinglePTT(Score, Constant);
        public string CachePath => Path.Combine(FileSystem.AppDataDirectory, $"{UserID}qqHead.png");
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
