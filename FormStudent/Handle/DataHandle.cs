using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Storage;
using FormStudent.Entity;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace FormStudent.Handle
{
    class DataHandle
    {
        private static string _filename = "demo.txt";
        private static string _fodlename = "presonal";

        public async static Task<HttpResponseMessage> GetDataToToken(string api_url, string scheme, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
            var response = httpClient.GetAsync(api_url);

            return response.Result;
        }

        public async static Task<string> ReadToken()
        {
            StorageFolder folder = await KnownFolders.PicturesLibrary.GetFolderAsync(_fodlename);
            StorageFile file = await folder.GetFileAsync(_filename);
            string json = await FileIO.ReadTextAsync(file);
            return json;
        }

        public async static void WriteToken(string abc)
        {
            StorageFolder folder = await KnownFolders.PicturesLibrary.GetFolderAsync(_fodlename);
            StorageFile file = await folder.GetFileAsync(_filename);
            await FileIO.WriteTextAsync(file, abc);
        }

        //public static void AddData(Song obj)
        //{
        //    using (SqliteConnection db =
        //        new SqliteConnection("Filename=SongData.db"))
        //    {
        //        db.Open();

        //        SqliteCommand insertCommand = new SqliteCommand();
        //        insertCommand.Connection = db;

        //        // Use parameterized query to prevent SQL injection attacks
        //        insertCommand.CommandText = "INSERT INTO ListSong(name, thumbnail,link,author) VALUES (@thumbnail, @name, @link,@author);";

        //        insertCommand.Parameters.AddWithValue("@thumbnail", obj.name);
        //        insertCommand.Parameters.AddWithValue("@name", obj.thumbnail);
        //        insertCommand.Parameters.AddWithValue("@link", obj.link);
        //        insertCommand.Parameters.AddWithValue("@author", obj.author);
        //        insertCommand.ExecuteReader();

        //        db.Close();
        //    }
        //}
    }
}
