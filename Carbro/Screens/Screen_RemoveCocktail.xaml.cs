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
    public sealed partial class Screen_RemoveCocktail : Page
    {
        bool emulation = true;

        List<Cocktails> cocktaillist;
        List<Bottles> bottleList;
        JsonHelper jh = new JsonHelper();
        string CocktailName = "";

        public Screen_RemoveCocktail()
        {
            
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            ApplicationView.PreferredLaunchViewSize = new Size(1280, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            this.InitializeComponent();
            cocktaillist = new List<Cocktails>();
            cocktaillist = jh.ReadCocktailsJsonToList();
            bottleList = new List<Bottles>();
            bottleList = jh.ReadBottlesJsonToList();
            addCocktails();
            
        }

        public void addCocktails()
        {
            CocktailWindow.Children.Clear();
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
            if (!PopupRemoveCocktail.IsOpen) { PopupRemoveCocktail.IsOpen = true; }
        }
        private void ButtonStop_Tapped(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (PopupRemoveCocktail.IsOpen) { PopupRemoveCocktail.IsOpen = false; }
        }

        private void ButtonCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (PopupRemoveCocktail.IsOpen) { PopupRemoveCocktail.IsOpen = false; }
        }

        public void GeneratePopup(string cocktailName)
        {
            Cocktails c = cocktaillist.Find(x => x.Name == cocktailName);
            RemoveCocktailNameField.Text = "";
            RemoveCocktailNameField.Text = c.Name;
            FillBottleNamesRemoveCocktail();
            FillPercentageRemoveCocktail(c);
        }

        private void Remove_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Cocktails c = cocktaillist.Find(x => x.Name == CocktailName);
            c.Name = RemoveCocktailNameField.Text;
            List<KeyValuePair<string, int>> listkvpBottles = new List<KeyValuePair<string, int>>();

            int bottleId = 2000;
            for (int i = 1; i < bottleList.Count + 1; i++)
            {
                string bottlenumber = "Percentage" + i.ToString();
                bottleId++;
                if (((TextBox)this.FindName(bottlenumber)).Text != "")
                {
                    if (Int32.Parse(((TextBox)this.FindName(bottlenumber)).Text) != 0)
                    {
                        KeyValuePair<string, int> kvpbottle = new KeyValuePair<string, int>(bottleList.Find(x => x.ID == (bottleId)).Name, Int32.Parse(((TextBox)this.FindName(bottlenumber)).Text));
                        listkvpBottles.Add(kvpbottle);
                    }

                }

            }
            c.Liquids = listkvpBottles;
            cocktaillist.Remove(c);
            jh.WriteListToJson(cocktaillist);
            if (PopupRemoveCocktail.IsOpen) { PopupRemoveCocktail.IsOpen = false; }
            addCocktails();
        }
        
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private void Back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private void FillBottleNamesRemoveCocktail()
        {
            int bottleId = 2000;
            string bottlenumber;
            string percentagenumber;
            for (int i = 1; i < bottleList.Count + 1; i++)
            {
                bottlenumber = "Bottle" + i.ToString();
                percentagenumber = "Percentage" + i.ToString();
                bottleId++;
                ((TextBox)this.FindName(percentagenumber)).Text = "";
                ((TextBlock)this.FindName(bottlenumber)).Text = bottleList.Find(x => x.ID == (bottleId)).Name;
            }
        }

        private void FillPercentageRemoveCocktail(Cocktails c)
        {
            int i = 1;
            string percentagenumber;
            foreach (var item in c.Liquids)
            {
                percentagenumber = "Percentage" + i.ToString();
                ((TextBox)this.FindName(percentagenumber)).Text = item.Value.ToString();
                i++;
            }
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
