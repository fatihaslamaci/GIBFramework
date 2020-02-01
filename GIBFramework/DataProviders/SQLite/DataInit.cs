using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBFramework.DataProviders.SQLite
{
    public partial class Data
    {
        public string SQLiteFileName { get; set; }
        public Data()
        {
            SQLiteFileName = "TestDB.sqlite3";
            SQLiteDbInit();
        }
        internal void SQLiteDbInit()
        {
            if (!System.IO.File.Exists(SQLiteFileName))
            {
                SQLiteConnection con;
                SQLiteConnection.CreateFile(SQLiteFileName);

        string sql = @"CREATE TABLE GIB_UserList(
                            identifier        TEXT PRIMARY KEY,
                            title             TEXT,
                            type              INTEGER,
                            firstCreationTime DATETIME,
                            accountType       INTEGER
                        );

                        --CREATE INDEX ix_GIB_UserList_title ON GIB_UserList(title);
                            
                        CREATE TABLE GIB_Alias(
                            id                INTEGER PRIMARY KEY,
                            identifier        TEXT,
                            type              INTEGER,
                            name              TEXT,
                            creationTime      DATETIME,     
                            deletionTime      DATETIME,
                            FOREIGN KEY(identifier) REFERENCES GIB_UserList(identifier)
                        );

                        CREATE TABLE GIB_SorguZamani(
                            updateTime        DATETIME  
                        );

";
                con = NewSQLiteConnection();
                con.Open();
                var cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        private SQLiteConnection NewSQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + SQLiteFileName + ";Version=3;");
        }


    }
}
