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

            SQLiteTools.Service sqlservice = new SQLiteTools.Service(SQLiteFileName);
            sqlservice.CreateOrAlterDB(GetSchema());

        }
        public SQLiteConnection NewSQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + SQLiteFileName + ";Version=3;");
        }
        internal string GetSchema()
        {

            return @"
CREATE TABLE IF NOT EXISTS GIB_UserList (
                            identifier        TEXT PRIMARY KEY,
                            title             TEXT,
                            type              INTEGER,
                            firstCreationTime DATETIME,
                            accountType       INTEGER
);

                            
CREATE TABLE IF NOT EXISTS GIB_Alias (
                            id                INTEGER PRIMARY KEY,
                            identifier        TEXT,
                            type              INTEGER,
                            name              TEXT,
                            creationTime      DATETIME,     
                            deletionTime      DATETIME,
                            FOREIGN KEY(identifier) REFERENCES GIB_UserList(identifier)
);


CREATE TABLE IF NOT EXISTS GIB_SorguZamani (
                            updateTime        DATETIME  
);

CREATE TABLE IF NOT EXISTS GIB_Invoices (
                            id               INTEGER PRIMARY KEY,
                            ETN              TEXT,
                            invoiceXML       TEXT,
                            send_isSucceded  BOOLEAN,
                            send_Message     TEXT,
                            send_Error       TEXT,
                            send_ErrorDetail TEXT,
                            send_returnETN        TEXT,
                            send_returnFaturaNo   TEXT
                            
);



";

        }

    }
}
