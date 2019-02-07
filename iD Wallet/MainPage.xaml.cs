using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using iD_Wallet.Classes;
using iD_Wallet.Controls;
using Windows.UI.Popups;

namespace iD_Wallet
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Color.FromArgb(100, 20, 20, 20);
            titleBar.ForegroundColor = Color.FromArgb(100, 185, 185, 185);
            titleBar.ButtonBackgroundColor = Color.FromArgb(100, 20, 20, 20);
            titleBar.ButtonForegroundColor = Color.FromArgb(100, 185, 185, 185);
        }

        void ResizePassListView()
        {
            passListControl.Height = mainPage.ActualHeight - mainControl.ActualHeight - passListControl.Margin.Top;
        }

        private void mainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ResizePassListView();
        }

        private void mainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizePassListView();
        }
    }
}
