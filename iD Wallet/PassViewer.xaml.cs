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
using iD_Wallet.Controls;
using iD_Wallet.Classes;
using Windows.UI.Core;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace iD_Wallet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PassViewer : Page
    {
        public static PassViewer passViewer;

        public StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        public PassViewer()
        {
            this.InitializeComponent();
            passViewer = this;

            //\/ rascunho apenas.

            JObject passJson = JObject.Parse(File.ReadAllText(localFolder.Path + "/" + PassListControl.passListControl.accessedPassID + "/pass.json"));
            lol.Text = (string)passJson[PassListControl.passListControl.accessedPassType]["backFields"][0]["value"];
            passView.Background = getColor((string)passJson["backgroundColor"], "rgb(", ")");
        }
        #region METHODS
        public int MarginCalc(dynamic i)
        {
            string hL = i["headerLabel"];
            string hV = i["headerValue"];

            int marginLeft = 0;

            if (hL.Length > hV.Length)
            {
                if (21 - hL.Length < 10)
                {
                    marginLeft = hL.Length * 10;
                }
                else
                {
                    marginLeft = hL.Length * Math.Abs(21 - hL.Length);
                }
            }
            else
            {
                if (21 - hV.Length < 10)
                {
                    marginLeft = hV.Length * 10;
                }
                else
                {
                    marginLeft = hV.Length * Math.Abs(21 - hV.Length);
                }
            }

            return marginLeft;
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static SolidColorBrush getColor(string strSource, string strStart, string strEnd)
        {
            string[] rgb = getBetween(strSource, strStart, strEnd).Split(',');
            var brush = new SolidColorBrush(Color.FromArgb(255, byte.Parse(rgb[0]), byte.Parse(rgb[1]), byte.Parse(rgb[2])));

            return brush;
        }
        #endregion

        private void Page_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(passView);

            if (ptrPt.Properties.IsXButton1Pressed)
            {
                if (((Frame)Window.Current.Content).CanGoBack)
                {
                    ((Frame)Window.Current.Content).GoBack();
                }               
            }

            e.Handled = true;
        }
    }
}
