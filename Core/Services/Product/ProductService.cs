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

namespace Core.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public ApplicationResult<int?> Register(ProductAddDto addDto)
        {
            var response = new ApplicationResult<int?>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };

            try
            {
                var productToCheck = _productRepository.Query().Where(x => x.ProductCode == addDto.ProductCode).FirstOrDefault();
                if(productToCheck != null)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "product with such product code is already registered";
                    response.Data = null;
                    return response;
                }
                Infrastructure.Entities.Product toregister = new();
                toregister.ProductCode = addDto.ProductCode;
                toregister.ProductName = addDto.ProductName;
                toregister.UnitPrice = addDto.UnitPrice;
                
                _productRepository.Add(toregister);
                _unitOfWork.SaveChanges();

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "product added succesfully";
                return response;
            } catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
