using Carbro.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Screen_AddCocktail : Page
    {
        public Screen_AddCocktail()
        {
            this.InitializeComponent();
            InitializeNames();
        }


        public void InitializeNames()
        {
            JsonHelper jh = new JsonHelper();
            var bottlenames = jh.ReadBottlesJsonToList();
            var count = 0;
            foreach (var item in bottlenames)
            {
                TextBlock tbName = new TextBlock();
                tbName.Height = 40;
                tbName.VerticalAlignment = VerticalAlignment.Center;
                tbName.Margin = new Thickness(10);
                tbName.Text = item.Name;
                TextBox tbPercentage = new TextBox();
                tbPercentage.Height = 40;
                tbPercentage.Margin = new Thickness(10);
                if (count < 6 )
                {
                    BottleNamesPanel.Children.Add(tbName);
                    PercentagePanel.Children.Add(tbPercentage);
                }
                else
                {
                    BottleNames2Panel.Children.Add(tbName);
                    Percentage2Panel.Children.Add(tbPercentage);
                }
                count++;

            }
            TextBlock tbCocktailName = new TextBlock();
            tbCocktailName.Text = "Name";
            TextBox tbValue = new TextBox();
            tbValue.Name = "NewCocktailName";
            tbValue.Margin = new Thickness(10);
            tbValue.Height = 20;
            CocktailName.Children.Add(tbCocktailName);
            CocktailName.Children.Add(tbValue);
            
        }

    }
}
