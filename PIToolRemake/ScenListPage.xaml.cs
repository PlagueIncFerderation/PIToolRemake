using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;

namespace PIToolRemake
{
    public class Scenario
    {
        private int _scenarioID = 0;
        private string _scenarioName = "";
        private float _scoreMultiplier = 0f;
        private int _scenarioType = 0;//Reborn;Novice;Origin;Alpha;Omega;
        private float _constant = 0f;
        private string _author = "";
        private int _feature = 0;//[Buster][Arts][Study][Balanced]
        private string _package = "";//packageDisplayed;
        public int ScenarioID
        {
            get => _scenarioID;
            set
            {
                if (_scenarioID != value)
                {
                    _scenarioID = value;
                }
            }
        }
        public string ScenarioName
        {
            get => _scenarioName;
            set
            {
                if (_scenarioName != value)
                {
                    _scenarioName = value;
                }
            }
        }
        public float ScoreMultiplier
        {
            get => _scoreMultiplier;
            set
            {
                if (value != _scoreMultiplier)
                {
                    _scoreMultiplier = value;
                }
            }
        }
        public int ScenarioType
        {
            get => _scenarioType;
            set
            {
                if (value != _scenarioType)
                {
                    _scenarioType = value;
                }
            }
        }
        public float Constant
        {
            get => _constant;
            set
            {
                if (value != _constant)
                {
                    _constant = value;
                }
            }
        }
        public string Author
        {
            get { return _author; }
            set
            {
                if (value != _author)
                {
                    _author = value;
                }
            }
        }
        public int Feature
        {
            get { return _feature; }
            set
            {
                if (value != _feature)
                {
                    _feature = value;
                }
            }
        }
        public string Package
        {
            get { return _package; }
            set
            {
                if (value != _package)
                {
                    _package = value;
                }
            }
        }
    }

    public static class Configs
    {
        public static string MyServer = "47.93.57.125";
        public static string MyLogin = "PEUser";
        public static string MyPass = "1145141919810";
        public static string MyDatabase = "postgres";
        public static string ConnectionStr = $"Host={MyServer};Username={MyLogin};Password={MyPass};Database={MyDatabase};Port=5432";
    }
    public class ScenListPageViewModel : INotifyPropertyChanged
    {
        private static ObservableCollection<Scenario> _scenList = [];
        public event PropertyChangedEventHandler PropertyChanged;
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
        protected virtual void OnPropertyChanged(string propertyName = null)
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
    public class FeatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue switch
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
            }
            return "[Null]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue switch//Reborn;Novice;Origin;Alpha;Omega;
                {
                    0 => "Reborn",
                    1 => "Novice",
                    2 => "Origin",
                    3 => "Alpha",
                    4 => "Omega",
                    _ => "Origin"
                };
            }
            return "Origin";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}