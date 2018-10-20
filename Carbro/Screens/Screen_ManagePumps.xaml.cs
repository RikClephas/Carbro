﻿using Carbro.Core;
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
            this.Frame.Navigate(typeof(UnlockedSettings));
        }

        private void BottleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                var gpio = GpioController.GetDefault();
                if (gpio != null)
                {
                    GpioPin pin1 = gpio.OpenPin(9);
                    pin1.SetDriveMode(GpioPinDriveMode.Output);
                    pin1.Write(GpioPinValue.Low);
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

                    string buttonName = rb.Content.ToString().ToLower();
                    switch (buttonName)
                    {
                        case "stop":
                            break;
                        case "1":
                            pin1.Write(GpioPinValue.High);
                            break;
                        case "2":
                            pin2.Write(GpioPinValue.High);
                            break;
                        case "3":
                            pin3.Write(GpioPinValue.High);
                            break;
                        case "4":
                            pin4.Write(GpioPinValue.High);
                            break;
                        case "5":
                            pin5.Write(GpioPinValue.High);
                            break;
                        case "6":
                            pin6.Write(GpioPinValue.High);
                            break;
                        case "7":
                            pin7.Write(GpioPinValue.High);
                            break;
                        case "8":
                            pin8.Write(GpioPinValue.High);
                            break;
                        case "9":
                            pin9.Write(GpioPinValue.High);
                            break;
                        case "10":
                            pin10.Write(GpioPinValue.High);
                            break;
                        case "11":
                            pin11.Write(GpioPinValue.High);
                            break;
                        case "12":
                            pin12.Write(GpioPinValue.High);
                            break;
                        default:
                            stop.IsChecked = true;
                            break;
                    }

                    pin1.Dispose();
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

        private void InitGPIO(int bottlenumber)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Int32 StopTime;

            StopTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
