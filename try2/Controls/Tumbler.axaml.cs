using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections;

namespace try2.Controls
{
   
    public partial class Tumbler : UserControl
    {
        Bitmap OnImage;
        Bitmap OffImage;

        bool ischecked = false;
        public bool IsChecked
        {
            set
            {
                ischecked = value;
                if (ischecked) TumblerIcon.Source = OnImage;
                else TumblerIcon.Source = OffImage;
            }
            get
            {
                return ischecked;
            }
        }

        public Tumbler()
        {
            InitializeComponent();
            OnImage = new Bitmap(AssetLoader.Open(new Uri("avares://try2/Assets/TumblerOn.png")));
            OffImage = new Bitmap(AssetLoader.Open(new Uri("avares://try2/Assets/TumblerOff.png")));
            IsChecked = false;
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            IsChecked = !IsChecked;
        }
    }
}
