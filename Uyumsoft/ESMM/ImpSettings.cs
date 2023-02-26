using GIBInterface.ESMM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uyumsoft.ESMM
{
    public partial class ESMM : GIBInterface.ISettings
    {
        Dictionary<string, string> _Settings;
        public Dictionary<string, string> Settings
        {
            get
            {
                if (_Settings == null)
                    return DefaultSettings();
                else
                    return _Settings;
            }
            set
            {
                _Settings = value;
            }
        }

        public Dictionary<string, string> DefaultSettings()
        {
            return new Dictionary<string, string>()
            {
               {"UserName","Uyumsoft"},
                {"Password","Uyumsoft"},
                {"Uri","https://efatura-test.uyumsoft.com.tr/services/Integration" },

                {"VergiNumarasi","Buraya Gönderimi yapan firmanın Vergi numarasını yazınız"},


            };
        }
    }
}
