using AdminPanel.Models.Security;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Request;
using Ecomerce_Project20260324.DTOs.ApiDTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdminPanel.Controllers
{
    public class SecurityController : Controller
    {
        private readonly HttpClient _httpClient;

        public SecurityController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        #region Register

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new Request_ApiUserRegisterDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/Auth/Register", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<Response_ApiUserRegisterDto>(result);

                if (responseDto != null && responseDto.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login", "Security");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, string.Join(", ", responseDto?.Message ?? new List<string> { "Registration failed." }));
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }

            return View(model);
        }

        #endregion

        #region Login

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new
            {
                Email = model.Email,
                Password = model.Password
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/Auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<Models.Security.Response_LoginDto>(result);

                if (responseDto != null && responseDto.Result == true)
                {
                    // Save JWT token in session
                    HttpContext.Session.SetString("JwtToken", responseDto.Token);
                    HttpContext.Session.SetString("RefreshToken", responseDto.RefreshToken);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            // If you want to remove only specific session values:--------------------------------------------------------------
            //HttpContext.Session.Remove("UserId");
            //HttpContext.Session.Remove("UserName");
            //HttpContext.Session.Remove("Token");

            // Clear all session data-------------------------------------------------------------------------------------------
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Security");
        }

        #endregion

        #region User

        public async Task<IActionResult> User()
        {
            return View();
        }

        #endregion


    }
}
