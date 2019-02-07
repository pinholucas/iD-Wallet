using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using iD_Wallet.Controls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace iD_Wallet.Classes
{
    class PassListAdd : Initialization
    {
        dynamic passFile = null;
        dynamic manifestFile = null;
        string passID = null;

        public async void AddPass()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".pkpass");
            StorageFile pkpassFile = await filePicker.PickSingleFileAsync();            

            //If user hasn't closed the open dialog window
            if (pkpassFile != null)
            {
                ZipArchive pkpassFileStream = null;

                await Task.Run(() =>
                {                    
                    //Autorização para abrir arquivos de qualquer pasta
                    StorageApplicationPermissions.FutureAccessList.AddOrReplace("pkpass", pkpassFile);

                    //Open .pkpass file as a ZipArchive
                    pkpassFileStream = ZipFile.OpenRead(pkpassFile.Path);
                });

                //Checks if pass.json and manifest.json exists in .pkass, open and read
                if (pkpassFileStream.GetEntry("pass.json").Name == "pass.json" && pkpassFileStream.GetEntry("manifest.json").Name == "manifest.json")
                {
                    var passjsonFile = pkpassFileStream.GetEntry("pass.json").Open();
                    var manifestjsonFile = pkpassFileStream.GetEntry("manifest.json").Open();                                    

                    using (var reader = new StreamReader(passjsonFile))
                    {
                        passFile = JsonConvert.DeserializeObject(reader.ReadToEnd());

                        passjsonFile.Dispose();
                    }

                    using (var reader = new StreamReader(manifestjsonFile))
                    {
                        manifestFile = JsonConvert.DeserializeObject(reader.ReadToEnd());
                        passID = manifestFile["pass.json"];

                        manifestjsonFile.Dispose();
                    }

                    //Checks if pass folder already exists (if pass was already added)
                    if (!Directory.Exists(localFolder.Path + "/" + passID))
                    {
                        //Creates pass folder and extract pass files there
                        await Task.Run(() =>
                        {
                            pkpassFileStream.ExtractToDirectory(localFolder.Path + "/" + passID);
                        });

                        //Reads the pass, write info in config.json and refresh the PassListView
                        InsertPass();
                        RefreshPassList();                                           
                    }
                    else
                    {
                        //Error message
                        var dialog = new MessageDialog("Arquivo já adicionado.");
                        await dialog.ShowAsync();
                    }
                    
                }
                else
                {
                    //Error message
                    var dialog = new MessageDialog("Arquivo corrompido ou incompatível. Por favor, entre em contato com o nosso suporte.");
                    await dialog.ShowAsync();
                }
            }
        }

        #region METHODS
        private void InsertPass()
        {
            #region DEFINE ORGANIZATION NAME

            string organizationName = passFile["organizationName"] != null ? passFile["organizationName"] : "";

            #endregion

            #region DEFINE PASS TYPE
            string type = null;

            if (passFile["boardingPass"] != null)
            {
                type = "boardingPass";
            }

            if (passFile["coupon"] != null)
            {
                type = "coupon";
            }

            if (passFile["eventTicket"] != null)
            {
                type = "eventTicket";
            }

            if (passFile["storeCard"] != null)
            {
                type = "storeCard";
            }

            if (passFile["generic"] != null)
            {
                type = "generic";
            }
            #endregion

            #region DEFINE LABEL COLOR

            string labelColor = passFile["labelColor"] != null ? passFile["labelColor"] : passFile["foregroundColor"];

            #endregion

            #region DEFINE HEADER FIELDS
            string hLabel = null;
            string hValue = null;
            string secHLabel = null;
            string secHValue = null;

            if (passFile[type]["headerFields"] != null && passFile[type]["headerFields"].Count > 0)
            {
                hLabel = passFile[type]["headerFields"][0]["label"];
                hValue = passFile[type]["headerFields"][0]["value"];

                if (passFile[type]["headerFields"].Count > 1)
                {
                    secHLabel = passFile[type]["headerFields"][1]["label"];
                    secHValue = passFile[type]["headerFields"][1]["value"];
                }
                else
                {
                    secHLabel = "";
                    secHValue = "";
                }
            }
            else
            {
                hLabel = "";
                hValue = "";
                secHLabel = "";
                secHValue = "";
            }
            #endregion

            #region DEFINE HEADER LOGO TEXT      

            string logoText = passFile["logoText"] != null ? passFile["logoText"] : "";

            #endregion

            #region DEFINE HEADER LOGO            
            var logoBitmap = new BitmapImage();
            Stream logoFileStream = null;

            string headerLogoPath = localFolder.Path + "/" + passID;

            if (File.Exists(headerLogoPath + "/logo.png"))
            {                
                headerLogoPath = headerLogoPath + "/logo.png";
                logoFileStream = File.OpenRead(headerLogoPath);

                using (var memStream = new MemoryStream())
                {
                    logoFileStream.CopyTo(memStream);
                    memStream.Position = 0;

                    logoBitmap.SetSource(memStream.AsRandomAccessStream());

                    logoFileStream.Dispose();
                }
            }
            else if (!File.Exists(headerLogoPath + "/logo.png") && File.Exists(headerLogoPath + "/icon.png"))
            {
                headerLogoPath = headerLogoPath + "/icon.png";
                logoFileStream = File.OpenRead(headerLogoPath);

                using (var memStream = new MemoryStream())
                {
                    logoFileStream.CopyTo(memStream);
                    memStream.Position = 0;

                    logoBitmap.SetSource(memStream.AsRandomAccessStream());

                    logoFileStream.Dispose();
                }
            }
            #endregion

            JObject rss = JObject.Parse(File.ReadAllText(localFolder.Path + "/config.json"));

            JArray passList = (JArray)rss["passList"];
            passList.Add(new JObject(
                   new JProperty("ID", manifestFile["pass.json"]),
                   new JProperty("type", type),
                   new JProperty("background", passFile["backgroundColor"]),
                   new JProperty("foreground", passFile["foregroundColor"]),
                   new JProperty("label", labelColor),                               
                   new JProperty("headerLogo", headerLogoPath),
                   new JProperty("headerLogoWidth", logoBitmap.PixelWidth),
                   new JProperty("headerLogoHeight", logoBitmap.PixelHeight), 
                   new JProperty("headerLogoText", logoText),
                   new JProperty("headerLabel", hLabel),
                   new JProperty("headerValue", hValue),
                   new JProperty("secHeaderLabel", secHLabel),
                   new JProperty("secHeaderValue", secHValue),
                   new JProperty("organizationName", organizationName),
                   new JProperty("description", passFile["description"])
                   ));

            File.WriteAllText(localFolder.Path + "/config.json", rss.ToString());
        }

        private void RefreshPassList()
        {
            var _Initialization = Initialization.initialization;
            var _PassListControl = PassListControl.passListControl;
            var _MainControl = MainControl.mainControl;

            _Initialization.configFile = JsonConvert.DeserializeObject(File.ReadAllText(localFolder.Path + "/config.json"));

            _MainControl.changeCategory(0);
            _PassListControl.passListUpdate(0, "");
        }
        #endregion
    }
}
