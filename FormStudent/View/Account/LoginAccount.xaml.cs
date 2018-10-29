using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FormStudent.Entity;
using FormStudent.Handle;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FormStudent.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginAccount : Page
    {
        public static string _filename = "demo.txt";
        public static string _fodlename = "presonal";
        
        Member currenMember = new Member();
        public LoginAccount()
        {
            this.InitializeComponent();
            
        }

        private async void Submit_Form(object sender, RoutedEventArgs e)
        {
            this.currenMember.email = this.Email.Text;
            this.currenMember.password = this.Password.Password;

            string jsonMember = JsonConvert.SerializeObject(this.currenMember);
            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Services.ServiceURL.API_LOGIN , content);
            var contents = await response.Result.Content.ReadAsStringAsync();

            KeyLogin keyLogin = JsonConvert.DeserializeObject<KeyLogin>(contents);

            Debug.WriteLine(response);
            Debug.WriteLine(contents);// chuoi Json

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                
                Debug.WriteLine(keyLogin.token);
                Debug.WriteLine(keyLogin.userId);
                DataHandle.WriteToken(keyLogin.token);

                this.Login_Success.Text = "Login Success";
                this.Login_Success.Visibility = Visibility.Visible;
                this.Login_Error.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Login_Error.Text = "Invalid account or password";
                this.Login_Error.Visibility = Visibility.Visible;
                this.Login_Success.Visibility = Visibility.Collapsed;
            }

            //Debug.WriteLine(keyLogin.token);
            //Debug.WriteLine(keyLogin.status);

            //Debug.WriteLine(keyLogin.secretToken);
            //Debug.WriteLine(keyLogin.userId);

        }

        private void password_validate(object sender, KeyRoutedEventArgs e)
        {
            string regex = "^[a-zA-Z0-9]{3-10}$";
           
        }
    }
}
