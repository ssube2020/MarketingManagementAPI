using Core.Dtos;
using Core.Helpers;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MarketingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributosController : ControllerBase
    {

        private readonly IDistributorService _distributorService;

        public DistributosController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        [HttpPost("registerdistributor")]
        public ActionResult<ApplicationResult<DistributorViewDto>> Register(RegistrationDto model)
        {
            if (model.Image != null)
            {
                if (!Directory.Exists("UploadedImages"))
                {
                    Directory.CreateDirectory("UploadedImages");
                }

                string directoryPath = Path.Combine("UploadedImages");
                var arr = model.Image?.FileName.Split(".");
                var ext = arr[arr.Length - 1];
                var picHash = $"{Hasher.GetHash()}.{ext}";
                string filepath = Path.Combine(directoryPath, picHash);

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    model.Image?.CopyTo(stream);
                }
                model.Imageurl = picHash;
            }
            else
            {
                model.Imageurl = null;
            }
            return new ApplicationResult<DistributorViewDto>
            {
                StatusCode = (int)System.Net.HttpStatusCode.OK,
                Message = "model is not valid",
                Data = null
            };
            var response = _distributorService.Register(model);
            return response;
        }

        [HttpGet("getdistributors")]
        public ActionResult<ApplicationResult<DistributosViewDto>> GetAllDistributors()
        {
            var response  = _distributorService.GetDistributors();
            return response;
        }

        [HttpDelete("deletedistributor")]
        public ActionResult<ApplicationResult<DistributorViewDto>> DeleteDistributor(int distributorId)
        {
            var response = _distributorService.DeleteDistributor(distributorId);
            return response;
        }

        [HttpPut("editdistributor")]
        public ActionResult<ApplicationResult<int?>> EditDistributor(DistributorEditDto editDto)
        {
            var response = _distributorService.EditDistributor(editDto);
            return response;
        }

       
    }
}
