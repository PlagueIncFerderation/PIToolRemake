using Npgsql;
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
        await MauiProgram.GetBestScoreAsync(_player.ID);
        foreach (var item in MauiProgram.Scores)
            _viewModel.ScoreList.Add(new ScenarioScore(item.Key));
        _viewModel.ScoreList = [.. _viewModel.ScoreList.OrderByDescending(item => item.IndividualPotential)];
        _viewModel.Player = _player;
        BindingContext = _viewModel;
    }
}
public class PersonalScorePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private static ObservableCollection<ScenarioScore> _scoreList = [];
    public ObservableCollection<ScenarioScore> ScoreList
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
}

