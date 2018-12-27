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
        private GpioPin[] _pins = new GpioPin[12];
        double startTime = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        double currentTime;
        List<Bottle> bottles = null;
        bool IOinitialized = false;
        List<Cocktail> cocktaillist;
        JsonHelper jh = new JsonHelper();
        string CocktailName = "";
        
        public Screen_Cocktails()
        {
            IOinitialized = InitGPIO();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            this.InitializeComponent();
            cocktaillist = new List<Cocktail>();
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
            if (!StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = true;
            }
        }

        private void ButtonStop_Tapped(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
            }

            PercentagePanel.Children.Clear();
            BottlePanel.Children.Clear();
            CocktailNamePanel.Children.Clear();
        }

        public void GeneratePopup(string cocktailName)
        {
            Cocktail c = cocktaillist.Find(x => x.Name == cocktailName);

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
            if (IOinitialized)
            {
                MakeCocktail(CocktailName);
            }

            if (StandardPopup.IsOpen)
            {
                StandardPopup.IsOpen = false;
            }

            PercentagePanel.Children.Clear();
            BottlePanel.Children.Clear();
            CocktailNamePanel.Children.Clear();
        }

        private bool InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            bottles = jh.ReadBottlesJsonToList();
            if (gpio == null)
            {
                return false;
            }
            for (int i = 0; i < 12; i++)
            {
                _pins[i] = gpio.OpenPin(bottles.Find(x => x.BottleNumber == i + 1).BottlePin);
                _pins[i].Write(GpioPinValue.Low);
                _pins[i].SetDriveMode(GpioPinDriveMode.Output);
            }
            return true;
        }

        private void MakeCocktail(string cocktailname)
        {
            cocktaillist = jh.ReadCocktailsJsonToList();
            Cocktail c = new Cocktail();
            c = cocktaillist.Find(x => x.Name == cocktailname);
            Button buttoon = new Button();
            int counter = 0;

            foreach (var item in c.Liquids)
            {
                Bottle b = jh.ReadBottlesJsonToList().Find(x => x.Name == item.Key);
                _pins[b.BottleNumber - 1].Write(GpioPinValue.High);
                System.Diagnostics.Debug.WriteLine("activated: " + b.BottlePin);
            }

            startTime = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Console.WriteLine(startTime);

            bool completed = false;

            while (!completed)
            {
                Console.WriteLine(startTime);
                currentTime = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                counter = c.Liquids.Count;
                foreach (var item in c.Liquids)
                {
                    Bottle b = jh.ReadBottlesJsonToList().Find(x => x.Name == item.Key);

                    if (currentTime >= startTime + item.Value * ((double)b.Calibration)/10.0/100.0)
                    {
                        counter--;
                        System.Diagnostics.Debug.WriteLine("deactivated: " + b.BottlePin);
                        _pins[b.BottleNumber - 1].Write(GpioPinValue.Low);
                    }
                    if (counter == 0)
                    {
                        completed = true;
                        System.Diagnostics.Debug.WriteLine("completed");
                    }
                }
            }
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (IOinitialized)
            {
                for (int i = 0; i < 12; i++)
                {
                    _pins[i].Dispose();
                }
            }
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
