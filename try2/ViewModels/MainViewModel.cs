using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.ComponentModel;
using System.Timers;

namespace try2.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    private const double MainSignalConst = 5;
    private const double SubSignalConst = 5;
    private const Int32 TimerElapse = 50;
    private static double phase = 0;
    private static Timer mTimer;
    public static void Main()
    {
        mTimer = new Timer(TimerElapse);
        mTimer.Elapsed += OnTimedEvent;
        mTimer.AutoReset = true;
        mTimer.Enabled = true;
    }
    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        phase += double.Pi / 10;
        double actval = 0;
        if (_msCheck) actval += MainSignalConst * _msValue;
        if (_fCheck) actval += SubSignalConst * _fValue * Math.Sin(phase);
        double noise = 0;
        if (_nCheck)
        {
            Random random = new Random();
            for(int i = 0; i < 12; i++) { noise += -1 + 2 * random.Next(); }
            actval+= noise;
        }
        _Signal = actval;
        _Noise = noise;
    }

    private static double _Signal = 0;

    public double Signal
    {
        get { return _Signal; }
        set
        {
            _Signal = value;
            OnPropertyChanged(nameof(Signal));
        }
    }

    private static bool _msCheck = false;
    public bool msCheck
    {
        get { return _msCheck; }
        set
        {
            _msCheck = value;
            OnPropertyChanged(nameof(msCheck));
        }
    }

    private static bool _fCheck = false;
    public bool fCheck
    {
        get { return _fCheck; }
        set
        {
            _fCheck = value;
            OnPropertyChanged(nameof(fCheck));
        }
    }

    private static bool _nCheck = false;
    public bool nCheck
    {
        get { return _nCheck; }
        set
        {
            _nCheck = value;
            OnPropertyChanged(nameof(nCheck));
        }
    }

    private static double _msValue = 0;

    public double msValue
    {
        get { return _msValue; }
        set
        {
            _msValue = value;
            OnPropertyChanged(nameof(msValue));
        }
    }

    private static double _fValue = 0;

    public double fValue
    {
        get { return _fValue; }
        set
        {
            _fValue = value;
            OnPropertyChanged(nameof(fValue));
        }
    }

    private static double _nValue = 0;

    public double nValue
    {
        get { return _nValue; }
        set
        {
            _nValue = value;
            OnPropertyChanged(nameof(nValue));
        }
    }

    private static double _Noise = 0;

    public double Noise
    {
        get { return _Noise; }
        set
        {
            _Noise = value;
            OnPropertyChanged(nameof(Noise));
        }
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
