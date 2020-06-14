using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Veriban
{
    public partial class VeribanEFatura : IMukellefListesi
    {
        public UserList GetUserList()
        {
            FtpInfo ftpInfo = new FtpInfo();
            ftpInfo.UserName = Settings["ftp_UserName"];
            ftpInfo.Password = Settings["ftp_Password"];
            ftpInfo.Adress = Settings["ftp_Adress"];

            return MukellefListesi.GetUserList(ftpInfo, "");
        }
    }


    public class FtpInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
    }

    public static class MukellefListesi
    {

        public static GIBInterface.EFaturaPaketi.UserList GetUserList(FtpInfo ftpInfo, string TempFolder)
        {
            GIBInterface.EFaturaPaketi.UserList r = null;
            string FileName = TempFolder + GetFileName(DateTime.Now);

            if (File.Exists(FileName) == false)
            {
                DownloadFtpUserList(ftpInfo, FileName);
                for (int i = 1; i < 12; i++)
                {
                    try
                    {
                        File.Delete(TempFolder + GetFileName(DateTime.Now.AddDays(-1 * (i))));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            var s = System.IO.File.ReadAllText(FileName, System.Text.Encoding.UTF8);

            r = Tools.XmlSerialization.XMLDeserialize<GIBInterface.EFaturaPaketi.UserList>(s);

            return r;
        }
        private static void DownloadFtpUserList(FtpInfo ftpInfo, string FileName)
        {
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpInfo.UserName, ftpInfo.Password);
                client.DownloadFile(ftpInfo.Adress, FileName);
            }
        }

        private static string GetFileName(DateTime now)
        {
            return "GIBUserList" + now.ToString("yyyyMMdd") + ".xml";
        }
    }


}
