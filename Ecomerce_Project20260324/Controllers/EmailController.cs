using Ecomerce_Project20260324.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ecomerce_Project20260324.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration     _configuration;
       /// <summary>
       /// private readonly ISendGridClient   _sendGridClient;
       /// </summary>

        private readonly IBrevoEmailService _emailService;

        //public EmailController(IConfiguration configuration,ISendGridClient sendGridClient)
        //{
        //    _configuration = configuration;
        //   _sendGridClient = sendGridClient;
        //}

        public EmailController(IBrevoEmailService emailService)
        {
            //_configuration = configuration;
            _emailService = emailService;
        }
        ////////-------Commentend For Send Grid----------------------------

        //[HttpGet]
        //public async Task<IActionResult> SendEmail()
        //{
        //    string fromemail = _configuration.GetSection("SendGridEmailSettings").GetValue<string>("FromEmail");

        //    string fromname = _configuration.GetSection("SendGridEmailSettings").GetValue<string>("FromName");

        //    var msg= new SendGridMessage()
        //    {
        //        From = new EmailAddress(fromemail, fromname),
        //        Subject = "Test Email from SendGrid",
        //        PlainTextContent = "This is a test email sent using SendGrid in ASP.NET Core.",
        //        HtmlContent = "<strong>This is a test email sent using SendGrid in ASP.NET Core.</strong>"
        //    };
        //    msg.AddTo("santoshdas0412@gmail.com");

        //    var response = await _sendGridClient.SendEmailAsync(msg);
        //    string message = response.IsSuccessStatusCode ? "Email sent successfully!" : "Failed to send email.";

        //    bool message1 = response.IsSuccessStatusCode;

        //    if(message1)
        //    {
        //        return Ok(message);
        //    }
        //    return BadRequest(message);
        //}


        /////////////////-------------------End Here-------------------------

        [HttpGet]
        public async Task<IActionResult> SendTestEmail()
        {

            await _emailService.SendEmailAsync(
                "santoshdas0412@gmail.com",   // change this
                "Test Email",
                "<h1>Hello from Brevo 🚀</h1>"
            );

            return Ok("Email sent successfully");
        }

    }
}
