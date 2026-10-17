using AdminPanel.Models;
using AdminPanel.Models.Product;
using AdminPanel.Models.Product.BaseProduct;
using Ecomerce_Project20260324.DTOs.BaseProductDtos.Request;
using iText.Layout.Renderer;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using Request_BaseProductDiscount = AdminPanel.Models.Product.BaseProduct.Request_BaseProductDiscount;

namespace AdminPanel.Controllers
{
    public class ProductController : Controller
    {

        public readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        #region ProductMaterial

        #region GetAllProducMaterial

        [HttpGet]
        public async Task<IActionResult> ProductMaterial(int? Product_Material_Id)
        {
            MasterModel Model = new MasterModel();

            if (Product_Material_Id.HasValue)
            {
                var Response = await _httpClient.GetFromJsonAsync<Response_Material>($"http://localhost:5225/api/ProductMaterial/GetMaterialById/{Product_Material_Id.Value}");

                if (Response != null && Response.IsSuccess == true)
                {
                    var ResponseMaterial = Response.Materials.FirstOrDefault();

                    Model.Material_Obj = new MaterialVM
                    {
                        Id = ResponseMaterial.Id,
                        Name = ResponseMaterial.Name
                    };
                }
            }
            else
            {
                var Response = await _httpClient.GetFromJsonAsync<Response_Material>("http://localhost:5225/api/ProductMaterial/GetAllMaterials");

                if (Response != null && Response.IsSuccess == true)
                {
                    Model.Mateial_List = Response.Materials.Select(m => new MaterialVM
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }
            }
            return View(Model);
        }

        #endregion

        #region  AddProductMaterial
        [HttpPost]
        public async Task<IActionResult> AddProductMaterial(MasterModel Model)
        {
            var Response = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/ProductMaterial/AddMaterial", Model.Material_Obj);
            if (!Response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not saved successfully.";
                return RedirectToAction("ProductMaterial");
            }
            TempData["Success"] = "Status saved successfully.";
            return RedirectToAction("ProductMaterial");


        }

        #endregion

        #region UpdateProductMaterial

        [HttpPost]
        public async Task<IActionResult> UpdateProductMaterial(MasterModel Model)
        {
            var Response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/ProductMaterial/UpdateMaterial/{Model.Material_Obj.Id}", Model.Material_Obj);

            if (!Response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("ProductMaterial");
            }

            TempData["Success"] = "Updated successfully.";
            return RedirectToAction("ProductMaterial");
        }

        #endregion

        #region DeleteProductMaterial
        [HttpGet]
        public async Task<IActionResult> DeleteProductMaterial(int Product_Material_Id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5225/api/ProductMaterial/DeleteMaterial/{Product_Material_Id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Deleted successfully.";
                return RedirectToAction("ProductMaterial");
            }

            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("ProductMaterial");
        }

        #endregion


        #endregion

        #region ProductColor

        #region GetAllProductColor

        [HttpGet]
        public async Task<IActionResult> ProductColor(int? Product_Color_Id)
        {
            MasterModel Model = new MasterModel();

            if (Product_Color_Id.HasValue)
            {
                var resposne = await _httpClient.GetFromJsonAsync<Response_ProductColor>($"http://localhost:5225/api/ProductColor/GetProductColorById/{Product_Color_Id.Value}");

                var responseFirst = resposne.ProductColors.FirstOrDefault();
                if (resposne.IsSuccess == true)
                {
                    Model.ProductColor_Obj = new ProductColorVM
                    {
                        Id = responseFirst.Id,
                        Name = responseFirst.Name
                    };
                }

            }
            else
            {
                var response = await _httpClient.GetFromJsonAsync<Response_ProductColor>("http://localhost:5225/api/ProductColor/GetAllProductColors");
                Model.ProductColor_List = response.ProductColors.Select(m => new ProductColorVM
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList();

            }
            return View(Model);
        }

        #endregion

        #region AddProductColor

        [HttpPost]
        public async Task<IActionResult> AddProductColor(MasterModel Model)
        {
            var Resposne = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/ProductColor/AddProductColor", Model.ProductColor_Obj);

            if (!Resposne.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not saved successfully.";
                return RedirectToAction("ProductColor");
            }
            TempData["Success"] = "Not saved successfully.";
            return RedirectToAction("ProductColor");

        }

        #endregion

        #region UpdateProductColor

        [HttpPost]
        public async Task<IActionResult> UpdateProductColor(MasterModel Model)
        {
            var Response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/ProductColor/UpdateProductColor/{Model.ProductColor_Obj.Id}", Model.ProductColor_Obj);

            if (!Response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("ProductColor");
            }
            TempData["Success"] = "Updated successfully.";

            return RedirectToAction("ProductColor");
        }

        #endregion

        #region DeleteProductColor

        [HttpGet]
        public async Task<IActionResult> DeleteProductColor(int Product_Color_Id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5225/api/ProductColor/RemoveProductColor/{Product_Color_Id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Deleted successfully.";
                return RedirectToAction("ProductColor");
            }
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("ProductColor");
        }

        #endregion


        #endregion

        #region ProductSize


        #region GetAllProductSize

        [HttpGet]
        public async Task<IActionResult> ProductSize(int? Product_Size_Id)
        {
            MasterModel Model = new MasterModel();
            if (Product_Size_Id.HasValue)
            {
                var Response = await _httpClient.GetFromJsonAsync<Response_ProductSize>($"http://localhost:5225/api/ProductSize/GetProductSizeById/{Product_Size_Id.Value}");
                if (Response.IsSuccess == true)
                {
                    var ResponseSize = Response.ProductSizes.FirstOrDefault();
                    Model.ProductSize_Obj = new ProductSizeVM
                    {
                        Id = ResponseSize.Id,
                        Name = ResponseSize.Name
                    };
                }
            }
            else
            {
                var Response = await _httpClient.GetFromJsonAsync<Response_ProductSize>("http://localhost:5225/api/ProductSize/GetAllProductSizes");
                if (Response.IsSuccess == true)
                {
                    Model.ProductSize_List = Response.ProductSizes.Select(m => new ProductSizeVM
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }
            }
            return View(Model);
        }

        #endregion

        #region AddProductSize

        [HttpPost]
        public async Task<IActionResult> AddProductSize(MasterModel Model)
        {
            var resposne = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/ProductSize/AddProductSize", Model.ProductSize_Obj);

            if (!resposne.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Saved successfully!";
                return RedirectToAction("ProductSize");
            }

            TempData["Success"] = "Saved successfully!";
            return RedirectToAction("ProductSize");
        }

        #endregion


        #region UpdateProductSize

        [HttpPost]
        public async Task<IActionResult> UpdateProductSize(MasterModel Model)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/ProductSize/UpdateProductSize/{Model.ProductSize_Obj.Id}", Model.ProductSize_Obj);


            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully!";
                return RedirectToAction("ProductSize");
            }

            TempData["Success"] = "Updated successfully!";
            return RedirectToAction("ProductSize");
        }

        #endregion


        #region DeleteProductSize

        [HttpGet]
        public async Task<IActionResult> DeleteProductSize(int Product_Size_Id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5225/api/ProductSize/DeleteProductSize/{Product_Size_Id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Deleted successfully!";
                return RedirectToAction("ProductSize");
            }
            TempData["Success"] = "Deleted successfully!";
            return RedirectToAction("ProductSize");
        }

        #endregion

        #endregion

        #region ProductCategory

        #region GetAllProductCategory

        [HttpGet]
        public async Task<IActionResult> ProductCategory(int? Product_Category_Id)
        {
            MasterModel Model = new MasterModel();

            if (Product_Category_Id.HasValue)
            {
                var response = await _httpClient.GetFromJsonAsync<Response_MainCategory>($"http://localhost:5225/api/MainCategory/GetMainCategory/{Product_Category_Id.Value}");
                var responseFist = response.MainCategories.FirstOrDefault();
                if (response.IsSuccess == true)
                {
                    Model.ProductCategory_Obj = new MainCategoryVM
                    {
                        Id = responseFist.Id,
                        Name = responseFist.Name
                    };
                }
            }
            else
            {
                var response = await _httpClient.GetFromJsonAsync<Response_MainCategory>("http://localhost:5225/api/MainCategory/GetAllMainCategories");

                if (response.IsSuccess == true)
                {
                    Model.ProductCategoryVM_List = response.MainCategories.Select(m => new MainCategoryVM
                    {
                        Id = m.Id,
                        Name = m.Name,
                    }).ToList();
                }

            }
            return View(Model);
        }

        #endregion

        #region AddProductCategory

        [HttpPost]
        public async Task<IActionResult> AddProductCategory(MasterModel Model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/MainCategory/AddMainCategory", Model.ProductCategory_Obj);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Saved successfully!";
                return RedirectToAction("ProductCategory");
            }
            TempData["Success"] = "Saved successfully!";
            return RedirectToAction("ProductCategory");

        }

