using GIBInterface;
using GIBInterface.UBLTR;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

            GetDoc(SendParameters);



            throw new NotImplementedException();

        }

        public ServiceLogo.DocumentType GetDoc(SendParameters SendParameters)
        {
            ServiceLogo.DocumentType DocType = new ServiceLogo.DocumentType();

            var zip = compressed(SendParameters);
            
            //TODO :buradaki file name için DB de Zarf ID oluşturulacak. henüz yapılmadı.
            DocType.fileName = Guid.NewGuid() + ".zip";
            DocType.binaryData = new ServiceLogo.base64BinaryData();
            DocType.binaryData.Value = zip;
            //TODO : hash işlemi yapılacak
            //DocType.hash = ;


            return DocType;
        }


        public byte[] compressed(SendParameters SendParameters)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    foreach (var invoice in SendParameters.InvoicesInfo)
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
