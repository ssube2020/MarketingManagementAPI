using Core.Dtos;
using Core.Interfaces.Services;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MarketingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost("addsale")]
        public ActionResult<ApplicationResult<int?>> AddSale(SaleAddDto model)
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

            var response = _salesService.AddSale(model);
            return response;
        }

        [HttpGet("filter")]
        public ActionResult<ApplicationResult<SalesViewDto>> FilterSales([FromQuery] FilterSaleDto filterdto)
        {
            var response = _salesService.FilterSales(filterdto);
            return response;
        }

        [HttpGet("getbonuses")]
        public ActionResult<ApplicationResult<DistributorBonusesViewDto>> GetDistributorBonuses([FromQuery] getBonusesDto filterdto)
        {
            var response = _salesService.GetDistributorBonuses(filterdto);
            return response;
        }
    }
}
