
//using AdminPanel.Custom;
using AdminPanel.Models;
using AdminPanel.Models.Banner;
using AdminPanel.Models.Status;
using Ecomerce_Project20260324.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace AdminPanel.Controllers
{

    [AuthorizeFilter]
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return PartialView("_Menu");
        }

        public IActionResult Header()
        {
            return PartialView("_Header");
        }

        public IActionResult Footer()
        {
            return PartialView("_Footer");
        }


        #region Banner

        #region GetBanner

        [HttpGet]
        public async Task<IActionResult> Banner(int? Banner_Id)
        {
            MasterModel model = new MasterModel();
            var response = new HttpResponseMessage();
            if (Banner_Id.HasValue)
            {
                response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetBannerById/" + Banner_Id);
                var jsonResponse1 = await response.Content.ReadFromJsonAsync<Response_Banner>();

                // Check data in debugger
                var banner = jsonResponse1.Banners.FirstOrDefault();

                if (banner != null)
                {
                    model.BannerObj = new BannerVM
                    {
                        Id = banner.Id,
                        Heading = banner.Heading,
                        Description = banner.Description,
                        ImagePath = "http://localhost:5225" + banner.ImagePath
                    };
                }
            }
            else
            {
                response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetAllBanner");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();  //When we get reponse as string then we can use JsonConvert.DeserializeObject to deserialize it into our desired object.

                    // var jsonResponse1 = await response.Content.ReadFromJsonAsync<Response_Banner>(); //here json is automatically deserialized into Response_Banner object but it is not working for some reason so we are using JsonConvert.DeserializeObject instead of this method.

                    // Deserialize into Response_Banner
                    var responseBanner = JsonConvert.DeserializeObject<Response_Banner>(jsonResponse);

                    if (responseBanner != null && responseBanner.isSuccess)
                    {
                        // Map Banners to BannerVM
                        model.BannerList = responseBanner.Banners.Select(b => new BannerVM
                        {
                            Id = b.Id,
                            Heading = b.Heading,
                            Description = b.Description,
                            ImagePath = "http://localhost:5225" + b.ImagePath
                        }).ToList();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseBanner?.Message ?? "Failed to fetch banners.");
                        model.BannerList = new List<BannerVM>();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to fetch banners.");
                }

                //model.BannerObj = new BannerVM();
            }
            return View(model);

        }

        //[HttpGet]
        //public async Task<IActionResult> Banner(int? Banner_Id)
        //{
        //    MasterModel model = new MasterModel();

        //    var response = new HttpResponseMessage();

        //    if (Banner_Id.HasValue)
        //    {
        //         response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetBannerById/" + Banner_Id);
        //    }
        //    else
        //    {
        //         response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetAllBanner");
        //    }

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await response.Content.ReadAsStringAsync();  //When we get reponse as string then we can use JsonConvert.DeserializeObject to deserialize it into our desired object.

        //       // var jsonResponse1 = await response.Content.ReadFromJsonAsync<Response_Banner>(); //here json is automatically deserialized into Response_Banner object but it is not working for some reason so we are using JsonConvert.DeserializeObject instead of this method.

        //        // Deserialize into Response_Banner
        //        var responseBanner = JsonConvert.DeserializeObject<Response_Banner>(jsonResponse);

        //        if (responseBanner != null && responseBanner.isSuccess)
        //        {
        //            // Map Banners to BannerVM
        //            model.BannerList = responseBanner.Banners.Select(b => new BannerVM
        //            {
        //                Id = b.Id,
        //                Heading = b.Heading,
        //                Description = b.Description,
        //                ImagePath = "http://localhost:5225" + b.ImagePath
        //            }).ToList();
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, responseBanner?.Message ?? "Failed to fetch banners.");
        //            model.BannerList = new List<BannerVM>();
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Failed to fetch banners.");
        //    }

        //   //model.BannerObj = new BannerVM();

        //    return View(model);
        //}

        #endregion

        #region   AddBanner
        [HttpPost]
        public async Task<IActionResult> AddBanner(MasterModel model)
        {

            MultipartFormDataContent formData = new MultipartFormDataContent();

            formData.Add(
                       new StringContent(model.BannerObj.Heading ?? ""), "Heading");
            formData.Add(
                new StringContent(model.BannerObj.Description ?? ""), "Description");

            if (model.ImagePath != null)
            {
                var stream = model.ImagePath.OpenReadStream();
                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(
                        model.ImagePath.ContentType
                    );
                formData.Add(
                    fileContent,
                    "ImageFile",
                    model.ImagePath.FileName
                );
            }

            var response = await _httpClient.PostAsync(
                "http://localhost:5225/api/Master/AddBanner",
                formData
            );

            var result = await response.Content.ReadAsStringAsync();
            ViewBag.Error = result;
            return RedirectToAction("Banner", "Home");

        }

        #endregion

        #region UpdateBanner

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(MasterModel model)
        {
            MultipartFormDataContent formData = new MultipartFormDataContent();

            formData.Add(
                new StringContent(model.BannerObj.Heading ?? ""),
                "Heading");

            formData.Add(
                new StringContent(model.BannerObj.Description ?? ""),
                "Description");

            if (model.ImagePath != null)
            {
                var streamContent = new StreamContent(
                    model.ImagePath.OpenReadStream());

                streamContent.Headers.ContentType =
                    new System.Net.Http.Headers.MediaTypeHeaderValue(
                        model.ImagePath.ContentType);

                formData.Add(
                    streamContent,
                    "ImageFile",
                    model.ImagePath.FileName);
            }

            var response = await _httpClient.PutAsync(
                $"http://localhost:5225/api/Master/UpdateBanner/{model.BannerObj.Id}",
                formData);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Banner");
            }

            return RedirectToAction("Banner");
        }

        #endregion

        #region DeleteBanner
        [HttpGet]
        public async Task<IActionResult> DeleteBanner(int Banner_Id)
        {
            var response = await _httpClient.DeleteAsync(
                 $"http://localhost:5225/api/Master/DeleteBanner/{Banner_Id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Banner deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete banner.";
            }

            return RedirectToAction("Banner");
        }

        #endregion

        #endregion

        #region  Status

        #region GetStatus


        [HttpGet]
        public async Task<IActionResult> Status(int? Status_Id)
        {
            MasterModel model = new MasterModel();

            var response = new HttpResponseMessage();

            if (Status_Id.HasValue)
            {
                response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetStatusById/" + Status_Id);
                var resposnejson = await response.Content.ReadFromJsonAsync<Response_Status>();

                var ResponseStatus = resposnejson.status.FirstOrDefault();

                if (ResponseStatus != null)
                {
                    model.StatusObj = new StatusVM
                    {
                        Status_Id = ResponseStatus.Status_Id,
                        Status_Name = ResponseStatus.Status_Name
                    };
                }
                return View(model);
            }
            else
            {
                response = await _httpClient.GetAsync("http://localhost:5225/api/Master/GetAllStatus");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseStatus = JsonConvert.DeserializeObject<Response_Status>(jsonResponse);
                    if (responseStatus != null && responseStatus.isSuccess)
                    {
                        model.StatusList = responseStatus.status.Select(s => new StatusVM
                        {
                            Status_Id = s.Status_Id,
                            Status_Name = s.Status_Name
                        }).ToList();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseStatus?.Message ?? "Failed to fetch status.");
                        model.StatusList = new List<StatusVM>();
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region SaveStatus

        [HttpPost]
        public async Task<IActionResult> SaveStatus(MasterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                    "http://localhost:5225/api/Master/AddStatus",
                    model.StatusObj);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Status saved successfully.";
                return RedirectToAction("Status");
            }

            TempData["Error"] = "Failed to save status.";
            return RedirectToAction("Status");

        }

        #endregion

        #region UpdateStatus

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(MasterModel model)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"http://localhost:5225/api/Master/UpdateStatus/{model.StatusObj.Status_Id}",
                model.StatusObj);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Status updated successfully.";
                return RedirectToAction("Status");
            }

            TempData["Error"] = "Failed to update status.";

            return RedirectToAction("Status");
        }

        #endregion

        #region DeleteStatus

        [HttpGet]
        public async Task<IActionResult> DeleteStatus(int? Status_Id)
        {
            var response = await _httpClient.DeleteAsync(
                  $"http://localhost:5225/api/Master/DeleteStatus/{Status_Id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Status deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to delete status.";
            }

            return RedirectToAction("Status");
        }

        #endregion

        #endregion

        #region CustomerFeedback

        [HttpGet]
        public async Task<IActionResult> CustomerFeedback()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustomerFeedback(MasterModel model)
        {
            return View();
        }



        #endregion


        public IActionResult ProductColor()
        {
            return View();
        }
        public IActionResult ProductMaterial()
        {
            return View();
        }
        public IActionResult ProductSize()
        {
            return View();
        }
        public IActionResult ProductVarient()
        {
            return View();
        }
        public IActionResult ProductBase()
        {
            return View();
        }

        public IActionResult ProductCategory()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



    }
}
