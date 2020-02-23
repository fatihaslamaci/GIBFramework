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
        private IGIBData Data { get; }
        public IEFatura Provider { get; }



        public string SettingsJson
        {
            get
            {
                if (Provider is ISettings)
                {
                    return JsonConvert.SerializeObject((Provider as ISettings).Settings, Formatting.Indented);
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

        public bool isLogin { get; private set; }

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


        public GIBInterface.UBLTR.InvoiceType ManipulatedInvoice(GIBInterface.UBLTR.InvoiceType invoice)
        {
            if (Provider is IManipulatedInvoice)
            {
                return (Provider as IManipulatedInvoice).Manipulated(invoice);
            }
            else
            {
                return invoice;
            }
        }

        public List<SendInvoiceData> SendInvoiceList(SendInvoiceListDataFind val)
        {

            return Data.SendInvoiceList(val,Provider.ProviderId());
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            SendResult r = new SendResult();

            Login();

            if (Provider is IManipulatedInvoice)
            {
                foreach (var item in SendParameters.InvoicesInfo)
                {
                    item.Invoices = (Provider as IManipulatedInvoice).Manipulated(item.Invoices);
                }
            }

            

            SendParameters = Data.SendInvoiceInsert(SendParameters, Provider.ProviderId());
            try
            {
                if (Provider is ILogin)
                {
                    // TODO Login işlemi gerekiyorsa yapılacak
                }

                // TODO : Gerekli validasyon işlemleri yapılacak
                if (true == true)
                {
                    r = Provider.SendInvoice(SendParameters);
                    Data.SendInvoiceUpdate(SendParameters, r);
                }
            }
            catch (Exception ex)
            {
                r.Error = ex.Message;
                r.ErrorDetail += (ex.StackTrace + "\n\r");
                if (ex.InnerException != null)
                {
                    r.ErrorDetail += ("\n\r");
                    r.ErrorDetail += ("InnerException: \n\r");
                    r.ErrorDetail += (ex.InnerException.Message + "\n\r");
                    r.ErrorDetail += (ex.InnerException.StackTrace + "\n\r");
                }
                Data.SendInvoiceUpdate(SendParameters, r);
            }
            return r;

        }

        public List<QueryStatusResponseData> FaturaDurumSorgula(List<QueryStatusParameters> SendParameters)
        {
            List<QueryStatusResponseData> r = new List<QueryStatusResponseData>();

            if (Provider is IFaturaDurumuSorgula)
            {
                Login();
                var invoiceStatus  = (Provider as IFaturaDurumuSorgula).InvoiceStatus(SendParameters);

                foreach (var item in SendParameters)
                {
                    QueryStatusResponseData statusData = new QueryStatusResponseData();

                    statusData.RecordId = item.RecordId;
                    statusData.InvoiceUUID = item.InvoiceUUID;

                    var findSatus = invoiceStatus.Find(x => x.InvoiceUUID == item.InvoiceUUID);

                    if (findSatus !=null)
                    {
                        statusData.InvoiceStatus = findSatus.InvoiceStatus;
                        statusData.Message = findSatus.Message;
                    }
                    else
                    {
                        statusData.Message = "Fatura Bulunamadı";
                        statusData.InvoiceStatus = QueryStatus.BasarisizSonuclandi;
                    }

                    r.Add(statusData);
                }
                
                Data.DurumSorgulamaYaz(r);
            }
            return r;
        }


        private void EgerBugunMukellefListesiOkunmadiIseOku()
        {
            if (Data.BugunMukelefSorgulandi() == false)
            {
                Login();
                var users = (Provider as IMukellefListesi).GetUserList();
                Data.GIBUserListSave(users);
            }
        }


        private void Login()
        {
            if (isLogin == false)
            {
                if (Provider is ILogin)
                {
                    isLogin = (Provider as ILogin).Login();
                }
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
