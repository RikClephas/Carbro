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
                        stop.IsChecked = true;
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
                string buttonName = rb.Content.ToString().ToLower();
                switch (buttonName)
                {
                    case "stop":
                        
                        break;
                    case "1":
                        
                        break;
                    case "2":
                        
                        break;
                    case "3":

                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "6":

                        break;
                    case "7":

                        break;
                    case "8":

                        break;
                    case "9":

                        break;
                    case "10":

                        break;
                    case "11":

                        break;
                    case "12":

                        break;
                    default:
                        stop.IsChecked = true;
                        break;
                }
            }
        }

        private void InitGPIO(int bottlenumber)
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                Console.WriteLine("There is no GPIO controller on this device.");
                return;
            }

            GpioPin pin = gpio.OpenPin(9);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin.Write(GpioPinValue.Low);
            GpioPin pin2 = gpio.OpenPin(23);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            pin2.Write(GpioPinValue.Low);
            GpioPin pin3 = gpio.OpenPin(12);
            pin3.SetDriveMode(GpioPinDriveMode.Output);
            pin3.Write(GpioPinValue.Low);
            GpioPin pin4 = gpio.OpenPin(22);
            pin4.SetDriveMode(GpioPinDriveMode.Output);
            pin4.Write(GpioPinValue.Low);
            GpioPin pin5 = gpio.OpenPin(27);
            pin5.SetDriveMode(GpioPinDriveMode.Output);
            pin5.Write(GpioPinValue.Low);
            GpioPin pin6 = gpio.OpenPin(16);
            pin6.SetDriveMode(GpioPinDriveMode.Output);
            pin6.Write(GpioPinValue.Low);
            GpioPin pin7 = gpio.OpenPin(20);
            pin7.SetDriveMode(GpioPinDriveMode.Output);
            pin7.Write(GpioPinValue.Low);
            GpioPin pin8 = gpio.OpenPin(21);
            pin8.SetDriveMode(GpioPinDriveMode.Output);
            pin8.Write(GpioPinValue.Low);
            GpioPin pin9 = gpio.OpenPin(6);
            pin9.SetDriveMode(GpioPinDriveMode.Output);
            pin9.Write(GpioPinValue.Low);
            GpioPin pin10 = gpio.OpenPin(13);
            pin10.SetDriveMode(GpioPinDriveMode.Output);
            pin10.Write(GpioPinValue.Low);
            GpioPin pin11 = gpio.OpenPin(19);
            pin11.SetDriveMode(GpioPinDriveMode.Output);
            pin11.Write(GpioPinValue.Low);
            GpioPin pin12 = gpio.OpenPin(26);
            pin12.SetDriveMode(GpioPinDriveMode.Output);
            pin12.Write(GpioPinValue.Low);

            List<GpioPin> pinlist = new List<GpioPin>();

            pinlist.Add(pin);
            pinlist.Add(pin2);
            pinlist.Add(pin3);
            pinlist.Add(pin4);
            pinlist.Add(pin5);
            pinlist.Add(pin6);
            pinlist.Add(pin7);
            pinlist.Add(pin8);
            pinlist.Add(pin9);
            pinlist.Add(pin10);
            pinlist.Add(pin11);
            pinlist.Add(pin12);

            List<GpioPin> ActivePinList = new List<GpioPin>();

            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Console.WriteLine(unixTimestamp);

            Int32 StopTime;

            bool completed = false;

            Bottles b = jh.ReadBottlesJsonToList().Find(x => x.BottleNumber == bottlenumber);

            while (!completed)
            {
                StopTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
            pin.Dispose();
            pin2.Dispose();
            pin3.Dispose();
            pin4.Dispose();
            pin5.Dispose();
            pin6.Dispose();
            pin7.Dispose();
            pin8.Dispose();
            pin9.Dispose();
            pin10.Dispose();
            pin11.Dispose();
            pin12.Dispose();
        }
    }
}
