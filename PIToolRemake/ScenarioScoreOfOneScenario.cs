using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIToolRemake
{
    public class ScenarioScoreOfOneScenario(int userID, int score, float ranking)
    {
        public int UserID { get; private set; } = userID;
        public int Score { get; private set; } = score;
        public int Ranking { get; set; } 
        public float Potential { get; private set; } = ranking;
        public string QQNumber => MauiProgram.UserIDToUserQQ[UserID];
        public string Nickname => MauiProgram.Players[QQNumber].Nickname;
        public string CachePath => Path.Combine(FileSystem.AppDataDirectory, $"{UserID}qqHead.png");
    }
}
