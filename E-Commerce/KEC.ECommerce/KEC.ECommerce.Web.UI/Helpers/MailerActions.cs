using DinkToPdf;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Web.UI.Mailer;
using System.IO;

namespace KEC.ECommerce.Web.UI.Helpers
{
    public class MailerActions
    {
        public static void SendEmail(EmailMessage emailMessage, EmailService emailService, EmailConfiguration emailConfiguration)
        {

            emailService.Send(emailMessage, emailConfiguration);

        }
        public static void ConvertHtml2PDF(string documentContent, PDFParams pdfParams)
        {
            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            WkHtmlToPdf.Preload();
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = { ColorMode = ColorMode.Color,
                                   Orientation = Orientation.Portrait,
                                   PaperSize = PaperKind.A4,
                                   Margins = new MarginSettings() { Top = 10 },
                                   Out = pdfParams.PdfOutputFile},
                Objects = { new ObjectSettings() { HtmlContent = documentContent }, }
            };
            converter.Convert(doc);
        }
        public static void SendLicencesEmail(EmailService emailService,EmailConfiguration emailConfiguration, EmailMessage message)
        {
            emailService.Send(message, emailConfiguration);
        }

    }
}
