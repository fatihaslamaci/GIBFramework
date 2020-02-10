using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIBFramework.SQLiteTools;

namespace GIBFramework.DataProviders.SQLite
{
    public partial class Data : IGIBData
    {
        public void GIBUserListSave(UserList userList)
        {
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        cmd.Transaction = tr;

                        cmd.CommandText = @"
Delete From GIB_UserList; 
Delete From GIB_Alias; 
Delete From GIB_SorguZamani; 
Insert Into GIB_SorguZamani(updateTime) Values(@updateTime);
";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SQLiteParameter("@updateTime", DateTime.Now.Date));
                        cmd.ExecuteNonQuery();

                        foreach (var user in userList.User)
                        {
                            cmd.CommandText = "Insert Into GIB_UserList (identifier,title,type,firstCreationTime,accountType) Values (@identifier,@title,@type,@firstCreationTime,@accountType)";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SQLiteParameter("@identifier", user.Identifier));
                            cmd.Parameters.Add(new SQLiteParameter("@title", user.Title));
                            cmd.Parameters.Add(new SQLiteParameter("@type", user.Type));
                            cmd.Parameters.Add(new SQLiteParameter("@firstCreationTime", user.FirstCreationTime));
                            cmd.Parameters.Add(new SQLiteParameter("@accountType", user.AccountType));

                            cmd.ExecuteNonQuery();

                            foreach (var document in user.Documents)
                            {

                                foreach (var alias in document.Alias)
                                {
                                    foreach (var name in alias.Name)
                                    {
                                        cmd.CommandText = "Insert Into GIB_Alias (identifier,type,name,creationTime,deletionTime) Values (@identifier,@type,@name,@creationTime,@deletionTime)";
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.Add(new SQLiteParameter("@identifier", user.Identifier));
                                        cmd.Parameters.Add(new SQLiteParameter("@type", document.type));
                                        cmd.Parameters.Add(new SQLiteParameter("@name", name));
                                        cmd.Parameters.Add(new SQLiteParameter("@creationTime", alias.CreationTime));
                                        cmd.Parameters.Add(new SQLiteParameter("@deletionTime", alias.DeletionTime));
                                        cmd.ExecuteNonQuery();
                                    }

                                }
                            }

                        }

                    }
                    tr.Commit();
                }
                con.Close();
            }

        }

        public User SQLiteUserFind(string VergiNo)
        {
            User r = new User();

            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "Select * from GIB_UserList where identifier=@identifier";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@identifier", VergiNo));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            r.Identifier = reader["identifier"].ToString();
                            r.Title = reader["title"].ToString();
                            r.Type = (UsrType)reader["type"].AsIntNull();
                            r.FirstCreationTime = reader["firstCreationTime"].AsDateTime().ToString("yyy-MM-ddThh.mm.ss");
                            r.AccountType = (AccType)reader["AccountType"].AsIntNull();
                            r.AccountTypeSpecified = true;

                        }
                        reader.Close();
                    }

                    cmd.CommandText = "Select * from GIB_Alias where identifier=@identifier and type=@type ";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@identifier", VergiNo));
                    cmd.Parameters.Add(new SQLiteParameter("@type", DocType.Invoice));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        DocumentType doc1 = new DocumentType();
                        doc1.type = DocType.Invoice;
                        doc1.typeSpecified = true;
                        List<AliasType> aliasList = new List<AliasType>();

                        while (reader.Read())
                        {
                            var alias = new AliasType();
                            alias.Name = new string[1];
                            alias.Name[0] = reader["name"].ToString();
                            alias.CreationTime = reader["creationTime"].AsDateTime().ToString("yyy-MM-ddThh.mm.ss");
                            alias.DeletionTime = reader["deletionTime"].AsDateTime().ToString("yyy-MM-ddThh.mm.ss");
                            aliasList.Add(alias);
                        }
                        doc1.Alias = aliasList.ToArray();
                        r.Documents = new DocumentType[1];
                        r.Documents[0] = doc1;
                        reader.Close();

                    }
                }
                con.Close();
            }

            return r;
        }

        public List<User> SQLiteUserFindByUnvan(string Unvan)
        {
            List<User> r = new List<User>();
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = @"
select
   GIB_UserList.identifier userIdentifier
  ,GIB_UserList.title userTitle
  ,GIB_UserList.type userType
  ,GIB_UserList.firstCreationTime userFirstCreationTime
  ,GIB_UserList.accountType userAccountType

  ,GIB_Alias.type as aliasType
  ,GIB_Alias.name as aliasName
  ,GIB_Alias.creationTime as aliasCreationTime
  ,GIB_Alias.deletionTime as aliasDeletionTime


from GIB_UserList left join GIB_Alias On GIB_UserList.identifier = GIB_Alias.identifier
where GIB_UserList.title LIKE @title
";

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@title", Unvan + "%"));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {

                        string Identifier = "";
                        while (reader.Read())
                        {
                            if (Identifier != reader["userIdentifier"].ToString())
                            {
                                Identifier = reader["userIdentifier"].ToString();
                                User user = new User();
                                user.Identifier = reader["userIdentifier"].ToString();
                                user.Title = reader["userTitle"].ToString();
                                user.Type = (UsrType)reader["userType"].AsIntNull();
                                user.FirstCreationTime = reader["userFirstCreationTime"].AsDateTime().ToString("yyy-MM-ddThh.mm.ss");
                                user.AccountType = (AccType)reader["userAccountType"].AsIntNull();
                                user.AccountTypeSpecified = true;
                                r.Add(user);
                            }
                            else
                            {
                                //TODO : Aliasların eklenmesi yapılacak.
                            }

                        }
                        reader.Close();
                    }
                }
                con.Close();
            }

            return r;

        }


        public bool BugunMukelefSorgulandi()
        {
            var r = false;

            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {

                    cmd.CommandText = "Select * from GIB_SorguZamani where updateTime=@updateTime Limit 1";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SQLiteParameter("@updateTime", DateTime.Now.Date));
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            r = true;
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return r;
        }

        public SendParameters SendInvoiceInsert(SendParameters sendParameters)
        {
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        cmd.Transaction = tr;
                        cmd.CommandText = "Insert Into GIB_Invoices(ETN,invoiceXML) Values (@ETN, @invoiceXML); SELECT last_insert_rowid()";
                        cmd.CommandType = CommandType.Text;
                        foreach (var item in sendParameters.InvoicesInfo)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SQLiteParameter("@ETN", item.Invoices.UUID.Value));
                            cmd.Parameters.Add(new SQLiteParameter("@invoiceXML", item.Invoices.CreateXml()));
                            item.RecordId = Convert.ToInt64(cmd.ExecuteScalar());
                        }
                    }
                    tr.Commit();
                }
                con.Close();
            }

            return sendParameters;

        }

        public void SendInvoiceUpdate(SendParameters sendParameters, SendResult r)
        {
            if (r.IsSucceded == false)
            {
                HataYaz(sendParameters, r);
            }
            else
            {
                BasariYaz(sendParameters, r);
            }
        }

        private void HataYaz(SendParameters sendParameters, SendResult r)
        {
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {


                        cmd.Transaction = tr;
                        cmd.CommandText = @"
Update GIB_Invoices set 
 send_isSucceded=@send_isSucceded 
,send_Message=@send_Message
,send_Error=@send_Error
,send_ErrorDetail=@send_ErrorDetail
where id=@id";
                        cmd.CommandType = CommandType.Text;
                        foreach (var item in sendParameters.InvoicesInfo)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SQLiteParameter("@id", item.RecordId));
                            cmd.Parameters.Add(new SQLiteParameter("@send_isSucceded", r.IsSucceded));
                            cmd.Parameters.Add(new SQLiteParameter("@send_Message", r.Message));
                            cmd.Parameters.Add(new SQLiteParameter("@send_Error", r.Error));
                            cmd.Parameters.Add(new SQLiteParameter("@send_ErrorDetail", r.ErrorDetail));

                            cmd.ExecuteNonQuery();
                        }


                    }
                    tr.Commit();
                }
                con.Close();
            }
        }

        private void BasariYaz(SendParameters sendParameters, SendResult r)
        {
            using (SQLiteConnection con = NewSQLiteConnection())
            {
                con.Open();
                using (SQLiteTransaction tr = con.BeginTransaction())
                {
                    using (SQLiteCommand cmd = con.CreateCommand())
                    {


                        cmd.Transaction = tr;
                        cmd.CommandText = @"
Update GIB_Invoices set 
 send_isSucceded=@send_isSucceded 
,send_Message=@send_Message
,send_returnETN        = @send_returnETN
,send_returnFaturaNo   = @send_returnFaturaNo

where id=@id";
                        cmd.CommandType = CommandType.Text;
                        foreach (var item in r.ResultInvoices)
                        {
                            var invoice = sendParameters.InvoicesInfo.Find(x => x.LocalDocumentId == item.FaturaNo);

                            cmd.Parameters.Clear();
                            cmd.Parameters.Add(new SQLiteParameter("@id", invoice.RecordId));
                            cmd.Parameters.Add(new SQLiteParameter("@send_isSucceded", r.IsSucceded));
                            cmd.Parameters.Add(new SQLiteParameter("@send_Message", r.Message));

                            cmd.Parameters.Add(new SQLiteParameter("@send_returnETN", item.ETN ));
                            cmd.Parameters.Add(new SQLiteParameter("@send_returnFaturaNo",item.FaturaNo));

                            cmd.ExecuteNonQuery();
                        }


                    }
                    tr.Commit();
                }
                con.Close();
            }
        }
    }
}
