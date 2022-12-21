using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class MailSendInfoBindingModel
    {
        public string MailAddress { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string FilePath { get; set; }
    }
}
