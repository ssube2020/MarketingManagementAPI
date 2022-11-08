using Core.Dtos;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarketingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addproduct")]
        public ActionResult<ApplicationResult<int?>> AddProduct(ProductAddDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return new ApplicationResult<int?>
                {
                    StatusCode = (int)System.Net.HttpStatusCode.OK,
                    Message = errors.ToString(),
                    Data = null
                };
            }

            var response = _productService.Register(model);
            return response;
        }

    }
}
