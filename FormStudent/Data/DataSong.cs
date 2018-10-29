using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace FormStudent.Data
{
    class DataSong
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=SongData.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                                      "EXISTS ListSong (id INTEGER IDENTITY PRIMARY KEY, " +
                                      "Name NVACHAR(50) NOT NULL,Thumbnail NVACHAR(50),Link NVACHAR(50) NOT NULL, Author NVACHAR(50))";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
    }
}
