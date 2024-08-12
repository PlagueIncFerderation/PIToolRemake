using Microsoft.Extensions.Logging;
using Npgsql;

namespace PIToolRemake
{
    public static class MauiProgram
    {
        public static List<Scenario> Scenarios=[];

        public static async Task GetScenarioList()
        {
            string query = "SELECT scenarioid, scenarioname, multiplier, constant, authordisplayed, packdisplayed, feature, type FROM public.scenario";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query);
            using var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Scenario scenario = new Scenario
                    {
                        ScenarioID = reader.GetInt32(reader.GetOrdinal("scenarioid")),
                        ScenarioName = reader.GetString(reader.GetOrdinal("scenarioname")),
                        ScoreMultiplier = reader.GetFloat(reader.GetOrdinal("multiplier")),
                        ScenarioType = reader.GetInt32(reader.GetOrdinal("type")),
                        Author = reader.GetString(reader.GetOrdinal("authordisplayed")),
                        Constant = reader.GetFloat(reader.GetOrdinal("constant")),
                        Feature = reader.GetInt16(reader.GetOrdinal("feature")),
                        Package = reader.GetString(reader.GetOrdinal("packdisplayed"))
                    };
                    Scenarios.Add(scenario);
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
