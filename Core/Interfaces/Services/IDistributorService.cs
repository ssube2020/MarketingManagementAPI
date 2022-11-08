using Core.Dtos;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IDistributorService
    {

        ApplicationResult<DistributorViewDto> Register(RegistrationDto model);
        ApplicationResult<DistributosViewDto> GetDistributors();
        ApplicationResult<DistributorViewDto> DeleteDistributor(int distrId);
        ApplicationResult<int?> EditDistributor(DistributorEditDto editDto);
    }
}
