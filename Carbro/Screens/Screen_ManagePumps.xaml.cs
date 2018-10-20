using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Carbro.Screens
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManagePumps : Page
    {
        public ManagePumps()
        {
            this.InitializeComponent();
        }

        private void ModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                string buttonName = rb.Content.ToString();
                switch (buttonName)
                {
                    case "Manual":
                        break;
                    case "Calibrate":
                        stop.IsChecked = true;
                        break;
                }
            }
        }

        private void BottleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                string buttonName = rb.Name.ToString();
                switch (buttonName)
                {
                    case "stop":
                        stop.Content="test2";
                        break;
                    case "one":
                        stop.Content = "test";
                        break;
                    case "two":
                        stop.Content = "test23";
                        break;
                }
            }
        }
    }
}
