using FormStudent.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FormStudent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
       
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.FormRegister.Navigate(typeof(View.ListSong));
            Data.DataSong.InitializeDatabase();
        }
        public string CurrenTag = "";

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (CurrenTag == radio.Tag.ToString())
            {
                return;
            }
          
            switch (radio.Tag.ToString())
            {
                case "MyAccount":
                    CurrenTag = "MyAccount";
                    this.FormRegister.Navigate(typeof(View.MyAccount));
                    break;
                case "Register":
                    CurrenTag = "Register";
                    this.FormRegister.Navigate(typeof(View.RegisterMember));
                    break;
                case "Login":
                    CurrenTag = "Login";
                    this.FormRegister.Navigate(typeof(View.LoginAccount));
                    break;
                case "ListSong":
                    CurrenTag = "ListSong";
                    this.FormRegister.Navigate(typeof(View.ListSong));
                    break;
                case "CreateSong":
                    CurrenTag = "CreateSong";
                    this.FormRegister.Navigate(typeof(View.CreateSong));
                    break;
                default:
                    Debug.WriteLine(e);
                    break;
            }
        }        

        private void btn_click(object sender, RoutedEventArgs e)
        {
            this.SplitVia.IsPaneOpen = !this.SplitVia.IsPaneOpen;
        }
    }
}
