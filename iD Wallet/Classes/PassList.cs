using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iD_Wallet.Classes;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Foundation;
using Windows.UI.Xaml;
using iD_Wallet.Controls;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace iD_Wallet.Classes
{
    public class PassList : Initialization
    {
        public static PassList passList;

        public List<MyPass> passListAll = new List<MyPass>();
        public List<MyPass> passListBoarding = new List<MyPass>();
        public List<MyPass> passListCoupons = new List<MyPass>();
        public List<MyPass> passListEvents = new List<MyPass>();
        public List<MyPass> passListCards = new List<MyPass>();
        public List<MyPass> passListGenerics = new List<MyPass>();

        public class MyPass
        {
            public string ID { get; set; }
            public string Type { get; set; }
            public dynamic backgroundColor { get; set; }
            public dynamic foregroundColor { get; set; }
            public dynamic labelColor { get; set; }
            public string headerLogo { get; set; }
            public int headerLogoWidth { get; set; }
            public int headerLogoHeight { get; set; }
            public string headerName { get; set; }
            public Thickness headerNameMargin { get; set; }
            public string headerLabel { get; set; }
            public string headerValue { get; set; }
            public string secHeaderLabel { get; set; }
            public string secHeaderValue { get; set; }
            public Thickness secHeaderLabelMargin { get; set; }
            public Thickness secHeaderValueMargin { get; set; }
        }

        public SolidColorBrush brushBG;
        public SolidColorBrush brushFG;
        public SolidColorBrush brushLG;
        public string hLogo;
        public int hLogoWidth;
        public string hName;
        public string hLabel;
        public string hValue;
        public string secHLabel;
        public string secHValue;

        string passType = null;

        public PassList()
        {
            passList = this; 
        }        

        public void passListAllLoad()
        {
            var _Initialization = Initialization.initialization;

            passListAll.Clear();
            
            foreach (var i in _Initialization.configFile["passList"])
            {
                // Insert(0,txt) = show newest first
                // Add(txt) = show oldest first
                passListAll.Insert(0, new MyPass
                {
                    ID = i["ID"],
                    Type = i["type"],
                    backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                    foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                    labelColor = getColor((string)i["label"], "rgb(", ")"),
                    headerLogo = i["headerLogo"],
                    headerLogoWidth = i["headerLogoWidth"],
                    headerName = i["headerLogoText"],
                    headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                    headerLabel = i["headerLabel"],
                    headerValue = i["headerValue"],
                    secHeaderLabel = i["secHeaderLabel"],
                    secHeaderValue = i["secHeaderValue"],
                    secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                    secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                });
                              
            }
        }

        public void passListBoardingLoad()
        {
            var _Initialization = Initialization.initialization;

            passListBoarding.Clear();
            
            foreach (var i in _Initialization.configFile["passList"])
            {
                passType = i["type"];

                if (passType == "boardingPass")
                {
                    passListBoarding.Insert(0,
                    new MyPass
                    {
                        ID = i["ID"],
                        Type = i["type"],
                        backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                        foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                        labelColor = getColor((string)i["label"], "rgb(", ")"),
                        headerLogo = i["headerLogo"],
                        headerLogoWidth = i["headerLogoWidth"],
                        headerName = i["headerLogoText"],
                        headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                        headerLabel = i["headerLabel"],
                        headerValue = i["headerValue"],
                        secHeaderLabel = i["secHeaderLabel"],
                        secHeaderValue = i["secHeaderValue"],
                        secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                        secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                    });
                }
            }
        }

        public void passListCouponsLoad()
        {
            var _Initialization = Initialization.initialization;

            passListCoupons.Clear();

            foreach (var i in _Initialization.configFile["passList"])
            {
                passType = i["type"];

                if (passType == "coupon")
                {
                    passListCoupons.Insert(0,
                    new MyPass
                    {
                        ID = i["ID"],
                        Type = i["type"],
                        backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                        foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                        labelColor = getColor((string)i["label"], "rgb(", ")"),
                        headerLogo = i["headerLogo"],
                        headerLogoWidth = i["headerLogoWidth"],
                        headerName = i["headerLogoText"],
                        headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                        headerLabel = i["headerLabel"],
                        headerValue = i["headerValue"],
                        secHeaderLabel = i["secHeaderLabel"],
                        secHeaderValue = i["secHeaderValue"],
                        secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                        secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                    });
                }
            }
        }

        public void passListEventsLoad()
        {
            var _Initialization = Initialization.initialization;

            passListEvents.Clear();

            foreach (var i in _Initialization.configFile["passList"])
            {
                passType = i["type"];

                if (passType == "eventTicket")
                {                    
                    passListEvents.Insert(0,
                    new MyPass
                    {
                        ID = i["ID"],
                        Type = i["type"],
                        backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                        foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                        labelColor = getColor((string)i["label"], "rgb(", ")"),
                        headerLogo = i["headerLogo"],
                        headerLogoWidth = i["headerLogoWidth"],
                        headerName = i["headerLogoText"],
                        headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                        headerLabel = i["headerLabel"],
                        headerValue = i["headerValue"],
                        secHeaderLabel = i["secHeaderLabel"],
                        secHeaderValue = i["secHeaderValue"],
                        secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                        secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                    });
                }
            }
        }

        public void passListCardsLoad()
        {
            var _Initialization = Initialization.initialization;

            passListCards.Clear();

            foreach (var i in _Initialization.configFile["passList"])
            {
                passType = i["type"];

                if (passType == "storeCard")
                {
                    passListCards.Insert(0,
                    new MyPass
                    {
                        ID = i["ID"],
                        Type = i["type"],
                        backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                        foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                        labelColor = getColor((string)i["label"], "rgb(", ")"),
                        headerLogo = i["headerLogo"],
                        headerLogoWidth = i["headerLogoWidth"],
                        headerName = i["headerLogoText"],
                        headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                        headerLabel = i["headerLabel"],
                        headerValue = i["headerValue"],
                        secHeaderLabel = i["secHeaderLabel"],
                        secHeaderValue = i["secHeaderValue"],
                        secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                        secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                    });
                }
            }
        }

        public void passListGenericsLoad()
        {
            var _Initialization = Initialization.initialization;

            passListGenerics.Clear();

            foreach (var i in _Initialization.configFile["passList"])
            {
                passType = i["type"];

                if (passType == "generic")
                {
                    passListGenerics.Insert(0,
                    new MyPass
                    {
                        ID = i["ID"],
                        Type = i["type"],
                        backgroundColor = getColor((string)i["background"], "rgb(", ")"),
                        foregroundColor = getColor((string)i["foreground"], "rgb(", ")"),
                        labelColor = getColor((string)i["label"], "rgb(", ")"),
                        headerLogo = i["headerLogo"],
                        headerLogoWidth = i["headerLogoWidth"],
                        headerName = i["headerLogoText"],
                        headerNameMargin = new Thickness((double)i["headerLogoWidth"] + 15, 0, 0, 0),
                        headerLabel = i["headerLabel"],
                        headerValue = i["headerValue"],
                        secHeaderLabel = i["secHeaderLabel"],
                        secHeaderValue = i["secHeaderValue"],
                        secHeaderLabelMargin = new Thickness(0, 6, MarginCalc(i) + 25, 0),
                        secHeaderValueMargin = new Thickness(0, 33, MarginCalc(i) + 25, 0)
                    });
                }
            }
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
    }

    



}
