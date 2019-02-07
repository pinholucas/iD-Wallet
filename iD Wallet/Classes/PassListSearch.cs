using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace iD_Wallet.Classes
{
    class PassListSearch : PassList
    {
        public List<MyPass> passListMatches = new List<MyPass>();

        string orgName;
        string desc;

        public PassListSearch()
        {            
        }

        public void Search(string keyword)
        {
            var _Initialization = Initialization.initialization;
            

            passListMatches.Clear();

            if (keyword != "")
            {
                foreach (var i in _Initialization.configFile["passList"])
                {
                    orgName = i["organizationName"];
                    desc = i["description"];
                    hName = i["headerLogoText"];
                    hLabel = i["headerLabel"];
                    hValue = i["headerValue"];
                    secHLabel = i["secHeaderLabel"];
                    secHValue = i["secHeaderValue"];

                    if (orgName.CaseInsensitiveContains(keyword) || desc.CaseInsensitiveContains(keyword) ||
                        hName.CaseInsensitiveContains(keyword) || hLabel.CaseInsensitiveContains(keyword) ||
                        hValue.CaseInsensitiveContains(keyword) || secHLabel.CaseInsensitiveContains(keyword) ||
                        secHValue.CaseInsensitiveContains(keyword)) 
                    {
                        passListMatches.Add(
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
        }
    }
}
