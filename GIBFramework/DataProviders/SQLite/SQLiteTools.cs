using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace GIBFramework.DataProviders.SQLiteTools
{
    internal class TableProp
    {
        //public Int64 Cid { set; get; }
        public string Name { set; get; }
        //public string Type { set; get; }
        //public bool Notnull { set; get; }
        //public string Dflt_value { set; get; }
        //public bool Pk { set; get; }
    }

    public class Service
    {

        private string dbFileName { set; get; }

        public Service(string dbFileName)
        {
            this.dbFileName = dbFileName;
        }

        public void CreateOrAlterDB(string dbSchema)
        {
            if (!System.IO.File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }

            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                using (SQLiteCommand cmd = new SQLiteCommand(dbSchema, con))
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            AlterDb(dbSchema);
        }
        public SQLiteConnection NewSQLiteConnection()
        {
            return new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
        }

        private void AlterDb(string dbSchema)
        {
            char[] ayrac = { ',', '\n', '\r' };
            var s = dbSchema.Split(ayrac, System.StringSplitOptions.RemoveEmptyEntries);
            string tableName = "";
            List<TableProp> tableProp = new List<TableProp>();
            foreach (var item in s)
            {
                var satir = item.Trim();
                satir = satir.Replace('\t', ' ');
                satir = satir.Replace("  ", " ");
                satir = satir.Replace("  ", " ");
                satir = satir.Replace("  ", " ");

                if ((string.IsNullOrWhiteSpace(satir)) || (satir.IndexOf("FOREIGN KEY(") >= 0) || (satir.IndexOf(";") >= 0) || (satir == ")"))
                {
                    continue;
                }
                string tempTable = TableName(satir);

                if (!string.IsNullOrWhiteSpace(tempTable))
                {
                    tableName = tempTable;
                    tableProp = GetTableProp(tableName);
                    continue;
                }

                string fieldName = GetFieldName(satir);
                if (tableProp.FirstOrDefault(i => (i.Name == fieldName)) == null)
                {
                    addColumn(tableName, satir);
                }
            }
        }

        private void addColumn(string tableName, string satir)
        {
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                    cmd.CommandText = "ALTER TABLE " + tableName + " ADD COLUMN " + satir;
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private string GetFieldName(string satir)
        {
            var index = satir.IndexOf(" ");
            string r = satir.Substring(0, index);
            return r;
        }

        private List<TableProp> GetTableProp(string tableName)
        {
            List<TableProp> r = new List<TableProp>();
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
                    cmd.CommandText = $"PRAGMA TABLE_INFO({tableName})";
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities
                    cmd.CommandType = CommandType.Text;
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableProp tableProp = new TableProp();
                            //tableProp.Cid= reader["Cid"].ToString();
                            tableProp.Name = reader["Name"].ToString();
                            //tableProp.Type = reader["Type"].ToString();
                            //tableProp.Notnull = reader["Notnull"].ToString();
                            //tableProp.Dflt_value = reader["Dflt_value"].ToString();
                            //tableProp.Pk = reader["Pk"].ToString();
                            r.Add(tableProp);
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return r;
        }

        private string TableName(string satir)
        {
            var ara = "CREATE TABLE IF NOT EXISTS";
            string r = "";
            var index = satir.IndexOf("CREATE TABLE IF NOT EXISTS");
            var aralen = ara.Length;
            if (index >= 0)
            {
                var s = satir.Substring(index + aralen);
                r = s.Replace('(', ' ').Trim();
            }
            return r;
        }
    }

    public static class DBNullConvert
    {
        public static long? AsLongNull(this object item, long? defaultValue = default(long?))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultValue;
            long result;
            if (!long.TryParse(item.ToString(), out result))
                return defaultValue;
            return result;
        }

        public static double? AsDoubleNull(this object item, double? defaultValue = default(double?))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultValue;
            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultValue;
            return result;
        }


        public static int? AsIntNull(this object item, int? defaultValue = default(int?))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultValue;
            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultValue;
            return result;
        }

        public static string AsStringNull(this object item, string defaultValue = default(string))
        {
            if (item == null || item.Equals(System.DBNull.Value))
            {
                return defaultValue;
            }
            return item.ToString().Trim();
        }

        public static bool? AsBoolNull(this object item, bool? defaultValue = default(bool?))
        {
            if (item == null || item.Equals(System.DBNull.Value))
                return defaultValue;

            return new List<string>() { "yes", "y", "true" }.Contains(item.ToString().ToLower());
        }

        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return defaultDateTime;

            return result;
        }

    }
}
