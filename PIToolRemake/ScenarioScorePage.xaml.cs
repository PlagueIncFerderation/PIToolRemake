using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PIToolRemake;

public partial class ScenarioScorePage : ContentPage
{
    private Scenario _scenario;
    private ScenarioScorePageViewModel _viewModel;

    public ScenarioScorePage(Scenario scenario)
    {
        InitializeComponent();
        _scenario = scenario;
        _viewModel = new ScenarioScorePageViewModel();
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await MauiProgram.GetScoresOfOneScenarioAsync(_scenario.ID);
        foreach (var item in MauiProgram.ScoreListOfOneScenario) 
            _viewModel.ScoreList.Add(item.Value);
        _viewModel.ScoreList = [.. _viewModel.ScoreList.OrderBy(item => item.Ranking)];
        _viewModel.Scenario = _scenario;
        BindingContext = _viewModel;
        using var client = new HttpClient();
        foreach (var item in _viewModel.ScoreList)
        {
            string qq = MauiProgram.UserIDToUserQQ[item.UserID];
            string imageUrl = "https://q.qlogo.cn/g?b=qq&nk=" + qq+ "&s=100";
            string filePath = item.CachePath;
            var response = await client.GetAsync(imageUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(filePath, content);
            }
            else
                continue;
        }
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}

public class ScenarioScorePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private ObservableCollection<ScenarioScoreOfOneScenario> _scoreList=[];
    public ObservableCollection<ScenarioScoreOfOneScenario> ScoreList
    {
        get { return _scoreList; }
        set { _scoreList = value; OnPropertyChanged(nameof(ScoreList)); }
    }

    private static Scenario _scenario=new();
    public Scenario Scenario
    {
        get { return _scenario; }
        set { _scenario = value; OnPropertyChanged(nameof(Scenario)); }
    }
}