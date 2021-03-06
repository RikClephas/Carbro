﻿using Carbro.Core;
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
    public sealed partial class Settings : Page
    {
        JsonHelper jh = new JsonHelper();
        List<Bottle> bottleList = new List<Bottle>();
        List<TwoFactor> TwoFactorList = new List<TwoFactor>();
        string unlockCaller = "";
        int randomNumber = 0;

        public Settings()
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

        private void ButtonCancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (PopupAddCocktail.IsOpen) { PopupAddCocktail.IsOpen = false; }
            if (PopupUnlock.IsOpen)
            {
                PopupUnlock.IsOpen = false;
                unlockCaller = "";
            }
        }

        private void Add_Tapped(object sender, TappedRoutedEventArgs e)
        {
            List<Cocktail> CocktailList = new List<Cocktail>();
            CocktailList = jh.ReadCocktailsJsonToList();
            Cocktail c = new Cocktail();
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
            if (PopupAddCocktail.IsOpen)
            {
                PopupAddCocktail.IsOpen = false;
            }
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
            this.Frame.Navigate(typeof(Screen_Cocktails));
        }

        private void Unlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TwoFactorList = jh.ReadTwoFactorToList();
            UnlockCodeField.Text = "";
            unlockCaller = ((Button)sender).Name.ToString();
            randomNumber = new Random().Next(0, TwoFactorList.Count());
            KeyName.Text = TwoFactorList.ElementAt(randomNumber).Key;
            if (!PopupUnlock.IsOpen)
            {
                PopupUnlock.IsOpen = true;
            }
            UnlockCodeField.Focus(FocusState.Pointer);
            UnlockCodeField.SelectAll();
        }

        private void Unlock_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (UnlockCodeField.Text == TwoFactorList.ElementAt(randomNumber).Pass)
            {
                UnlockCodeField.Text = "";
                if (PopupUnlock.IsOpen)
                {
                    PopupUnlock.IsOpen = false;
                }
                switch(unlockCaller)
                {
                    case "removeCocktailLock":
                        this.Frame.Navigate(typeof(Screen_RemoveCocktail));
                        break;
                    case "removeCocktailButton":
                        this.Frame.Navigate(typeof(Screen_RemoveCocktail));
                        break;
                    case "managePumpsLock":
                        this.Frame.Navigate(typeof(ManagePumps));
                        break;
                    case "managePumpsButton":
                        this.Frame.Navigate(typeof(ManagePumps));
                        break;
                    case "cleanMachineLock":
                        this.Frame.Navigate(typeof(CleanPumps));
                        break;
                    case "cleanMachineButton":
                        this.Frame.Navigate(typeof(CleanPumps));
                        break;
                    case "shutdownLock":
                        this.Frame.Navigate(typeof(UnlockedSettings));
                        break;
                    case "shutdownButton":
                        this.Frame.Navigate(typeof(UnlockedSettings));
                        break;
                    default:
                        this.Frame.Navigate(typeof(UnlockedSettings));
                        break;
                }
            }
            else
            {
                UnlockCodeField.Text = "";
                UnlockCodeField.Focus(FocusState.Pointer);
                UnlockCodeField.SelectAll();
            }
        }
    }
}
