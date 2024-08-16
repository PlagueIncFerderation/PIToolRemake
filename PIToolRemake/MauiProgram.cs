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
        public static Dictionary<int, ScenarioScoreOfOnePlayer> ScoreListOfOnePlayer { get; set; } = [];
        public static Dictionary<int, ScenarioScoreOfOneScenario> ScoreListOfOneScenario { get; set; } = [];
        public static Dictionary<int, string> UserIDToUserQQ => Players.ToDictionary(pair => pair.Value.ID, pair => pair.Key);

        public static async Task GetScenarioListAsync()
        {
            string query = "SELECT scenarioid, scenarioname, multiplier, constant, author, packid, feature FROM public.scenario";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    Scenario scenario = new Scenario
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("scenarioid")),
                        Name = reader.GetString(reader.GetOrdinal("scenarioname")),
                        ScoreMultiplier = reader.GetFloat(reader.GetOrdinal("multiplier")),
                        Author = reader.GetString(reader.GetOrdinal("author")),
                        Constant = reader.GetFloat(reader.GetOrdinal("constant")),
                        Feature = reader.GetInt16(reader.GetOrdinal("feature")),
                        Package = reader.GetString(reader.GetOrdinal("packid"))
                    };
                    Scenarios.Add(scenario);
                }
            }
            ScenarioDictionary = Scenarios.ToDictionary(item => item.ID, item => item);
        }

        public static async Task GetPackageListAsync()
        {
            string query = "SELECT packageid, packagename FROM public.packagename";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
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

        public static async Task GetScoresOfOnePlayerAsync(int userID)
        {
            ScoreListOfOnePlayer.Clear();
            string query = "SELECT scenarioid, score, rating FROM public.score WHERE userid=@userid";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.Add("@userid", NpgsqlDbType.Integer).Value = userID;
            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("scenarioid"));
                    int score = reader.GetInt32(reader.GetOrdinal("score"));
                    int ranking = reader.GetInt32(reader.GetOrdinal("rating"));
                    ScoreListOfOnePlayer.Add(id, new ScenarioScoreOfOnePlayer(id,score,ranking));
                }
            }
        }

        public static async Task GetScoresOfOneScenarioAsync(int scenarioID)
        {
            ScoreListOfOneScenario.Clear();
            ScenarioScoreOfOneScenario.Constant = ScenarioDictionary.TryGetValue(scenarioID, out var item) ? item.Constant : 0;
            string query = "SELECT userid, score, rating FROM public.score WHERE scenarioid=@scenarioid";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.Add("@scenarioid", NpgsqlDbType.Integer).Value = scenarioID;
            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("userid"));
                    int score = reader.GetInt32(reader.GetOrdinal("score"));
                    int ranking = reader.GetInt32(reader.GetOrdinal("rating"));
                    ScoreListOfOneScenario.Add(id, new ScenarioScoreOfOneScenario(id, score, ranking));
                }
            }
        }

        public static async Task GetPlayerListAsync()
        {
            string query = "SELECT userid, qqnumber, nickname, potential, banned, scoresum, rank FROM public.player";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
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

        public static async Task GetScenarioImageAsync()
        {
            using var client = new HttpClient();
            foreach (var scenario in Scenarios)
            {
                string imageUrl = scenario.ImageUrl;
                string filePath = scenario.CacheFilePath;
                var response = await client.GetAsync(imageUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(filePath, content);
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
