using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIToolRemake
{
    public static class Configs
    {
        public static string MyServer = "47.93.57.125";
        public static string MyLogin = "PEUser";
        public static string MyPass = "1145141919810";
        public static string MyDatabase = "postgres";
        public static string ConnectionStr = $"Host={MyServer};Username={MyLogin};Password={MyPass};Database={MyDatabase};Port=5432";
    }
}
