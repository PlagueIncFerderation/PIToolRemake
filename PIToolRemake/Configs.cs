namespace PIToolRemake
{
    public static class Configs
    {
        public static string MyServer = "47.93.57.125";
        public static string MyLogin = "Reader";
        public static string MyPass = "reader";
        public static string MyDatabase = "postgres";
        public static string ConnectionStr = $"Host={MyServer};Username={MyLogin};Password={MyPass};Database={MyDatabase};Port=5432";
    }
}
