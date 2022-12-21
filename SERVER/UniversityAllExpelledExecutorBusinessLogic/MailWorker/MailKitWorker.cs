using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UniversityAllExpelledDataBaseImplement.Implements;
using UniversityAllExpelledExecutorBusinessLogic.BusinessLogic;
using UniversityAllExpelledExecutorBusinessLogic.OfficePackage.Implements;
using UniversityAllExpelledExecutorContracts.BindingModels;

namespace UniversityAllExpelledExecutorBusinessLogic.MailWorker
{
    public class MailKitWorker : AbstractMailWorker
    {
        private ReportLogic _logic = new ReportLogic(new TestingStorage(), new StudentStorage(), new StatementStorage(),
            new DisciplineStorage(), new SaveToExcel(), new SaveToWord(), new SaveToPdf());
        private string Path = "D:\\Ulstu\\CourseWorkMobile\\SERVER\\Files\\ReportPdf.pdf";

        public MailKitWorker()
        {

        }
        protected override async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort);
            try
            {
                objMailMessage.From = new MailAddress(_mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;

                //создать пдф отчёт
                _logic.SaveStudentStatementTestingsToPdfFile(new ReportBindingModel()
                {
                    FileName = Path,
                    Parametrs = info.Parametrs
                }); 

                Attachment data = new Attachment(Path, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(Path);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(Path);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(Path);
                objMailMessage.Attachments.Add(data);

                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin, _mailPassword);
                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }      
    }
}
