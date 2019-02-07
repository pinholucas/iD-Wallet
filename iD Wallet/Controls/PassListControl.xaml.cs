using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using iD_Wallet.Classes;
using Newtonsoft.Json;
using Windows.UI;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace iD_Wallet.Controls
{
    public sealed partial class PassListControl : UserControl
    {
        public static PassListControl passListControl;

        PassList _PassList = new PassList();
        PassListSearch _PassListSearch = new PassListSearch();

        public string accessedPassID = null;
        public string accessedPassType = null;

        public PassListControl()
        {
            this.InitializeComponent();
            passListControl = this;
        }

        private void UserControl_Loading(FrameworkElement sender, object args)
        {
            var _MainControl = MainControl.mainControl;
            _MainControl.changeCategory(App.app.ActivatedCategory);
        }

        public void passListUpdate(int type, string searchField)
        {
            switch (type)
            {
                case 0:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListAllLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListAll;                    
                    break;
                case 1:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListBoardingLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListBoarding;
                    break;
                case 2:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListCouponsLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListCoupons;
                    break;
                case 3:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListEventsLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListEvents;
                    break;
                case 4:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListCardsLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListCards;
                    break;
                case 5:
                    passListControl.passListView.ItemsSource = null;
                    _PassList.passListGenericsLoad();
                    passListControl.passListView.ItemsSource = _PassList.passListGenerics;
                    break;
                case 6:
                    passListControl.passListView.ItemsSource = null;                    
                    _PassListSearch.Search(searchField);
                    passListControl.passListView.ItemsSource = _PassListSearch.passListMatches;
                    break; 
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            PassList.MyPass pass = senderElement.DataContext as PassList.MyPass;

            accessedPassID = pass.ID;
            accessedPassType = pass.Type;
            ((Frame)Window.Current.Content).Navigate(typeof(PassViewer));
        }
    }
}
