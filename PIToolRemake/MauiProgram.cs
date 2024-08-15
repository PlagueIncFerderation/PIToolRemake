using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace PIToolRemake
{
    public static class MauiProgram
    {
        public static Dictionary<string, string> Packages { get; set; } = [];
        public static List<Scenario> Scenarios { get; set; } = [];
        public static Dictionary<int, Scenario> ScenarioDictionary { get; set; } = [];
        public static Dictionary<string, Player> Players { get; set; } = [];
        public static Dictionary<int, KeyValuePair<int, int>> Scores { get; set; } = [];// <scenarioID, <score, ranking>>

        public static async Task GetScenarioListAsync()
        {
            string query = "SELECT scenarioid, scenarioname, multiplier, constant, author, packid, feature FROM public.scenario";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            using var reader =await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Scenario scenario = new Scenario
                    {
                        ScenarioID = reader.GetInt32(reader.GetOrdinal("scenarioid")),
                        ScenarioName = reader.GetString(reader.GetOrdinal("scenarioname")),
                        ScoreMultiplier = reader.GetFloat(reader.GetOrdinal("multiplier")),
                        Author = reader.GetString(reader.GetOrdinal("author")),
                        Constant = reader.GetFloat(reader.GetOrdinal("constant")),
                        Feature = reader.GetInt16(reader.GetOrdinal("feature")),
                        Package = reader.GetString(reader.GetOrdinal("packid"))
                    };
                    Scenarios.Add(scenario);
                }
            }
            ScenarioDictionary = Scenarios.ToDictionary(item => item.ScenarioID, item => item);
        }

        public static async Task GetPackageListAsync()
        {
            string query = "SELECT packageid, packagename FROM public.packagename";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query,connection);
            using var reader =await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string pkgID = reader.GetString(reader.GetOrdinal("packageid"));
                    string pkgName = reader.GetString(reader.GetOrdinal("packagename"));
                    Packages.Add(pkgID, pkgName);
                }
            }
        }

        public static async Task GetBestScoreAsync(int userid)
        {
            string query = "SELECT scenarioid, score, rating FROM public.score WHERE userid=@userid";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query,connection);
            command.Parameters.Add("@param", NpgsqlDbType.Integer).Value = userid;
            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("scenarioid"));
                    int score = reader.GetInt32(reader.GetOrdinal("score"));
                    int ranking = reader.GetInt32(reader.GetOrdinal("rating"));
                    var kvp = new KeyValuePair<int, int>(score, ranking);
                    Scores.Add(id, kvp);
                }
            }
        }

        public static async Task GetPlayerListAsync()
        {
            string query = "SELECT userid, qqnumber, nickname, potential, banned, scoresum, rank FROM public.player";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query,connection);
            using var reader =await command.ExecuteReaderAsync();
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Player player = new()
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("userid")),
                            IsBlocked = reader.GetBoolean(reader.GetOrdinal("banned")),
                            Nickname = reader.GetString(reader.GetOrdinal("nickname")),
                            Potential = reader.GetFloat(reader.GetOrdinal("potential")),
                            QQNumber = reader.GetString(reader.GetOrdinal("qqnumber")),
                            Ranking = reader.GetInt32(reader.GetOrdinal("rank")),
                            TotalScore = reader.GetInt64(reader.GetOrdinal("scoresum"))
                        };
                        Players.Add(player.QQNumber, player);
                    }
                }
            }
        }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
