namespace PIToolRemake
{

    public partial class PersonalScoreQueryPage : ContentPage
    {
        public PersonalScoreQueryPage()
        {
            InitializeComponent();
        }

        private void OnScoreQuery(object sender, EventArgs e)
        {
            string qqNumber = QQNumberEntry.Text;
            if (string.IsNullOrEmpty(qqNumber))
                DisplayAlert("你尚未输入你的QQ号！", "请输入你的QQ号！", "OK");
            else if (MauiProgram.Players.TryGetValue(qqNumber, out var player))
            {
                if (player.IsBlocked == false)
                    Navigation.PushAsync(new PersonalScorePage(player));
                else
                    DisplayAlert("该用户封禁中！", "如果对自己账户状态有疑问，请咨询管理员！", "OK");
            }
            else
                DisplayAlert("该用户不存在！", "请在pi加盟群进行注册！" + Environment.NewLine + "如果对自己用户状态有疑问，请咨询管理员！", "OK");

        }
    }
}