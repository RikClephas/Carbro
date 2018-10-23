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
        double unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        double StopTime;
        List<Bottles> bottles = null;
        private Bottles bottle = null;
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
                        TimeGrid.Visibility = Visibility.Collapsed;
                        if (stop.IsChecked == false) { stop.IsChecked = true; }
                        break;
                    case "Calibrate":
                        TimeGrid.Visibility = Visibility.Visible;
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
            bottles = jh.ReadBottlesJsonToList();
            int buttonNumber = 0;
            if (rb != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (IOinitialized)
                    {
                        _pins[i].Write(GpioPinValue.Low);
                    }
                }
                string buttonName = rb.Content.ToString().ToLower();
                if(buttonName == "stop")
                {
                    StopTime = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    newValue.Text = ((int)((StopTime - unixTimestamp)*10)).ToString();
                    saveButton.Visibility = Visibility.Visible;
                }
                else
                {
                    if(Int32.TryParse(buttonName, out buttonNumber))
                    {
                        if (IOinitialized)
                        {
                            _pins[buttonNumber - 1].Write(GpioPinValue.High);
                        }
                        unixTimestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                        bottle = bottles.Find(x => x.BottleNumber == buttonNumber);
                        oldValue.Text = bottle.Calibration.ToString();
                        newValue.Text = "0";
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
                _pins[i].Write(GpioPinValue.Low);
                _pins[i].SetDriveMode(GpioPinDriveMode.Output);
            }
            return true;
        }

        private void Save_Tapped(object sender, RoutedEventArgs e)
        {
            int newCalibration = 0;
            if(Int32.TryParse(newValue.Text, out newCalibration))
            {
                if(bottle != null && bottles != null)
                {
                    bottles.Find(x => x.BottleNumber == bottle.BottleNumber).Calibration = newCalibration;
                    jh.WriteDrinksListToJson(bottles);
                    oldValue.Text = newValue.Text;
                    bottle = null;
                    bottles = null;
                    saveButton.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
