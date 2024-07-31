using Microsoft.Maui.Controls;
using PIToolRemake;
using System.ComponentModel;

namespace PIToolRemake
{
    public partial class CalculatorPage : ContentPage
    {

        public CalculatorPage()
        {
            InitializeComponent();
            BindingContext = new CalculatePageViewModel();
        }

        private void CalculateBtn_Clicked(object sender, EventArgs e)
        {
            try

            {
                int d = int.Parse(Days.Text);
                float c = float.Parse(Cure.Text);
                float m = float.Parse(Multiplier.Text);

                int s = (int)(20000000 * (4 - c) * (1 + m) / (3 * d));

                ((CalculatePageViewModel)BindingContext).Score = s;
            }
            catch
            {
                ((CalculatePageViewModel)BindingContext).Score = 0;
            }
        }
    }

    public class CalculatePageViewModel : INotifyPropertyChanged
    {
        private int _score = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}