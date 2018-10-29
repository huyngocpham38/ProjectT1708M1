using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FormStudent.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileHandle : Page
    {
        public FileHandle()
        {
            this.InitializeComponent();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string content = this.Content.Text;
            string filename = fileName.Text;

            StorageFolder folder = KnownFolders.PicturesLibrary;
            StorageFile file = await folder.CreateFileAsync(filename,CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file,content);
            
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            string fileName = this.fileName.Text;
            StorageFolder folder =KnownFolders.PicturesLibrary;
            StorageFile file = await folder.GetFileAsync(fileName);
            string content = await FileIO.ReadTextAsync(file);
            Content.Text = content;
        }
    }
}
