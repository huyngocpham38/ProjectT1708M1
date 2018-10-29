using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using FormStudent.Entity;
using FormStudent.Handle;
using FormStudent.Services;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FormStudent.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateSong : Page
    {
        Song currenMember = new Song();
        public CreateSong()
        {
            this.InitializeComponent();
           
        }

        private async void btn_Save(object sender, RoutedEventArgs e)
        {
            currenMember.name = this.Name.Text;
            currenMember.link = this.Link.Text;
            currenMember.author = this.Author.Text;
            currenMember.thumbnail = this.Thumbnail.Text;
            currenMember.description = this.Description.Text;
            currenMember.singer = this.Singer.Text;

            string jsonMember = JsonConvert.SerializeObject(this.currenMember);
            Debug.WriteLine(jsonMember);

            string token = await DataHandle.ReadToken();
            Debug.WriteLine(token);

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");

            var response = httpClient.PostAsync(ServiceURL.API_SONG , content);

            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                var contents = await response.Result.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);
                Debug.WriteLine("Success");
            }
            else
            {
                var contents = await response.Result.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);
                Debug.WriteLine(token);
            }

        }
    }
}
