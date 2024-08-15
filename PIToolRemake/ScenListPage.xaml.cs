using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace PIToolRemake
{
    public class ScenListPageViewModel : INotifyPropertyChanged
    {
        private static ObservableCollection<Scenario> _scenList = [];
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Scenario> ScenList
        {
            get => _scenList;
            set
            {
                if (_scenList != value)
                {
                    _scenList = value;
                    OnPropertyChanged();
                }
            }
        }
        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class ScenListPage : ContentPage
    {
        private ScenListPageViewModel _viewModel;
        public ScenListPage()
        {
            InitializeComponent();
            _viewModel = new ScenListPageViewModel();
            foreach (var scenario in MauiProgram.Scenarios)
            {
                _viewModel.ScenList.Add(scenario);
            }
            BindingContext = _viewModel;
        }
        private void OnSortByID(object sender, EventArgs e)
        {
            _viewModel.ScenList = [.. _viewModel.ScenList.OrderBy(scenList => scenList.ScenarioID)];
            RefreshScenarioList();
        }
        private void RefreshScenarioList()
        {
            scenarioList.ItemsSource = null;
            scenarioList.ItemsSource = _viewModel.ScenList;
        }
        private void OnSortByConstant(object sender, EventArgs e)
        {
            _viewModel.ScenList = [.. _viewModel.ScenList.OrderByDescending(scenario => scenario.Constant)];
            RefreshScenarioList();
        }
        private void OnSortByPackage(object sender, EventArgs e)
        {
            _viewModel.ScenList = [.. _viewModel.ScenList.OrderByDescending(scenario => scenario.Package)];
            RefreshScenarioList();
        }
        private void OnSortByAuthor(object sender, EventArgs e)
        {
            _viewModel.ScenList = [.. _viewModel.ScenList.OrderByDescending(scenario => scenario.Author)];
            RefreshScenarioList();
        }
    }

}