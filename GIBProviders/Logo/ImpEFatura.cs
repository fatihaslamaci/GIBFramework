using GIBInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GIBProviders.Logo
{
    public partial class EFatura : IEFatura
    {
        public string ProviderId()
        {
            return "Logo";
        }

        public SendResult SendInvoice(SendParameters SendParameters)
        {
            SendResult r = new SendResult();

            var grup = SendParameters.InvoicesInfo.GroupBy(x => new { x.Customer.VknTckn, x.Customer.Alias });
            foreach (var item in grup)
            {

                var Invoices = item.ToList();
                var DocType = GetDoc(Guid.NewGuid(), Invoices);

                if (service.sendInvoice(DocType, item.Key.Alias, SessionID))
                {
                    r.IsSucceded = true;
                    r.ResultInvoices = new List<ResultInvoice>();
                    foreach (var invoice in Invoices)
                    {
                        ResultInvoice rr = new ResultInvoice();
                        rr.ETN = invoice.Invoices.UUID.Value;
                        rr.FaturaNo = invoice.Invoices.ID.Value;
                        r.ResultInvoices.Add(rr);
                    }
                }
                else
                {
                    r.IsSucceded = false;
                    r.ResultInvoices = new List<ResultInvoice>();
                    foreach (var invoice in Invoices)
                    {
                        ResultInvoice rr = new ResultInvoice();
                        r.ResultInvoices.Add(rr);
                    }
                }
            }

            return r;
        }

        public ServiceLogo.DocumentType GetDoc(Guid ZarfId, List<InvoiceInfo> InvoicesInfo)
        {
            ServiceLogo.DocumentType DocType = new ServiceLogo.DocumentType();

            var zip = compressed(InvoicesInfo);

            //TODO :buradaki file name için DB de Zarf ID oluşturulacak. henüz yapılmadı.
            DocType.fileName = ZarfId + ".zip";
            DocType.binaryData = new ServiceLogo.base64BinaryData();
            DocType.binaryData.Value = zip;
            DocType.hash = GetMd5Hash(zip);

            return DocType;
        }


        static string GetMd5Hash(byte[] input)
        {
            using (MD5 md5Hash = MD5.Create())
            {

                byte[] data = md5Hash.ComputeHash(input);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }


        public byte[] compressed(List<InvoiceInfo> InvoicesInfo)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var invoice in InvoicesInfo)
                    {
                        var zipEntry = zipArchive.CreateEntry(invoice.Invoices.UUID.Value + ".xml");
                        byte[] bytes = invoice.Invoices.CreateBytes();
                        using (MemoryStream originalFileStream = new MemoryStream(bytes))
                        {
                            using (var zipEntryStream = zipEntry.Open())
                            {
                                originalFileStream.CopyTo(zipEntryStream);
                            }
                        }
                    }
                }
                return compressedFileStream.ToArray();
            }
        }


    }
}
