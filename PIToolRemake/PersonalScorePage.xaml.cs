using System.ComponentModel;

namespace PIToolRemake;

public partial class PersonalScorePage : ContentPage
{
    private int _id = 0;
    public PersonalScorePage(int id)
    {
        InitializeComponent();
        _id = id;
    }

    protected async override void OnAppearing()
    {
        await MauiProgram.GetBestScoreAsync(_id);
    }
    
}
public class PersonalScorePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}