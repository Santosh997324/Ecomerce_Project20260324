using Ecomerce_Project20260324.DTOs.BannerDto.Request;
using Ecomerce_Project20260324.DTOs.BannerDto.Response;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Request;
using Ecomerce_Project20260324.DTOs.CustomerFeedback.Response;
using Ecomerce_Project20260324.DTOs.StatusDto.Request;
using Ecomerce_Project20260324.DTOs.StatusDto.Response;
using Ecomerce_Project20260324.Models.Master;
using Ecomerce_Project20260324.Models.Product;
using Ecomerce_Project20260324.Repository.Banner;
using Ecomerce_Project20260324.Repository.CustomerFeedback;
using Ecomerce_Project20260324.Repository.Status;
using iText.Kernel.Pdf.Colorspace.Shading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sib_api_v3_sdk.Model;
using System.Formats.Asn1;

namespace Ecomerce_Project20260324.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterController : ControllerBase
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICustomerFeedbackRepository _customerFeedbackRepository;
        public MasterController(IBannerRepository bannerRepository, IStatusRepository statusRepository, ICustomerFeedbackRepository customerFeedbackRepository)
        {
            _bannerRepository = bannerRepository;
            _statusRepository = statusRepository;
            _customerFeedbackRepository = customerFeedbackRepository;

        }

        [HttpGet]
        [Route("GetAllBanner")]
        public async Task<ActionResult<Response_Banner>> GetAllBanner()
        {
            var result = await _bannerRepository.GetAllBannerAsync();

            if (!result.isSuccess)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetBannerById/{Banner_Id}")]
        public async Task<ActionResult<Response_Banner>> GetBannerById([FromRoute] int Banner_Id)
        {
            var Banner = await _bannerRepository.GetBannerByIdAsync(Banner_Id);

            if (Banner == null)
            {
                return BadRequest(Banner);
            }
            return Ok(Banner);
        }

        [HttpPost]
        [Route("AddBanner")]
        public async Task<IActionResult> AddBanner([FromForm] Request_Banner banner)
        {

            string fileName = "";
            if (banner.ImageFile != null)
            {
                fileName = Guid.NewGuid().ToString()
                           + Path.GetExtension(banner.ImageFile.FileName);

                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BannerImages");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await banner.ImageFile.CopyToAsync(stream);
                }

                banner.ImagePath = "/BannerImages/" + fileName;
            }


            var Banner = await _bannerRepository.AddBanner(banner);
            if (!Banner.isSuccess)
            {
                return BadRequest(Banner);
            }
            return Ok(Banner);
        }

        [HttpPut]
        [Route("UpdateBanner/{Banner_Id}")]
        public async Task<ActionResult<Response_Banner>> UpdateBanner([FromRoute] int Banner_Id, [FromForm] Request_Banner banner)
        {

            string fileName = "";
            if (banner.ImageFile != null)
            {
                fileName = Guid.NewGuid().ToString()
                           + Path.GetExtension(banner.ImageFile.FileName);

                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BannerImages");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await banner.ImageFile.CopyToAsync(stream);
                }
                banner.ImagePath = "/BannerImages/" + fileName;
            }
            var BannerExisting = await _bannerRepository.UpdateBanner(Banner_Id, banner);
           
            if (BannerExisting.isSuccess == false)
            {
                return BadRequest(BannerExisting);
            }
            return Ok(BannerExisting);

        }

        [HttpDelete]
        [Route("DeleteBanner/{Banner_Id}")]
        public async Task<ActionResult<Response_Banner>> DeleteBanner([FromRoute] int Banner_Id)
        {
            var Banner_Response = await _bannerRepository.DeleteBanner(Banner_Id);

            if (Banner_Response.isSuccess == false)
            {
                return BadRequest(Banner_Response);
            }
            return Ok(Banner_Response);

        }


        [HttpGet]
        [Route("GetAllStatus")]
        public async Task<ActionResult<Response_Status>> GetAllStatus()
        {
            var StatuData = await _statusRepository.GetAllStatus();
            if (StatuData.isSuccess == false)
            {
                return BadRequest(StatuData);
            }

            return Ok(StatuData);
        }

        [HttpGet]
        [Route("GetStatusById/{Status_Id}")]
        public async Task<ActionResult<Response_Banner>> GetStatusById([FromRoute] int Status_Id)
        {
            var StatusData = await _statusRepository.GetStatusById(Status_Id);
            if (StatusData.isSuccess == false)
            {
                return NotFound(StatusData);
            }

            return Ok(StatusData);
        }

        [HttpPost]
        [Route("AddStatus")]
        public async Task<IActionResult> AddStatus([FromBody] Request_Status request_Status)
        {
            var StatusData = await _statusRepository.AddStatus(request_Status);

            if (StatusData.isSuccess == false)
            {
                return BadRequest(StatusData);
            }

            return Ok(StatusData);
        }

        [HttpPut]
        [Route("UpdateStatus/{Status_Id}")]
        public async Task<ActionResult<Response_Banner>> UpdateStatus([FromRoute] int Status_Id, [FromBody] Request_Status request_Status)
        {
            var StatusData = await _statusRepository.UpdateStatus(Status_Id, request_Status);

            if (StatusData.isSuccess == false)
            {
                return Ok(StatusData);
            }

            return Ok(StatusData);
        }

        [HttpDelete]
        [Route("DeleteStatus/{Status_Id}")]
        public async Task<ActionResult<Response_Status>> DeleteStatus([FromRoute] int Status_Id)
        {
            var ResponseStatus = await _statusRepository.DeleteStatus(Status_Id);
            if (ResponseStatus.isSuccess == false)
            {
                return BadRequest();
            }

            return Ok(ResponseStatus);
        }


        [HttpGet]
        [Route("GetAllCustomerFeedback")]
        public async Task<ActionResult<Response_CustomerFeedback>> GetAllCustomerFeedback()
        {
            var CustomerFeedback = await _customerFeedbackRepository.GetAllCustomerFeedback();

            if (!CustomerFeedback.isSuccess)
            {
                return NotFound(CustomerFeedback);
            }
            return Ok(CustomerFeedback);
        }


        [HttpGet]
        [Route("GetCustomerFeedbackById/{Customer_Feedback_Id}")]
        public async Task<ActionResult<Response_CustomerFeedback>> GetCustomerFeedbackById([FromRoute] int Customer_Feedback_Id)
        {
            var CustomerFeedback = await _customerFeedbackRepository.GetCustomerFeedbackById(Customer_Feedback_Id);

            if (!CustomerFeedback.isSuccess)
            {
                return BadRequest(CustomerFeedback);
            }
            return Ok(CustomerFeedback);
        }


        [HttpDelete]
        [Route("DeleteCustomerFeedback/{Customer_Feedback_Id}")]
        public async Task<ActionResult<Response_CustomerFeedback>> DeleteCustomerFeedback([FromRoute] int Customer_Feedback_Id)
        {
            var CustomerFeedback = await _customerFeedbackRepository.DeleteCustomerFeedback(Customer_Feedback_Id);

            if (!CustomerFeedback.isSuccess)
            {
                return BadRequest(CustomerFeedback);
            }
            return Ok(CustomerFeedback);
        }

        [HttpPost]
        [Route("AddCustomerFeedback")]
        public async Task<IActionResult> AddCustomerFeedback([FromBody] Request_CustomerFeedback request_CustomerFeedback)
        {
            var CustomerFeedback = await _customerFeedbackRepository.AddCustomerFeedback(request_CustomerFeedback);

            if (!CustomerFeedback.isSuccess)
            {
                return BadRequest(CustomerFeedback);
            }
            return Ok(CustomerFeedback);
        }

        [HttpPut]
        [Route("UpdateCustomerFeedback/{Customer_Feedback_Id}")]
        public async Task<ActionResult<Response_CustomerFeedback>> UpdateCustomreFeedback([FromRoute] int Customer_Id, [FromBody] Request_CustomerFeedback request_CustomerFeedback)
        {
            var CustomerFeedback = await _customerFeedbackRepository.UpdateCustomerFeedback(Customer_Id, request_CustomerFeedback);

            if (!CustomerFeedback.isSuccess)
            {
                return BadRequest(CustomerFeedback);
            }

            return Ok(CustomerFeedback);
        }


    }
}
