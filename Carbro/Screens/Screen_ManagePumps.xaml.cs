using Carbro.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
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

        JsonHelper jh = new JsonHelper();

        private int[] _pinNumbers = new[] { 9, 23, 12, 22, 27, 16, 20, 21, 6, 13, 19, 26 };
        private GpioPin[] _pins = new GpioPin[12];
        bool IOinitialized = false;

        public ManagePumps()
        {
            IOinitialized = InitGPIO();
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
                        if (stop.IsChecked == false) { stop.IsChecked = true; }
                        break;
                    case "Calibrate":
                        if (stop.IsChecked == false) { stop.IsChecked = true; }
                        break;
                }
            }
        }

        private void Back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (IOinitialized)
            {
                for (int i = 0; i < 12; i++)
                {
                    _pins[i].Dispose();
                }
            }
            this.Frame.Navigate(typeof(UnlockedSettings));
        }

        private void BottleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (IOinitialized)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        _pins[i].Write(GpioPinValue.Low);
                    }
                    string buttonName = rb.Content.ToString().ToLower();
                    switch (buttonName)
                    {
                        case "stop":
                            break;
                        case "1":
                            _pins[0].Write(GpioPinValue.High);
                            break;
                        case "2":
                            _pins[1].Write(GpioPinValue.High);
                            break;
                        case "3":
                            _pins[2].Write(GpioPinValue.High);
                            break;
                        case "4":
                            _pins[3].Write(GpioPinValue.High);
                            break;
                        case "5":
                            _pins[4].Write(GpioPinValue.High);
                            break;
                        case "6":
                            _pins[5].Write(GpioPinValue.High);
                            break;
                        case "7":
                            _pins[6].Write(GpioPinValue.High);
                            break;
                        case "8":
                            _pins[7].Write(GpioPinValue.High);
                            break;
                        case "9":
                            _pins[8].Write(GpioPinValue.High);
                            break;
                        case "10":
                            _pins[9].Write(GpioPinValue.High);
                            break;
                        case "11":
                            _pins[10].Write(GpioPinValue.High);
                            break;
                        case "12":
                            _pins[11].Write(GpioPinValue.High);
                            break;
                        default:
                            stop.IsChecked = true;
                            break;
                    }
                }
            }
        }

        private bool InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return false;
            }
            for (int i = 0; i < 12; i++)
            {
                _pins[i] = gpio.OpenPin(_pinNumbers[i]);
                _pins[i].SetDriveMode(GpioPinDriveMode.Output);
                _pins[i].Write(GpioPinValue.Low);
            }
            return true;
            /*
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Int32 StopTime;

            StopTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            */
        }
    }
}
