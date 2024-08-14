using Microsoft.Extensions.Logging;
using Npgsql;

namespace PIToolRemake
{
    public static class MauiProgram
    {
        public static List<Scenario> Scenarios=[];
        public static Dictionary<string, string> Packages = new();

        public static async Task GetScenarioListAsync()
        {
            string query = "SELECT scenarioid, scenarioname, multiplier, constant, author, packid, feature FROM public.scenario";
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
                        Author = reader.GetString(reader.GetOrdinal("author")),
                        Constant = reader.GetFloat(reader.GetOrdinal("constant")),
                        Feature = reader.GetInt16(reader.GetOrdinal("feature")),
                        Package = reader.GetString(reader.GetOrdinal("packid"))
                    };
                    Scenarios.Add(scenario);
                }
            }
        }

        public static async Task GetPackagesAsync()
        {
            string query = "SELECT packageid, packagename FROM public.packagename";
            using var connection = new NpgsqlConnection(Configs.ConnectionStr);
            await connection.OpenAsync();
            using var command = new NpgsqlCommand(query);
            using var reader = command.ExecuteReader();
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
