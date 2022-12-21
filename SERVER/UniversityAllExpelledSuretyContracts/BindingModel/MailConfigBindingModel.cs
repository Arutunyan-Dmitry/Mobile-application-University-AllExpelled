using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAllExpelledSuretyContracts.BindingModel
{
    public class MailConfigBindingModel
    {
        public string MailLogin { get; set; }
        public string MailPassword { get; set; }
        public string SmtpClientHost { get; set; }
        public int SmtpClientPort { get; set; }
        public string PopHost { get; set; }
        public int PopPort { get; set; }
    }
}
