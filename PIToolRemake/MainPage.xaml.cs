namespace PIToolRemake
{

    public partial class MainPage : ContentPage
    {
        private bool ScenarioListIsEmpty()
        {
            if (MauiProgram.Scenarios.Count == 0)
            {
                string title = "PI Tool被玩坏了！";
                string msg = "这不是PI Tool的问题！绝对不是！" + Environment.NewLine + "请检查一下你的网络，如果确定网络正常，请报告屑乙烯！";
                string cancel = "确定";
                DisplayAlert(title, msg, cancel);
                return true;
            }
            else { return false; }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MauiProgram.GetScenarioListAsync();
            await MauiProgram.GetPackageListAsync();
            await MauiProgram.GetPlayerListAsync();
            await MauiProgram.GetScenarioImageAsync();
        }

        private void ScoreCalBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalculatorPage());
        }

        private void ScenListScoreBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonalScoreQueryPage());
        }

        private void ScenListRksBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void RandomScenBtn_Clicked(object sender, EventArgs e)
        {
            if (ScenarioListIsEmpty()) return;
            else
            {
                Scenario item = RandomPicker.PickRandomElement(MauiProgram.Scenarios);
                string feature = item.Feature switch
                {
                    1 => "[Buster]",
                    2 => "[Arts]",
                    3 => "[Buster][Arts]",
                    4 => "[Study]",
                    5 => "[Buster][Study]",
                    6 => "[Arts][Study]",
                    7 => "[Buster][Arts][Study]",
                    0 => "[Balanced]",
                    _ => "[Balanced]",
                };
                string message1 = $"[{item.Constant}]";
                string message2 = item.ScenarioName;
                string message3 = $"By {item.Author}";
                string msg = string.Join(separator: Environment.NewLine, message2 + " " + message1, message3, feature);
                DisplayAlert("您随机到的场景是", msg, "OK");
            }
        }

        private void ScenListBtn_Clicked(object sender, EventArgs e)
        {
            if (ScenarioListIsEmpty()) return;
            else Navigation.PushAsync(new ScenListPage());
        }

        private void AuthorBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AuthorPage());
        }
    }

}
