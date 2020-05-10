using GIBInterface;
using GIBInterface.EFaturaPaketi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBProviders.SahteEntegrator
{
        public partial class EFatura : IMukellefListesi
        {
            public UserList GetUserList()
            {
                UserList r = new UserList();

            var xml = @"
<UserList>
    <User>
        <Identifier>1111113261</Identifier>
        <Title>Sapaş Sakıpaga Gıda Pazarlama A.Ş. Test Kullanıcısı</Title>
        <Type>OZEL</Type>
        <FirstCreationTime>2014-03-19T10:54:25</FirstCreationTime>
        <AccountType>GIBPORTAL</AccountType>
        <Documents>
            <Document type=""Invoice"">
                <Alias>
                    <Name>urn:mail:defaultpk@sakipaga.com.tr</Name>
                    <CreationTime>2014-03-19T10:54:25</CreationTime>
                </Alias>
            </Document>
        </Documents>
    </User>
    <User>
        <Identifier>2970610282</Identifier>
        <Title>DİMAK DİREN MAKİNE ARAŞTIRMA GELİŞTİRME ELEKTRİK VE GIDA TEKNOLOJİLERİ İNOVASYON YAZILIM SANAYİ VE TİCARET LİMİTED ŞİRKETİ</Title>
        <Type>OZEL</Type>
        <FirstCreationTime>2017-11-30T23:25:33</FirstCreationTime>
        <AccountType>ENTEGRASYON</AccountType>
        <Documents>
            <Document type=""Invoice"">
                <Alias>
                    <Name>urn:mail:defaultpk@dimes.com.tr</Name>
                    <CreationTime>2019-07-22T13:58:28</CreationTime>
                </Alias>
                <Alias>
                    <Name>urn:mail:defaultpk@dimes.com.tr</Name>
                    <CreationTime>2017-11-30T23:25:33</CreationTime>
                    <DeletionTime>2017-12-11T16:44:31</DeletionTime>
                </Alias>
            </Document>
            <Document type=""DespatchAdvice"">
                <Alias>
                    <Name>urn:mail:defaultpk@dimes.com.tr</Name>
                    <CreationTime>2019-07-22T13:58:28</CreationTime>
                </Alias>
            </Document>
        </Documents>
    </User>
</UserList>
";


                r = Tools.XmlSerialization.XMLDeserialize<UserList>(xml);
                
                return r;
            }
        }

}
