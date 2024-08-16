using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PIToolRemake;

public partial class PersonalScorePage : ContentPage
{
    private Player _player;
    private PersonalScorePageViewModel _viewModel;
    public PersonalScorePage(Player player)
    {
        InitializeComponent();
        _player = player;
        _viewModel = new PersonalScorePageViewModel();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await MauiProgram.GetScoresOfOnePlayerAsync(_player.ID);
        foreach (var item in MauiProgram.ScoreListOfOnePlayer)
            _viewModel.ScoreList.Add(item.Value);
        _viewModel.ScoreList = [.. _viewModel.ScoreList.OrderByDescending(item => item.IndividualPotential)];
        _viewModel.Player = _player;
        BindingContext = _viewModel;
        string imageUrl = "https://q.qlogo.cn/g?b=qq&nk=" + _player.QQNumber + "&s=100";
        using var client = new HttpClient();
        string filePath = Path.Combine(FileSystem.AppDataDirectory, $"head.png");
        var response = await client.GetAsync(imageUrl);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(filePath, content);
            _viewModel.FilePath = filePath;
        }
        else
            await DisplayAlert("错误发生", "未能拉取QQ头像！", "OK");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        File.Delete(_viewModel.FilePath);
    }
}
public class PersonalScorePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private static ObservableCollection<ScenarioScoreOfOnePlayer> _scoreList = [];
    public ObservableCollection<ScenarioScoreOfOnePlayer> ScoreList
    {
        get { return _scoreList; }
        set { _scoreList = value; OnPropertyChanged(); }
    }

    private static Player player = new();
    public Player Player
    {
        get { return player; }
        set { player = value; OnPropertyChanged(); }
    }

    private static string filePath = string.Empty;
    public string FilePath
    {
        get => filePath; set { filePath = value; OnPropertyChanged(); }
    }
}

