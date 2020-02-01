using GIBInterface;
using GIBInterface.EFaturaPaketi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBFramework
{


    public class EFatura
    {
        public IGIBData Data { get; }
        public IEFatura Provider { get; }
        
        

        public string SettingsJson { 
            get
            {
                if (Provider is ISettings)
                {
                    return JsonConvert.SerializeObject((Provider as ISettings).Settings,Formatting.Indented);
                }
                else
                {
                    return "{}";
                }
            }
            set
            {
                if (Provider is ISettings)
                {
                    (Provider as ISettings).Settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                }
            } 
        
        }

        public EFatura(IEFatura Provider, IGIBData Data)
        {
            this.Data = Data;
            this.Provider = Provider;
        }

        public EFatura(IEFatura Provider)
        {
            this.Data = new DataProviders.SQLite.Data();
            this.Provider = Provider;
        }

        public User MukellefBilgisi(string VKN_TCKN)
        {
            EgerBugunMukellefListesiOkunmadiIseOku();

            return Data.SQLiteUserFind(VKN_TCKN);
        }

        public List<User> MukellefAra(string Unvan)
        {
            EgerBugunMukellefListesiOkunmadiIseOku();

            return Data.SQLiteUserFindByUnvan(Unvan);
        }

        private void EgerBugunMukellefListesiOkunmadiIseOku()
        {
            if (Data.BugunMukelefSorgulandi() == false)
            {
                //if (isLogin == false)
                //{
                //    isLogin = LoginHazirla();
                //}

                var users = (Provider as IMukellefListesi).GetUserList();
                Data.GIBUserListSave(users);
            }
        }

        public string DefaultSettingsJson()
        {
            if (Provider is ISettings)
            {
                return JsonConvert.SerializeObject((Provider as ISettings).DefaultSettings(), Formatting.Indented);
            }
            else
            {
                return "{}";
            }
        }
    }
}
