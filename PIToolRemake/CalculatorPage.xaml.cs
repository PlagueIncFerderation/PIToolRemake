using System.ComponentModel;

namespace PIToolRemake
{
    public partial class CalculatorPage : ContentPage
    {
        private CalculatePageViewModel _viewModel = new();
        public CalculatorPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        private void CalculateBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                int d = int.Parse(Days.Text);
                float c = float.Parse(Cure.Text);
                float m = float.Parse(Multiplier.Text);
                int s = (int)(20000000 * (4 - c) * (1 + m) / (3 * d));
                _viewModel.Score = s;
            }
            catch (Exception ex)
            {
                DisplayAlert("错误发生", ex.Message + Environment.NewLine + "如果你不能看懂上述文本，请咨询翻译软件或屑乙烯", "OK");
                _viewModel.Score = 0;
            }
        }
    }

    public class CalculatePageViewModel : INotifyPropertyChanged
    {
        private int _score = 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }
        protected virtual void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}