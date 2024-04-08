using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Timers;

namespace try2.ViewModels;

public class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    static MainViewModel It;
    public MainViewModel()
    {
        Main();
        It = this;
    }



    private const double MainSignalConst = 5;
    private const double SubSignalConst = 1;
    private const double NoiseSmoothing = 0.05;
    private const Int32 TimerElapse = 30;
    private static double _phase = 0;
    private static string _phasestr = "";
    private static DateTime StartTime = DateTime.Now;
    private static DateTime GaussStartTime = DateTime.Now;
    private static Timer mTimer;
    private static Timer noiseTimer;
    public static void Main()
    {
        mTimer = new Timer(TimerElapse);
        mTimer.Elapsed += OnTimedGaussSignalEvent;
        mTimer.Elapsed += OnTimedSinSignalEvent;
        mTimer.Elapsed += OnTimedFinalSignalEvent;
        mTimer.AutoReset = true;
        mTimer.Enabled = true;

        noiseTimer = new Timer(_noiseTimerElapse);
        noiseTimer.Elapsed += OnTimedNoiseEvent;
        noiseTimer.AutoReset = true;
        noiseTimer.Enabled = true;
    }

    private static double _GaussAvg = 0.5;
    public double GaussAvg
    {
        get { return _GaussAvg; }
        set
        {
            _GaussAvg = value;
            OnPropertyChanged(nameof(GaussAvg));
        }
    }
    public string Phase
    {
        get { return _phasestr; }
        set { _phasestr = value; OnPropertyChanged(nameof(Phase));}
    }

    static string _timefromstart = "";
    public string TimeFromStart
    {
        get { return _timefromstart; }
        set { _timefromstart = value; OnPropertyChanged(nameof(TimeFromStart)); }
    }

    const double sigma = 0.1;
    protected static double Gauss(double t)
    {
        return Math.Exp(-(t - _GaussAvg / 2) *(t - _GaussAvg / 2) / 2 / sigma/sigma);
    }
    private static void OnTimedGaussSignalEvent(Object source, ElapsedEventArgs e)
    {
        double psignal = 0;
        TimeSpan ts = DateTime.Now.Subtract(GaussStartTime);
        double dt = ts.TotalSeconds;
        if (_msCheck) psignal = MainSignalConst * _msValue * Gauss(dt);
        if (dt > _GaussAvg) GaussStartTime = DateTime.Now.AddSeconds(dt - _GaussAvg);
        It.PureSignal = psignal;
    }
    private static void OnTimedSinSignalEvent(Object source, ElapsedEventArgs e)
    {
        double pssignal = 0;
        TimeSpan ts = DateTime.Now.Subtract(StartTime);
        double t = ts.TotalSeconds;
        if (_fCheck) pssignal = SubSignalConst * _SinValue * Math.Sin(2 * double.Pi * _fValue * t);
        It.PureSinSignal = pssignal;
    }
    private static void OnTimedNoiseEvent(Object source, ElapsedEventArgs e)
    {
        double noise = 0;
        if (_nCheck)
        {
            Random random = new Random();
            for (int i = 0; i < 12; i++) { noise += -1 + 2 * random.NextDouble(); }
            noise *= _nValue / 12;
        }
        It.Noise = noise;
    }
    private static void OnTimedFinalSignalEvent(Object source, ElapsedEventArgs e)
    {
        It.Signal = It.PureSignal + It.PureSinSignal + It.Noise;
    }

    private static Int64 _noiseTimerElapse = 100;
    public Int64 NoiseTimerElapse
    {
        get { return _noiseTimerElapse; }
        set 
        { 
            _noiseTimerElapse = value;
            noiseTimer.Interval = _noiseTimerElapse;
            OnPropertyChanged(nameof(NoiseTimerElapse)); 
        }
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

    private static double _msValue = 1;

    public double msValue
    {
        get { return _msValue; }
        set
        {
            _msValue = value;
            OnPropertyChanged(nameof(msValue));
        }
    }

    private static double _fValue = 1;

    public double fValue
    {
        get { return _fValue; }
        set
        {
            _fValue = value;
            OnPropertyChanged(nameof(fValue));
        }
    }

    private static double _nValue = 1;

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

    private static double _PureSignal = 0;

    public double PureSignal
    {
        get { return _PureSignal; }
        set
        {
            _PureSignal = value;
            OnPropertyChanged(nameof(PureSignal));
        }
    }

    private static double _PureSinSignal = 0;

    public double PureSinSignal
    {
        get { return _PureSinSignal; }
        set
        {
            _PureSinSignal = value;
            OnPropertyChanged(nameof(PureSinSignal));
        }
    }

    private static double _SinValue = 1;
    public double SinValue
    {
        get { return _SinValue; }
        set
        {
            _SinValue = value;
            OnPropertyChanged(nameof(SinValue));
        }
    }

    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
