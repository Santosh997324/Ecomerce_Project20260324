namespace Ecomerce_Project20260324.Repository
{
    public interface IBrevoEmailService
    {
       
           Task SendEmailAsync(string toEmail, string subject, string htmlContent);
        
    }
}
