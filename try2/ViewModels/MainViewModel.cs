using Avalonia.Interactivity;
using System.ComponentModel;

namespace try2.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    private bool _CheckState;
    public bool CheckState
    {
        get { return _CheckState; }
        set
        {
            _CheckState = value;
            OnPropertyChanged(nameof(CheckState));
        }
    }

    private double _SliderValProperty = 25;
    public double SliderVal
    {
        get { return _SliderValProperty; }
        set
        {
            _SliderValProperty = value;
            OnPropertyChanged(nameof(SliderVal));
        }
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
