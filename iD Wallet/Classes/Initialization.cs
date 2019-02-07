using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iD_Wallet.Classes;
using System.Runtime.Serialization.Json;
using Windows.Storage.AccessCache;
using Windows.Storage;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Windows.UI.Popups;
using System.ComponentModel;
using iD_Wallet.Controls;

namespace iD_Wallet.Classes
{
    public class Initialization
    {
        public static Initialization initialization;

        public StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public dynamic configFile;

        public Initialization()
        {
            initialization = this;
            AppLoad();
        }

        public void AppLoad()
        {
            if (File.Exists(localFolder.Path + "/config.json") == false)
            {
                JObject configJson = 
                    new JObject(
                        new JProperty("firstStart", true),
                        new JProperty("passList", 
                            new JArray())
                    );
                         
                File.WriteAllText(localFolder.Path + "/config.json", configJson.ToString());
            }
            else
            {
                configFile = JObject.Parse(File.ReadAllText(localFolder.Path + "/config.json")); //talvez seja + rapido de carregar

                //configFile = JsonConvert.DeserializeObject(File.ReadAllText(localFolder.Path + "/config.json"));
            }
        }

    }
}
