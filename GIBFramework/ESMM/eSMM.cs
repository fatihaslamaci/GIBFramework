using GIBInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GIBFramework.ESMM
{
    public class ESMM
    {
        private IGIBData Data { get; }

        public GIBInterface.ESMM.IESMM Provider { get; }

        public string SettingsJson
        {
            get
            {
                if (Provider is GIBInterface.ISettings)
                {
                    return JsonConvert.SerializeObject((Provider as GIBInterface.ISettings).Settings, Formatting.Indented);
                }
                else
                {
                    return "{}";
                }
            }
            set
            {
                if (Provider is GIBInterface.ISettings)
                {
                    (Provider as GIBInterface.ISettings).Settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
                }
            }

        }


        private GIBInterface.TokenType token { get; set; }

        public ESMM(GIBInterface.ESMM.IESMM Provider, IGIBData Data)
        {
            this.Data = Data;
            this.Provider = Provider;
        }

        public ESMM(GIBInterface.ESMM.IESMM Provider)
        {
            this.Data = new DataProviders.SQLite.Data();
            this.Provider = Provider;
        }

        public GIBInterface.ESMM.SendResultESMM Send(GIBInterface.ESMM.SendParametersESMM SendParameters)
        {
            GIBInterface.ESMM.SendResultESMM r = new GIBInterface.ESMM.SendResultESMM();
            Login();
            r = Provider.Send(SendParameters);


            return r;
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

        private void Login()
        {
            if (isLogin == false)
            {
                var ILogin = (Provider as ILogin);
                if (ILogin.Login())
                {
                    token.TokenId = ILogin.TokenId();
                    token.Token = ILogin.Token();
                    token.CreationTime = DateTime.Now;
                    token.Id = Data.InsertToken(token);
                }
            }
        }

        public bool isLogin
        {
            get
            {
                bool r = false;
                if (Provider is ILogin)
                {
                    var ILogin = (Provider as ILogin);
                    if (token == null)
                    {
                        token = Data.GetToken(ILogin.TokenId());
                        ILogin.LoadToken(token.Token);
                    }

                    if (TokenSonKullanmaZamaniDolmadi(ILogin.ExpirationTimeMinute()))
                    {
                        r = true;
                    }

                }
                else
                {
                    r = true;
                }

                return r;
            }
        }

        private bool TokenSonKullanmaZamaniDolmadi(int dakika)
        {
            return token.CreationTime.AddMinutes(dakika) > DateTime.Now;
        }


    }
}
