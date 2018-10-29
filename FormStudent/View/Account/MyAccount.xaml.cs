using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class MyAccount : Page
    {
        private ObservableCollection<Member> listMembers;
        internal ObservableCollection<Member> listAccount { get => listMembers; set => listMembers = value; }
       
        private int _currentIndex = 0;

        public MyAccount()
        {
            this.listAccount = new ObservableCollection<Member>();
            this.InitializeComponent();
            LoadMember();
        }

        public async void LoadMember()
        {
            listAccount.Clear();

            string token = await DataHandle.ReadToken();
            Debug.WriteLine(token);
            var httpResponseMessage = DataHandle.GetDataToToken(ServiceURL.API_GET_MyAccount,"Basic",token);

            var informationJson = await httpResponseMessage.Result.Content.ReadAsStringAsync();

            Member member = JsonConvert.DeserializeObject<Member>(informationJson);
            

            Debug.WriteLine(member.email);
        }
    }
}
