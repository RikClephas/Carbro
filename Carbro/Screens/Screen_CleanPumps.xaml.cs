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
    public sealed partial class CleanPumps : Page
    {

        JsonHelper jh = new JsonHelper();
        
        private GpioPin[] _pins = new GpioPin[12];
        bool IOinitialized = false;

        public CleanPumps()
        {
            IOinitialized = InitGPIO();
            this.InitializeComponent();
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

        private void CleanRadioButton_Checked(object sender, RoutedEventArgs e)
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
                    string buttonName = rb.Name.ToLower();
                    switch (buttonName)
                    {
                        case "stop":
                            break;
                        case "leftpumpsbutton":
                            _pins[0].Write(GpioPinValue.High);
                            _pins[1].Write(GpioPinValue.High);
                            _pins[2].Write(GpioPinValue.High);
                            _pins[6].Write(GpioPinValue.High);
                            _pins[7].Write(GpioPinValue.High);
                            _pins[8].Write(GpioPinValue.High);
                            break;
                        case "rightpumpsbutton":
                            _pins[3].Write(GpioPinValue.High);
                            _pins[4].Write(GpioPinValue.High);
                            _pins[5].Write(GpioPinValue.High);
                            _pins[9].Write(GpioPinValue.High);
                            _pins[10].Write(GpioPinValue.High);
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
            List<Bottles> bottles = jh.ReadBottlesJsonToList();
            if (gpio == null)
            {
                return false;
            }
            for (int i = 0; i < 12; i++)
            {
                _pins[i] = gpio.OpenPin(bottles.Find(x => x.BottleNumber == i).BottlePin);
                _pins[i].Write(GpioPinValue.Low);
                _pins[i].SetDriveMode(GpioPinDriveMode.Output);
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
