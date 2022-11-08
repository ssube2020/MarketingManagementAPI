using Core.Dtos;
using Core.Interfaces.Services;
using Core.ViewModels;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Sales
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDistributorRepository _distributorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SalesService(IUnitOfWork unitOfWork, ISalesRepository salesRepository,
            IProductRepository productRepository, IDistributorRepository distributorRepository)
        {
            _salesRepository = salesRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _distributorRepository = distributorRepository;
        }

        public ApplicationResult<int?> AddSale(SaleAddDto addDto)
        {
            var response = new ApplicationResult<int?>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };

            try
            {
                Infrastructure.Entities.Sale toAddSale = new();
                var distributor = _distributorRepository.Get(addDto.DistributorId);
                if (distributor == null)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "could not find distributor with this id";
                    response.Data = addDto.DistributorId;
                    return response;
                }
                else
                {
                    toAddSale.DistributorId = addDto.DistributorId;
                }
                var product = _productRepository.Query().Where(x => x.ProductCode == addDto.ProductCode.ToString()).FirstOrDefault();
                if (product == null)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "such product code does not exist";
                    response.Data = null;
                    return response;
                }
                else if (product.UnitPrice == addDto.UnitPrice)
                {
                    toAddSale.SaleDate = addDto.SaleDate;
                    toAddSale.ProductCode = addDto.ProductCode;
                    toAddSale.Quantity = addDto.Quantity;
                    toAddSale.UnitPrice = addDto.UnitPrice;
                    toAddSale.TotalPrice = addDto.Quantity * addDto.UnitPrice;
                    _salesRepository.Add(toAddSale);
                    _unitOfWork.SaveChanges();

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "sale added succesfully";
                    response.Data = null;
                    return response;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "such unit price for this productcode does not exist ";
                    response.Data = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                return response;
            }

        }

        public ApplicationResult<SalesViewDto> FilterSales(FilterSaleDto filterDto)
        {
            var response = new ApplicationResult<SalesViewDto>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };

            try
            {
                var salesList = GetSalesByFilterType(filterDto);
                if (salesList != null && salesList.Count > 0)
                {
                    List<SaleViewDto> sales = new();
                    foreach (var sale in salesList)
                    {
                        SaleViewDto s = new();
                        var distributor = _distributorRepository.Get(sale.DistributorId);
                        s.DistributorName = distributor.Name;
                        s.SaleDate = sale.SaleDate;
                        s.ProductCode = sale.ProductCode;
                        s.Quantity = sale.Quantity;
                        s.UnitPrice = sale.UnitPrice;
                        s.TotalPrice = sale.TotalPrice;
                        sales.Add(s);
                    }

                    SalesViewDto salesView = new();
                    salesView.Sales = sales;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "filtered sales";
                    response.Data = salesView;
                    return response;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "sales not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = ex.Message;
                return response;
            }
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.Message = "unknown server error";
            return response;
        }

        private List<Infrastructure.Entities.Sale> GetSalesByFilterType(FilterSaleDto filterdto)
        {
            if (filterdto.DistributorId == null && String.IsNullOrEmpty(filterdto.ProductCode) && filterdto.SaleDate == null)
            {
                return _salesRepository.GetAll();
            }
            else if (filterdto.DistributorId != null && filterdto.SaleDate != null && !String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.DistributorId == filterdto.DistributorId && k.SaleDate == filterdto.SaleDate && k.ProductCode == filterdto.ProductCode).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId != null && filterdto.SaleDate == null && String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.DistributorId == filterdto.DistributorId).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId != null && filterdto.SaleDate != null && String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.DistributorId == filterdto.DistributorId && k.SaleDate == filterdto.SaleDate).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId != null && filterdto.SaleDate == null && !String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.DistributorId == filterdto.DistributorId && k.ProductCode == filterdto.ProductCode).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId == null && filterdto.SaleDate != null && String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.SaleDate == filterdto.SaleDate).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId == null && filterdto.SaleDate != null && !String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.ProductCode == filterdto.ProductCode && k.SaleDate == filterdto.SaleDate).ToList();
                return salesList;
            }
            else if (filterdto.DistributorId == null && filterdto.SaleDate == null && !String.IsNullOrEmpty(filterdto.ProductCode))
            {
                var salesList = _salesRepository.Query().Where(k => k.ProductCode == filterdto.ProductCode).ToList();
                return salesList;
            }
            else
            {
                return null;
            }
        }

        public ApplicationResult<DistributorBonusesViewDto> GetDistributorBonuses(getBonusesDto filterdto)
        {
            var response = new ApplicationResult<DistributorBonusesViewDto>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "unknown error",
                Data = null
            };

            DistributorBonusesViewDto toReturn = new();
            try
            {
                List<DistributorBonusViewDto> distributorBonuses = new();
                var distributors = GetDistributorsByFilterType(filterdto);
                if (distributors != null)
                {
                    foreach (var distr in distributors)
                    {
                        double distrBonus = CountBonus(distr.Id, filterdto.StartDate, filterdto.EndDate);
                        DistributorBonusViewDto item = new();
                        item.Name = distr.Name;
                        item.Surname = distr.Surname;
                        item.Bonus = distrBonus;
                        distributorBonuses.Add(item);
                    }
                    toReturn.Bonuses = distributorBonuses;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "distributor bonuses";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "enter start date and end date to filter";
                    return response;
                }

                if (filterdto.MinBonus != null && filterdto.MaxBonus != null && filterdto.MaxBonus >= filterdto.MinBonus)
                {
                    toReturn.Bonuses = distributorBonuses.Where(x => x.Bonus <= filterdto.MaxBonus && x.Bonus >= filterdto.MinBonus).ToList();
                }
                else if (filterdto.MinBonus == null && filterdto.MaxBonus == null)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = "min bonus or(and) max bonus format is incorrect";
                    return response;
                }
            }
            catch (Exception ex)
            {
                toReturn.Message = ex.Message;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            response.Message = toReturn.Message;
            response.Data = toReturn;
            return response;
        }

        private List<Infrastructure.Entities.Distributor> GetDistributorsByFilterType(getBonusesDto filterdto)
        {
            if (filterdto.StartDate == null || filterdto.EndDate == null)
            {
                return null;
            }
            else if (!String.IsNullOrEmpty(filterdto.Name) && !String.IsNullOrEmpty(filterdto.SurName))
            {
                return _distributorRepository.Query().Where(x => x.Name == filterdto.Name && x.Surname == filterdto.SurName).ToList();
            }
            else if (!String.IsNullOrEmpty(filterdto.Name) && String.IsNullOrEmpty(filterdto.SurName))
            {
                return _distributorRepository.Query().Where(x => x.Name == filterdto.Name).ToList();
            }
            else if (String.IsNullOrEmpty(filterdto.Name) && !String.IsNullOrEmpty(filterdto.SurName))
            {
                return _distributorRepository.Query().Where(x => x.Surname == filterdto.SurName).ToList();
            }
            else if (String.IsNullOrEmpty(filterdto.Name) && String.IsNullOrEmpty(filterdto.SurName))
            {
                return _distributorRepository.GetAll();
            }
            else
            {
                return null;
            }
        }

        private Double CountBonus(int distrId, DateTime startDate, DateTime endDate)
        {
            double bonus = 0;
            {
                var salesByDistributor = _salesRepository.Query().Where(x => x.DistributorId == distrId && DateTime.Compare(startDate, x.SaleDate) < 0 && DateTime.Compare(endDate, x.SaleDate) > 0).ToList();
                foreach (var item in salesByDistributor)
                {
                    bonus += item.TotalPrice * 0.1;
                }

                var underRecomendation1 = _distributorRepository.Query().Where(x => x.RecomendatorId == distrId).ToList();
                if (underRecomendation1 != null && underRecomendation1.Count != 0)
                {
                    foreach (var item in underRecomendation1)
                    {
                        var salesByUnderRecomendation1 = _salesRepository.Query().Where(x => x.DistributorId == item.Id && DateTime.Compare(startDate, x.SaleDate) < 0 && DateTime.Compare(endDate, x.SaleDate) > 0).ToList();
                        foreach (var itm in salesByUnderRecomendation1)
                        {
                            bonus += itm.TotalPrice * 0.05;
                        }

                        var underRecomendation2 = _distributorRepository.Query().Where(x => x.RecomendatorId == item.Id).ToList();
                        if (underRecomendation2 != null && underRecomendation2.Count != 0)
                        {
                            foreach (var itm in underRecomendation2)
                            {
                                var salesByUnderRecomendation2 = _salesRepository.Query().Where(x => x.DistributorId == itm.Id && DateTime.Compare(startDate, x.SaleDate) < 0 && DateTime.Compare(endDate, x.SaleDate) > 0).ToList();
                                foreach (var it in salesByUnderRecomendation2)
                                {
                                    bonus += it.TotalPrice * 0.01;
                                }
                            }

                        }
                    }
                }
                return bonus;
            }
        }
    }
}
