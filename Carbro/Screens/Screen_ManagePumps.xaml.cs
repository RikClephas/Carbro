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
        bool Manual_Toggle = true;
        public ManagePumps()
        {
            this.InitializeComponent();
        }

        private void Manual_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Manual_Button.Background = new SolidColorBrush(Colors.Green);
            Calibrate_Button.Background = new SolidColorBrush(Colors.Gray);
            Manual_Toggle = true;
        }

        private void Calibrate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Manual_Button.Background = new SolidColorBrush(Colors.Gray);
            Calibrate_Button.Background = new SolidColorBrush(Colors.Green);
            Manual_Toggle = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
