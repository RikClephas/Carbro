using Carbro.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
    public sealed partial class Screen_Cocktails : Page
    {

        bool emulation = true;


        List<Cocktails> cocktaillist;
        JsonHelper jh = new JsonHelper();
        string CocktailName = "";



        public Screen_Cocktails()
        {
            
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            this.InitializeComponent();
            cocktaillist = new List<Cocktails>();
            cocktaillist = jh.ReadCocktailsJsonToList();
            addCocktails();
            
        }

        public void addCocktails()
        {
            

            foreach (var item in cocktaillist)
            {

                Button b = new Button();
                b.Content = item.Name;
                b.FontFamily = new FontFamily("/Assets/Fonts/HoboStd.otf#Hobo Std");
                b.Foreground = (SolidColorBrush)Resources["ButtonTextColor"];
                b.FontSize = 30;
                b.Height = 120;
                b.Width = 228;
                b.Margin = new Thickness(10);
                b.Background = (SolidColorBrush)Resources["ButtonColor"];
                b.Tapped += CocktailSelected;
                CocktailWindow.Children.Add(b);
            }


            
        }
        public void CocktailSelected(object sender, TappedRoutedEventArgs e)
        {
            GeneratePopup(((Button)sender).Content.ToString());
            CocktailName = ((Button)sender).Content.ToString();
            // open the Popup if it isn't open already 
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
            

        }
        private void ButtonStop_Tapped(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
            PercentagePanel.Children.Clear();
            BottlePanel.Children.Clear();
            CocktailNamePanel.Children.Clear();
        }

        public void GeneratePopup(string cocktailName)
        {
            Cocktails c = cocktaillist.Find(x => x.Name == cocktailName);

            TextBlock tbName = new TextBlock();
            tbName.Text = c.Name;
            
            CocktailNamePanel.Children.Add(tbName);
            foreach (var item in c.Liquids)
            {
                TextBlock tbValue = new TextBlock();
                tbValue.Text = item.Value.ToString() + "%";
                TextBlock tbBottle = new TextBlock();
                tbBottle.Text = item.Key;
                PercentagePanel.Children.Add(tbValue);
                BottlePanel.Children.Add(tbBottle);
            }
            

            
        }

        private void Start_Tapped(object sender, TappedRoutedEventArgs e)
        {
            InitGPIO(CocktailName);
        }

        

        private void InitGPIO(string cocktailname)
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

            List<Cocktails> cocktailslist = new List<Cocktails>();
            cocktaillist = jh.ReadCocktailsJsonToList();
            Cocktails c = new Cocktails();
            c = cocktaillist.Find(x => x.Name == cocktailname);
            List<GpioPin> ActivePinList = new List<GpioPin>();
            Button buttoon = new Button();

            foreach (var item in c.Liquids)
            {

                Bottles b = jh.ReadBottlesJsonToList().Find(x => x.Name == item.Key);
                pinlist.Find(x => x.PinNumber == b.BottlePin).Write(GpioPinValue.High);
                ActivePinList.Add(pinlist.Find(x => x.PinNumber == b.BottlePin));
                System.Diagnostics.Debug.WriteLine("activated: " + b.BottlePin);


            }
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Console.WriteLine(unixTimestamp);

            Int32 StopTime;

            bool completed = false;

            while (!completed)
            {
                Console.WriteLine(unixTimestamp);
                StopTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;



                var counter = c.Liquids.Count;
                foreach (var item in c.Liquids)
                {
                    Bottles b = jh.ReadBottlesJsonToList().Find(x => x.Name == item.Key);




                    if (StopTime >= unixTimestamp + ((item.Value) * ((b.Calibration/10))/100))
                    {
                        counter--;

                        System.Diagnostics.Debug.WriteLine("deactivated: " + b.BottlePin);
                        ActivePinList.Find(x => x.PinNumber == b.BottlePin).Write(GpioPinValue.Low);


                    }
                    if (counter == 0)
                    {
                        completed = true;
                        System.Diagnostics.Debug.WriteLine("completed");
                    }
                }

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
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
            PercentagePanel.Children.Clear();
            BottlePanel.Children.Clear();
            CocktailNamePanel.Children.Clear();

        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private async void OpenNewPageAsync(Type pageName)
        {
            var viewId = 0;
            var newView = CoreApplication.CreateNewView();
            await newView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    var frame = new Frame();
                    frame.Navigate(pageName, null);
                    Window.Current.Content = frame;

                    viewId = ApplicationView.GetForCurrentView().Id;


                    Window.Current.Activate();

                });
            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);


        }
    }
}
