using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Settings
{
    public class AppSetting
    {
        public string Secret { get; set; } = null!;

        //send mail
        public string SendGridApiKey { get; set; } = null!;
        public string EMailAddress { get; set; } = null!;
        public string NameApp { get; set; } = null!;
    }
}
