using GIBInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace Veriban
{
    public partial class VeribanEFatura : IEFatura
    {
        public string ProviderId()
        {
            return "Veriban";
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            SendResult r = new SendResult();
            r.IsSucceded = true;
            r.ResultInvoices = new List<ResultInvoice>();

            foreach (var item in SendParameters.InvoicesInfo)
            {
                ServiceVeriban.TransferDocument document = new ServiceVeriban.TransferDocument();


                document.UUID = item.Invoices.UUID.ToString();
                document.BinaryDataArray = GetZipFile(item);                      //Gönderilen ZipDosyasının Binary str Array karşılığı.
                document.BinaryDataHash = GetMd5Hash(document.BinaryDataArray);   //Hash, //Gönderilen ZipBinaryDataArray in Hash karşılığı.
                document.DocumentType = ServiceVeriban.DocumentType.Xml;          //ZIP dosyası içerisindeki dosya formatı XML.
                document.FileName = item.Invoices.UUID.ToString() + ".zip";       //transfer edilecek dosya için Unique bir değere ihtiyaç vardır ve dosya uzantısı ZIP olmalı.
                document.IsDirectSend = true;                                     //true : Fatura direk imzalanarak GİB'na gönderilir. false: Fatura onay sürecinden geçtikten sonra GİB'na gönderilir.
                document.Alias = item.AliciPostaKutusuEtiketi;

                var response = service.Transfer(document, SessionID);
               
                var rr = new ResultInvoice();
                rr.ETN = item.Invoices.UUID.Value;
                rr.FaturaNo = item.Invoices.ID.Value;
                
                r.ResultInvoices.Add(rr);
            }


            return r;

        }

        static string GetMd5Hash(byte[] input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(input);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    _ = sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }



        public static byte[] GetZipFile(InvoiceInfo invoice)
        {
            byte[] bytes = invoice.Invoices.CreateBytes();

            byte[] zipFileBinaryDataArray = null;
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddEntry(invoice.Invoices.UUID.Value + ".XML", bytes);
                    zip.Save(memoryStreamOutput);
                }
                zipFileBinaryDataArray = memoryStreamOutput.ToArray();
            }
            return zipFileBinaryDataArray;
        }


        

    }
}
