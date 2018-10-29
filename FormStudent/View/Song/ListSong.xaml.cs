using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FormStudent.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private bool _isPlaying = false;
        private ObservableCollection<Song> listSong;
        internal ObservableCollection<Song> listSongView { get => listSong; set => listSong = value; }
        List<Song> list = new List<Song>();
        private int _currentIndex = 0;
        
        public ListSong()
        {
            this.listSongView = new ObservableCollection<Song>();
            
            this.InitializeComponent();
           // ShowAll();
           ShowSong();
        }

        private async void ShowSong()
        {   listSongView.Clear();
            string token = await DataHandle.ReadToken();
            Debug.WriteLine(token);

            var httpResponseMessage = DataHandle.GetDataToToken(ServiceURL.API_SONG, "Basic", token);

            var informationJson = await httpResponseMessage.Result.Content.ReadAsStringAsync();
           
            Debug.WriteLine(informationJson);

            ObservableCollection<Song> songModel = JsonConvert.DeserializeObject<ObservableCollection<Song>>(informationJson);

            foreach (var keySong in songModel)
            {
                listSongView.Add(keySong);
            }
        }

        private void Do_Next(object sender, RoutedEventArgs e)
        {
            Pause_Song();
            _currentIndex += 1;
            if (_currentIndex >= this.listSongView.Count)
            {
                _currentIndex = 0;
            }
            Uri mp3Link = new Uri(this.listSongView[_currentIndex].link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.listSongView[_currentIndex].name + " - " + this.listSongView[_currentIndex].author;
            this.MyListSong.SelectedIndex = _currentIndex;
            Play_Song();
        }

        private void Do_Previous(object sender, RoutedEventArgs e)
        {
            Pause_Song();
            _currentIndex -= 1;
            if (_currentIndex < 0)
            {
                _currentIndex = this.listSongView.Count - 1;
            }
            Uri mp3Link = new Uri(this.listSongView[_currentIndex].link);
            this.MyPlayer.Source = mp3Link;
            this.Song_Name.Text = this.listSongView[_currentIndex].name + " - " + this.listSongView[_currentIndex].author;
            this.MyListSong.SelectedIndex = _currentIndex;
            Play_Song();
        }

        private void Choosed_Song(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            Song choosed = panel.Tag as Song;

            _currentIndex = this.MyListSong.SelectedIndex;
            
            Uri mp3Link = new Uri(choosed.link);
            this.MyPlayer.Source = mp3Link;
            Debug.WriteLine(mp3Link);
            this.Song_Name.Text = this.listSongView[_currentIndex].name + "-" + this.listSongView[_currentIndex].author;
            Play_Song();
        }

        private void Play_Song()
        {
            _isPlaying = true;
            this.Player_Status.Text = "Song : ";
            this.MyPlayer.Play();
            this.Play_Button.Symbol = Symbol.Pause;
           
        }
        private void videoMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {   
            var totalDurationTime = MyPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            var totalDurationTime1 = MyPlayer.NaturalDuration.TimeSpan.Minutes + ":"+ MyPlayer.NaturalDuration.TimeSpan.Seconds;
            this.EndTime.Text = Convert.ToString(totalDurationTime1);
            
            //Debug.WriteLine(totalDurationTime1);
        }
        private void Pause_Song()
        {
            _isPlaying = false;
            this.Player_Status.Text = "Paused";
            this.MyPlayer.Pause();
            this.Play_Button.Symbol = Symbol.Play;
        }

        private void Click_Play(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                Pause_Song();
            }
            else
            {
                Play_Song();
            }
        }

        private void ShowAll()
        {
            Data.DataSong.InitializeDatabase();
            //User entries = new User();
            listSongView.Clear();
            using (SqliteConnection db =
                new SqliteConnection("Filename=SongData.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * from ListSong", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    listSongView.Add(new Song
                    {
                        name = query.GetString(1),
                        thumbnail = query.GetString(2),
                        author = query.GetString(4),
                        link = query.GetString(3),
                    });
                }
                db.Close();
            }
        }
    }
}
