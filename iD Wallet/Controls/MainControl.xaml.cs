using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using iD_Wallet.Classes;
using System.Threading.Tasks;
using Windows.System;

namespace iD_Wallet.Controls
{
    public sealed partial class MainControl : UserControl
    {
        public static MainControl mainControl;

        PassListControl _PassListControl = new PassListControl();
        PassListSearch _PassListSearch = new PassListSearch();
        PassListAdd _PassListAdd = new PassListAdd();

        SolidColorBrush normalColor = new SolidColorBrush(Color.FromArgb(255, 185, 185, 185));
        SolidColorBrush activeColor = new SolidColorBrush(Color.FromArgb(255, 45, 115, 200));
        
        public MainControl()
        {
            this.InitializeComponent();
            mainControl = this;
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            if (txtBox_Search.Visibility == Visibility.Collapsed)
            {
                txtBox_Search.Visibility = Visibility.Visible;
                txtBox_Search.Focus(FocusState.Keyboard);

                doLiveSearch();
            }
            else if (txtBox_Search.Visibility == Visibility.Visible)
            {
                txtBox_Search.Text = "";
                txtBox_Search.Visibility = Visibility.Collapsed;
                changeCategory(App.app.ActivatedCategory);
            }            
        }
        
        private void txtBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            doLiveSearch();
        }

        private void txtBox_Search_LostFocus(object sender, RoutedEventArgs e)
        {
            searchBoxLoseFocus();
        }

        private void txtBox_Search_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Escape)
            {
                searchBoxLoseFocus();
            }
        }

        public void changeCategory(int button)
        {              
            App.app.ActivatedCategory = button;

            switch (button)
            {
                case 0:
                    resetButtons();
                    btn_All.Foreground = activeColor;
                    _PassListControl.passListUpdate(0, "");
                    break;
                case 1:
                    resetButtons();
                    btn_BoardingPass.Foreground = activeColor;
                    _PassListControl.passListUpdate(1, "");
                    break;
                case 2:
                    resetButtons();
                    btn_Coupon.Foreground = activeColor;
                    _PassListControl.passListUpdate(2, "");
                    break;
                case 3:
                    resetButtons();
                    btn_EventTicket.Foreground = activeColor;
                    _PassListControl.passListUpdate(3, "");
                    break;
                case 4:
                    resetButtons();
                    btn_StoreCard.Foreground = activeColor;
                    _PassListControl.passListUpdate(4, "");
                    break;
                case 5:
                    resetButtons();
                    btn_Generic.Foreground = activeColor;
                    _PassListControl.passListUpdate(5, "");
                    break;
            }
        }

        private void btn_All_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(0);
            searchBoxLoseFocus();
        }

        private void btn_BoardingPass_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(1);
            searchBoxLoseFocus();
        }

        private void btn_Coupon_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(2);
            searchBoxLoseFocus();
        }

        private void btn_EventTicket_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(3);
            searchBoxLoseFocus();
        }

        private void btn_StoreCard_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(4);
            searchBoxLoseFocus();
        }

        private void btn_Generic_Click(object sender, RoutedEventArgs e)
        {            
            changeCategory(5);
            searchBoxLoseFocus();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            _PassListAdd.AddPass();
            searchBoxLoseFocus();
        }

        private void btn_More_Click(object sender, RoutedEventArgs e)
        {
            searchBoxLoseFocus();
        }

        #region METHODS
        public void searchBoxLoseFocus()
        {
            if (txtBox_Search.Visibility == Visibility.Visible)
            {
                txtBox_Search.Visibility = Visibility.Collapsed;
                txtBox_Search.Text = "";
                changeCategory(App.app.ActivatedCategory);
            }
        }        

        private void resetButtons()
        {
            btn_All.Foreground = normalColor;
            btn_BoardingPass.Foreground = normalColor;
            btn_Coupon.Foreground = normalColor;
            btn_EventTicket.Foreground = normalColor;
            btn_StoreCard.Foreground = normalColor;
            btn_Generic.Foreground = normalColor;
        }

        private void doLiveSearch()
        {
            resetButtons();
            _PassListControl.passListUpdate(6, txtBox_Search.Text);
        }
        #endregion
        
    }    
}
