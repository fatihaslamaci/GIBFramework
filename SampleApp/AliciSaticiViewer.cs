using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class AliciSaticiViewer : UserControl
    {

        private AliciSatici _AliciSatici;
        public AliciSatici AliciSatici { 
            get { return GetAliciSatici(); } 
            set { SetAliciSatici(value); } 
        }

        private AliciSatici GetAliciSatici()
        {
            return _AliciSatici;
        }
        private void SetAliciSatici(AliciSatici AliciSatici)
        {

            if (AliciSatici!=null)
            {
                if (AliciSatici.Party!=null)
                {
                    
                    if(AliciSatici.Party.PartyName!=null)
                    {
                        if (AliciSatici.Party.PartyName.Name!=null)
                        {
                            txtGondericiUnvan.Text = AliciSatici.Party.PartyName.Name.Value;
                        }
                    }


                    //PartyIdentification = new PartyIdentificationType[1] { new PartyIdentificationType() { ID = new IDType { Value = txtGondericiVkn.Text, schemeID = "VKN" } } },
                    //PostalAddress = new AddressType
                    //{
                    //    CityName = new CityNameType { Value = txtGondericiIl.Text },
                    //    StreetName = new StreetNameType { Value = txtGondericiCaddeSokak.Text },
                    //    Country = new CountryType { Name = new NameType1 { Value = txtGondericiUlke.Text } },
                    //    Room = new RoomType { Value = txtGondericiKapiNo.Text },
                    //    BuildingNumber = new BuildingNumberType { Value = txtGondericiKapiNo.Text },
                    //    CitySubdivisionName = new CitySubdivisionNameType { Value = txtGoncericiIlce.Text },


                    //},

                }
            }



            _AliciSatici = AliciSatici;
        }

        public AliciSaticiViewer()
        {
            InitializeComponent();
        }



    }
}
