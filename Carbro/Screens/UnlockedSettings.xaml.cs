using Carbro.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
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
    public sealed partial class UnlockedSettings : Page
    {
        JsonHelper jh = new JsonHelper();
        List<Bottles> bottleList = new List<Bottles>();
        
        public UnlockedSettings()
        {
            bottleList = jh.ReadBottlesJsonToList();

            this.InitializeComponent();
        }

        private void AddCocktail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FillBottleNamesAddCocktail();
            if (!PopupAddCocktail.IsOpen) { PopupAddCocktail.IsOpen = true; }
        }

        private void EditCocktail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Screen_EditCocktail));
        }

        private void RemoveCocktail_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Screen_RemoveCocktail));
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (PopupAddCocktail.IsOpen) { PopupAddCocktail.IsOpen = false; }
            if (PopupShutdown.IsOpen) { PopupShutdown.IsOpen = false; }

        }

        private void Add_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<Cocktails> CocktailList = new List<Cocktails>();
            CocktailList = jh.ReadCocktailsJsonToList();
            Cocktails c = new Cocktails();
            c.Name = AddCocktailNameField.Text;
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
            CocktailList.Add(c);

            jh.WriteListToJson(CocktailList);
            if (PopupAddCocktail.IsOpen) { PopupAddCocktail.IsOpen = false; }

        }

        private void FillBottleNamesAddCocktail()
        {
            
            int bottleId = 2000;
            for (int i = 1; i < bottleList.Count+1; i++)
            {
                string bottlenumber = "Bottle" + i.ToString();
                bottleId++;
                

                   ((TextBlock)this.FindName(bottlenumber)).Text = bottleList.Find(x => x.ID == (bottleId)).Name;
                
            }

        }

        private void SettingsBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));

        }

        private void ManageBottles_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void CleanMachine_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void CalibratePumps_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void Shutdown_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!PopupShutdown.IsOpen) { PopupShutdown.IsOpen = true; }
        }

        private void ShutdownButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
