using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

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
            if(string.IsNullOrEmpty(qqNumber))
                DisplayAlert("����δ�������QQ�ţ�", "���������QQ�ţ�", "OK");
            else if(MauiProgram.Players.TryGetValue(qqNumber, out var player))
            {
                if (player.IsBlocked == false)
                    Navigation.PushAsync(new PersonalScorePage(player.ID));
                else
                    DisplayAlert("���û�����У�", "������Լ��˻�״̬�����ʣ�����ѯ����Ա��", "OK");
            }
            else
                DisplayAlert("���û������ڣ�", "����pi����Ⱥ����ע�ᣡ" + Environment.NewLine + "������Լ��û�״̬�����ʣ�����ѯ����Ա��", "OK");

        }
    }
}