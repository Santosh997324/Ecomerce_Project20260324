using AutoMapper;
using Azure.Core;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Request;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Response;
using Ecomerce_Project20260324.Models.AuthModels;
using Ecomerce_Project20260324.Repository.AuthRepository;
using Ecomerce_Project20260324.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using SendGrid.Helpers.Mail;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace Ecomerce_Project20260324.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly ISendGridClient _sendGridClient;
        public AuthController(IAuthRepository authRepository, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _configuration = configuration;
            // _sendGridClient = sendGridClient;
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Response_ApiUserRegisterDto>> Register([FromBody] Request_ApiUserRegisterDto request_ApiUserRegisterDto)
        {
            var userDto = await _authRepository.Register(request_ApiUserRegisterDto);

            //"email": "santoshdas1675@gmail.com",
            //"password": "Santosh@123",
            //"firstName": "Santosh",
            //"lastName": "Dev"

            //santoshdas0412@gmail.com
            //Rajeev@123

            if (userDto.IsSuccess == false)
            {
                return BadRequest(new Response_ApiUserRegisterDto()
                {
                    IsSuccess = false,
                    Message = userDto.Message
                });
            }


            //User Confirmation
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userDto.apiUser);

            //var caltedUrl = Url.Action("ConfirmEmail", "EmailConfirmation", new { userId = userDto.apiUser.Id, token = user }, Request.Scheme);

            var callUrl = Request.Scheme + "://" + Request.Host + Url.Action("ConfirmEmail", "Auth", new { userId = userDto.apiUser.Id, code = code });


            var body = EmailTemplates.EmailLinkTemplate(callUrl);

            var result = await SendEmail(body, request_ApiUserRegisterDto.Email);

            string EmailStatus = result ? "Email sent successfully!" : "Failed to send email.";

            return Ok(new Response_ApiUserRegisterDto()
            {
                IsSuccess = true,
                apiUser = userDto.apiUser,
                Message = new List<string>() { EmailStatus }
            });
        }


        [HttpPost]
        [Route("registerAdmin/{secretKey}")]
        public async Task<ActionResult<Response_ApiUserRegisterDto>> RegisterAdmin([FromBody] Request_ApiUserRegisterDto request_ApiUserRegisterDto, int secretKey)
        {
            var userDto = await _authRepository.RegisterAdmin(request_ApiUserRegisterDto, secretKey);
            if (userDto.IsSuccess == false)
            {
                return BadRequest(new Response_ApiUserRegisterDto()
                {
                    IsSuccess = false,
                    Message = userDto.Message
                });
            }
            return Ok(new Response_ApiUserRegisterDto()
            {
                IsSuccess = true,
                Message = new List<string>() { "Admin Registered Successfully!" },
                // apiUser = userDto.apiUser
            });

        }


        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<ActionResult<Response_ApiUserConfirmEmail>> ConfirmEmail(string userId, string code)
        {
            if (userId == null)
            {
                return BadRequest(new Response_ApiUserConfirmEmail()
                {
                    IsSuccess = false,
                    Message = "User Id is null"
                });
            }


            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest(new Response_ApiUserConfirmEmail()
                {
                    IsSuccess = false,
                    Message = "User not found"
                });
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            var status = result.Succeeded ? "Email confirmed successfully!" : "Email confirmation failed.";


            return Ok(new Response_ApiUserConfirmEmail()
            {
                IsSuccess = true,
                Message = status
            });
        }

        private async Task<bool> SendEmail(string body, string email, string subject = "Email Verification")
        {
            try
            {
                var config = sib_api_v3_sdk.Client.Configuration.Default;
                config.ApiKey["api-key"] = _configuration["BrevoSettings:ApiKey"];

                var apiInstance = new TransactionalEmailsApi();

                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(
                        _configuration["BrevoSettings:SenderName"],
                        _configuration["BrevoSettings:SenderEmail"]
                    ),
                    To = new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo(email)
            },
                    Subject = subject,
                    HtmlContent = body
                };

                var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Response_LoginDto>> Login([FromBody] Request_LoginDto request_LoginDto)
        {
            var authResponse = await _authRepository.LoginDto(request_LoginDto);
            if (authResponse.Result == false)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);

        }

        [HttpPost]
        [Route("GenerateTokens")]
        public async Task<ActionResult<Response_LoginDto>> GenerateTokens([FromBody] Request_TokenDto request_TokenDto)
        {
            var authResponse = await _authRepository.VarifyAndGenerateToken(request_TokenDto);
            if (authResponse.Result == false)
            {
                return BadRequest(authResponse);
            }
            return Ok(authResponse);
        }


        [Authorize]
        [HttpDelete("Logout")]
        public async Task<ActionResult> Logout()
        {
            string userId = HttpContext.User.FindFirstValue("uid");
            await _authRepository.LogoutDeleteRefreshToken(userId);
            return Ok("Logout Succesfully!");
        }


        [HttpPost]
        [Route("ResendEmail/{email}")]
        public async Task<ActionResult<Response_ApiUserConfirmEmail>> ResendEmail(
            [FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new Response_ApiUserConfirmEmail()
                {
                    IsSuccess = false,
                    Message = "No user found with given email"
                });
            }

            if (user.EmailConfirmed == true)
            {
                return BadRequest(new Response_ApiUserConfirmEmail()
                {
                    IsSuccess = false,
                    Message = "Email already confirmed"
                });
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackURL = Request.Scheme + "://" + Request.Host
                + Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, code = code });

            var body = EmailTemplates.EmailLinkTemplate(callbackURL);

            //SENDING EMAIL

            var result = await SendEmail(body, "santoshdas1675@gmail.com");
            string EmailMessage = result ? "Email sent" : "Email failed to send";

            return Ok(new Response_ApiUserConfirmEmail()
            {
                IsSuccess = true,
                Message = EmailMessage
            });
        }

        [HttpGet]
        [Route("ResetPasswordSendEmail/{email}")]
        public async Task<IActionResult> ResetPasswordSendEmail(
            [FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("Wrong email adress");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("Email is not confirmed");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //encode token to not have special characters
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var callbakcUrl = Request.Scheme + "://" + Request.Host +
                $"/ResetPassword?email={email}&token={validToken}";

            var body = EmailTemplates.EmailLinkTemplate(callbakcUrl);
            var result = await SendEmail(body, user.Email, "Password reset");

            if (result == false)
            {
                return BadRequest("Password failed to reset");
            }

            return Ok("Password reset link sent to email");
        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult<Response_PasswordResetDto>> ResetPassword(
            [FromBody] Request_PasswordResetDto passwordResetDto)
        {
            var user = await _userManager.FindByEmailAsync(passwordResetDto.Email);

            if (user == null)
            {
                return BadRequest(new Response_PasswordResetDto()
                {
                    isSuccess = false,
                    Message = "No user found with given Id"
                });
            }

            if (passwordResetDto.newPassword != passwordResetDto.confirmPassword)
            {
                return BadRequest(new Response_PasswordResetDto()
                {
                    isSuccess = false,
                    Message = "Password do not match"
                });
            }

            //DECODE TOKEN
            var decodedToken = WebEncoders.Base64UrlDecode(passwordResetDto.Token);
            var normalizedToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalizedToken,
                passwordResetDto.newPassword);

            if (result.Succeeded)
            {
                return Ok(new Response_PasswordResetDto()
                {
                    isSuccess = true,
                    Message = "Password changed successfully"
                });
            }

            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(new Response_PasswordResetDto()
            {
                isSuccess = false,
                Message = "Failed to change password",
                Errors = errors
            });
        }

        [HttpPost]
        [Route("UpdateUserDetails")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] Request_ApiUserDetailsUpdate userUpdate)
        {
            string userId = HttpContext.User.FindFirstValue("uid");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("No user found with given Id");
            }

            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;
            await _userManager.UpdateAsync(user);

            return Ok("User Updated");
        }


        ///------------------Send SendGrid Method-------------------------//////////


        //private async Task<bool> SendEmail(string body, string email, string subject = "Email Verification")
        //{
        //    string fromEmail = _configuration.GetSection("SendGridEmailSettings")
        //        .GetValue<string>("FromEmail");
        //    string fromName = _configuration.GetSection("SendGridEmailSettings")
        //        .GetValue<string>("FromName");

        //    var msg = new SendGridMessage()
        //    {
        //        From = new EmailAddress(fromEmail, fromName),
        //        Subject = subject,
        //        PlainTextContent = body,
        //        HtmlContent = body
        //    };

        //    var emailToSend = email;

        //    msg.AddTo("santoshdas0412@gmail.com");

        //    var response = await _sendGridClient.SendEmailAsync(msg);

        //    return response.IsSuccessStatusCode;

        //}



        //--------------------End Here------------------///

    }
}
