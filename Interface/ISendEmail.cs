using System.Threading.Tasks;

namespace NetCoreRestApiSendEmailWithTemplate.Interface
{
    public interface ISendEmail
    {
         Task sendEmail(string to,string toAdressTitle, string subject,string title,string fromAdressTitle);
    }
}