        #endregion

        #region UpdateProductCategory

        [HttpPost]
        public async Task<IActionResult> UpdateProductCategory(MasterModel Model)
        {
            var resposne = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/MainCategory/UpdateMainCategory/{Model.ProductCategory_Obj.Id}", Model.ProductCategory_Obj);
            if (!resposne.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully!";
                return RedirectToAction("ProductCategory");
            }
            TempData["Success"] = "Updated successfully!";
            return RedirectToAction("ProductCategory");
        }

        #endregion

        #region DeleteProductCategory

        [HttpGet]
        public async Task<IActionResult> DeleteProductCategory(int Product_Category_Id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5225/api/MainCategory/DeleteMainCategory/{Product_Category_Id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Deleted successfully!";
                return RedirectToAction("ProductCategory");
            }
            TempData["Success"] = "Deleted successfully!";
            return RedirectToAction("ProductCategory");
        }

        #endregion

        #endregion

        #region BaseProduct

        #region GetAllProduct

        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int? Base_Product_Id)
        {
            MasterModel Model = new MasterModel();



            if (Base_Product_Id.HasValue)
            {
                // Fetch the response using GetFromJsonAsync
                //var responsea = await _httpClient.GetFromJsonAsync<BaseProductVM_Dtos>($"http://localhost:5225/api/BaseProduct/GetByIdFullInfo/{Base_Product_Id}");
                var response = await _httpClient.GetAsync($"http://localhost:5225/api/BaseProduct/GetByIdFullInfo/{Base_Product_Id}");
                var responseJson = await response.Content.ReadAsStringAsync();
                try
                {
                    var result = JsonSerializer.Deserialize<Response_BaseProduct>(responseJson,
                                               new JsonSerializerOptions
                                               {
                                                   PropertyNameCaseInsensitive = true
                                               });
                    if (result != null)
                    {
                        //Model.BaseProductVM_Dtos_Obj = result.baseProductVM_Dtos;

                        var dto = result.baseProductVM_Dtos;

                        Model.BaseProductVM_Obj = new BaseProductVM
                        {
                            Id = dto.Id,
                            Name = dto.Name,
                            Price = dto.Price,
                            Discount = Convert.ToInt32(dto.Discount),
                            TotalPrice = dto.TotalPrice,
                            MaterialId = dto.Material.id,
                            MainCategoryId = dto.MainCategory.id,
                            Description = dto.Description
                        };
                    }


                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it, display an error message, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                try
                {
                    var reponse = await _httpClient.GetAsync("http://localhost:5225/api/BaseProduct/GetAllWithFullInfoAsync");
                    if (reponse != null)
                    {
                        var responseJson = await reponse.Content.ReadAsStringAsync();
                        var result =
                             JsonSerializer.Deserialize<List<BaseProductVM_Dtos>>(
                                 responseJson,
                                 new JsonSerializerOptions
                                 {
                                     PropertyNameCaseInsensitive = true
                                 });
                        if (result != null)
                        {
                            Model.BaseProductVM_Dtos_List = result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            try
            {
                //Category, Material
                var response = await _httpClient.GetFromJsonAsync<Response_MainCategory>("http://localhost:5225/api/MainCategory/GetAllMainCategories");

                if (response.IsSuccess == true)
                {
                    Model.ProductCategoryVM_List = response.MainCategories.Select(m => new MainCategoryVM
                    {
                        Id = m.Id,
                        Name = m.Name,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                var Response = await _httpClient.GetFromJsonAsync<Response_Material>("http://localhost:5225/api/ProductMaterial/GetAllMaterials");

                if (Response != null && Response.IsSuccess == true)
                {
                    Model.Mateial_List = Response.Materials.Select(m => new MaterialVM
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(Model);
        }
        #endregion

        #region AddBaseProduct

        [HttpPost]
        public async Task<IActionResult> AddBaseProduct(MasterModel Model)
        {

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5225/api/BaseProduct/AddBaseProduct", Model.BaseProductVM_Obj);
            if (!response.IsSuccessStatusCode)
            {

                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("GetAllProduct", "Product");
            }
            return RedirectToAction("GetAllProduct", "Product");
        }

        #endregion

        #region UpdateBaseProduct

        [HttpPost]
        public async Task<IActionResult> UpdateBaseProduct(MasterModel Model)
        {

            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/BaseProduct/UpdateBaseProductPrice/{Model.BaseProductVM_Obj.Id}", Model.BaseProductVM_Obj);

            if (!response.IsSuccessStatusCode)
            {

                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("GetAllProduct");
            }

            return RedirectToAction("GetAllProduct");
        }

        #endregion

        #region DeleteBaseProduct

        [HttpGet]
        public async Task<IActionResult> DeleteBaseProduct(int Id)
        {
            var reponse = await _httpClient.DeleteAsync($"http://localhost:5225/api/BaseProduct/RemoveBaseProduct/{Id}");
            if (!reponse.IsSuccessStatusCode)
            {
                return RedirectToAction("GetAllProduct");
            }
            return RedirectToAction("GetAllProduct");
        }

        #endregion


        #region PriceUpdate

        [HttpGet]
        public async Task<IActionResult> PriceUpdate(int Base_Product_Id)
        {
            MasterModel Model = new MasterModel();

            var response = await _httpClient.GetAsync($"http://localhost:5225/api/BaseProduct/GetByIdFullInfo/{Base_Product_Id}");
            var responseJson = await response.Content.ReadAsStringAsync();
            try
            {
                var result = JsonSerializer.Deserialize<Response_BaseProduct>(responseJson,
                                           new JsonSerializerOptions
                                           {
                                               PropertyNameCaseInsensitive = true
                                           });
                if (result != null)
                {
                    //Model.BaseProductVM_Dtos_Obj = result.baseProductVM_Dtos;
                    var dto = result.baseProductVM_Dtos;
                    Model.BaseProductVM_Obj = new BaseProductVM
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Price = dto.Price,
                        Discount = Convert.ToInt32(dto.Discount),
                        TotalPrice = dto.TotalPrice,
                        MaterialId = dto.Material.id,
                        MainCategoryId = dto.MainCategory.id,
                        Description = dto.Description
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message, etc.)
                Console.WriteLine(ex.Message);
            }


            return PartialView("_PriceUpdate", Model);

        }

        #endregion

        #region UpdatePrice

        [HttpPost]
        public async Task<IActionResult> UpdatePrice(MasterModel Model)
        {
            var request = new Models.Product.BaseProduct.Request_BaseProductPrice
            {
                Price = Model.BaseProductVM_Obj.Price
            };
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/BaseProduct/UpdatePrice/{Model.BaseProductVM_Obj.Id}", request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("GetAllProduct");
            }

            TempData["Success"] = "Not Updated successfully.";
            return RedirectToAction("GetAllProduct");
        }

        #endregion


        #region UpdateDiscount

        [HttpGet]
        public async Task<IActionResult> DiscountPrice(int Base_Product_Id)
        {
            MasterModel Model = new MasterModel();

            var response = await _httpClient.GetAsync($"http://localhost:5225/api/BaseProduct/GetByIdFullInfo/{Base_Product_Id}");
            var responseJson = await response.Content.ReadAsStringAsync();
            try
            {
                var result = JsonSerializer.Deserialize<Response_BaseProduct>(responseJson,
                                           new JsonSerializerOptions
                                           {
                                               PropertyNameCaseInsensitive = true
                                           });
                if (result != null)
                {
                    //Model.BaseProductVM_Dtos_Obj = result.baseProductVM_Dtos;
                    var dto = result.baseProductVM_Dtos;
                    Model.BaseProductVM_Obj = new BaseProductVM
                    {
                        Id = dto.Id,
                        Name = dto.Name,
                        Price = dto.Price,
                        Discount = Convert.ToInt32(dto.Discount),
                        TotalPrice = dto.TotalPrice,
                        MaterialId = dto.Material.id,
                        MainCategoryId = dto.MainCategory.id,
                        Description = dto.Description
                    };
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, display an error message, etc.)
                Console.WriteLine(ex.Message);
            }

            return PartialView("_DiscountPrice", Model);
        }


        #endregion

        #region UpdateDiscount

        [HttpPost]
        public async Task<IActionResult> UpdateDiscont(MasterModel Model)
        {
            var request = new Request_BaseProductDiscount
            {
                Discount = Model.BaseProductVM_Obj.Discount
            };
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5225/api/BaseProduct/UpdateBaseProductDiscount/{Model.BaseProductVM_Obj.Id}", request);
            if (!response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Not Updated successfully.";
                return RedirectToAction("GetAllProduct");
            }
            TempData["Success"] = "Not Updated successfully.";
            return RedirectToAction("GetAllProduct");
        }

        #endregion

        #region SearchBaseProduct

        [HttpGet]
        public async Task<IActionResult> SugestionBaseProduct(string Product_Name)
        {
            MasterModel Model = new MasterModel();


            var response = await _httpClient.GetAsync($"http://localhost:5225/api/BaseProduct/GetProductSearchSuggestions/{Product_Name}");

            var result = await response.Content.ReadFromJsonAsync<List<string>>();

            return Json(result);
        }

        #endregion


        [HttpGet]
        public async Task<IActionResult> SearchBaseProduct(string Product_Type)
        {
            MasterModel Model = new MasterModel();

            List<BaseProductVM_Dtos>? products = null;
            if (Product_Type != null)
            {
                var reponse = await _httpClient.GetAsync($"http://localhost:5225/api/BaseProduct/GetProductSearch/{Product_Type}");

                 products = await reponse.Content.ReadFromJsonAsync<List<BaseProductVM_Dtos>>();

            }
            else
            {
                var reponse = await _httpClient.GetAsync("http://localhost:5225/api/BaseProduct/GetAllWithFullInfoAsync");
                if (reponse != null)
                {
                    var responseJson = await reponse.Content.ReadAsStringAsync();
                    products =
                         JsonSerializer.Deserialize<List<BaseProductVM_Dtos>>(
                             responseJson,
                             new JsonSerializerOptions
                             {
                                 PropertyNameCaseInsensitive = true
                             });
                    if (products != null)
                    {
                        Model.BaseProductVM_Dtos_List = products;
                    }
                }
            }

            return Json(products);

        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(int Base_Product_Id, IFormFile ImagePath)
        {
            if (ImagePath == null || ImagePath.Length == 0)
            {
                TempData["Error"] = "No image selected.";
                return RedirectToAction("GetAllProduct");
            }

            using (var content = new MultipartFormDataContent())
            {
                // Add the file content
                var fileStreamContent = new StreamContent(ImagePath.OpenReadStream());
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ImagePath.ContentType);
                content.Add(fileStreamContent, "ImagePath", ImagePath.FileName);

                // Send the request
                var response = await _httpClient.PostAsync($"http://localhost:5225/api/BaseProduct/UploadImage/{Base_Product_Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Image uploaded successfully.";
                    return RedirectToAction("GetAllProduct");
                }
                else
                {
                    TempData["Error"] = "Failed to upload image.";
                    return RedirectToAction("GetAllProduct");
                }
            }
        }


        #endregion


    }
}
