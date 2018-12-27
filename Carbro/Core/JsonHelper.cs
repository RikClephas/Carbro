using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carbro.Screens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.Storage;

namespace Carbro.Core
{
    public class JsonHelper
    {
        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        // Windows.Storage.StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
        private string path; //"ms-appx:///JsonFiles/Cocktails.json"; //"C:\\Users\\Jesse\\Videos\\CocktailMakerJson\\Cocktails.json
        private string path2;
        private string path3;

        public JsonHelper()
        {
            path = localFolder.Path + "\\Cocktails.json";
            path2 = localFolder.Path + "\\Bottles.json";
            path3 = localFolder.Path + "\\2FA.json";

            // path = installedLocation.Path + "\\Cocktails.json";

        }



        internal async void WriteListToJson(List<Cocktail> cocktailList)
        {

            await Task.Run(() =>
            {

                File.WriteAllText(path, JsonConvert.SerializeObject(cocktailList));

            });
        }

        internal List<Cocktail> ReadCocktailsJsonToList()
        {
            string list = "";

            list = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<Cocktail>>(list);

        }

        internal List<Bottle> ReadBottlesJsonToList()
        {
            string list = "";

            list = File.ReadAllText(path2);
            return JsonConvert.DeserializeObject<List<Bottle>>(list);

        }

        internal List<TwoFactor> ReadTwoFactorToList()
        {
            return JsonConvert.DeserializeObject<List<TwoFactor>>(File.ReadAllText(path3));
        }

        internal async void WriteDrinksListToJson(List<Bottle> Bottlelist)
        {

            await Task.Run(() =>
            {
                File.WriteAllText(path2, JsonConvert.SerializeObject(Bottlelist));
            });
        }
    }
}
