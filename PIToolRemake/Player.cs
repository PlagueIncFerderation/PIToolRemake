namespace PIToolRemake
{
    public class Player
    {
        public int ID { get; set; } = 0;
        public string QQNumber { get; set; } = "";
        public string Nickname { get; set; } = "";
        public float Potential { get; set; } = 0f;
        public bool IsBlocked { get; set; } = false;
        public long TotalScore { get; set; } = 0L;
        public int Ranking { get; set; } = 0;
    }
}
