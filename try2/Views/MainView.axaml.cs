using Avalonia.Controls;
using Avalonia.Interactivity;

namespace try2.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    int counter = 0;
    int counter2 = 0;
    public void ClickHandler(object sender, RoutedEventArgs args)
    {
        Tumblerstate.Text = tumbler.IsChecked.ToString();
    }

  
}
