using Core.Dtos;
using Core.Interfaces.Services;
using Core.ViewModels;
using Infrastructure.IRepositories;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Core.Helpers;
using Infrastructure.Enums;

namespace Core.Services.Distributor
{
    public class DistributorService : IDistributorService

    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly IUnitOfWork _unitOfWork;


        public DistributorService(IDistributorRepository distributorRepository, IUnitOfWork unitOfWork)
        {
            _distributorRepository = distributorRepository;
            _unitOfWork = unitOfWork;
        }

        public ApplicationResult<DistributorViewDto> Register(RegistrationDto model)
        {
            var response = new ApplicationResult<DistributorViewDto>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };

            try
            {
                var personToCheck = _distributorRepository.Query().FirstOrDefault(k => k.Id == model.RecomendatorId);


                if (personToCheck != null)
                {
                    if (personToCheck.NumOfRecomsGiven == (int)MaxValueTypes.MaxRecommendation)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "this person has reached recomendations limit";
                        return response;
                    }
                }

                Infrastructure.Entities.Distributor toregister = new();
                toregister.Name = model.Name;
                toregister.Surname = model.Surname;
                toregister.Birthday = model.Birthday;
                toregister.Gender = model.Gender;
                toregister.ImageUrl = null;
                toregister.PrivateDocType = model.PrivateDocType;
                toregister.DocSerialNumber = model.DocSerialNumber;
                toregister.DocNumber = model.DocNumber;
                toregister.PrivateNumber = model.PrivateNumber;
                toregister.GivingAuthority = model.GivingAuthority;
                toregister.ContactType = model.ContactType;
                toregister.ContactInfo = model.ContactInfo;
                toregister.AddressType = model.AddressType;
                toregister.Address = model.Address;
                if (model.RecomendatorId != null)
                {
                    int? checkRecomendator = CheckForLimitAndHierarchy((Int32)model.RecomendatorId);
                    if (checkRecomendator == null)
                    {
                        response.Message = "could not find recomendator with that id";
                        return response;
                    }
                    else if (checkRecomendator >= 0)
                    {
                        toregister.HierarchyLevel = (int)checkRecomendator + 1;
                        toregister.RecomendatorId = model.RecomendatorId;
                    }
                    else if (checkRecomendator == -1)
                    {
                        response.Message = "this distributor has not left more recomendations";
                        return response;
                    }
                    else if (checkRecomendator == -2)
                    {
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.Message = "distirbutor can not be registered because of hierarchy level restriction";
                        return response;
                    }
                    else
                    {
                        response.Message = "unknown error, try again !!!";
                        return response;
                    }
                }
                else
                {
                    toregister.RecomendatorId = null;
                }
                toregister.NumOfRecomsGiven = 0;

                //if (!Directory.Exists("UploadedImages"))
                //{
                //    Directory.CreateDirectory("UploadedImages");
                //}
                //if (model.Image != null)
                //{
                //    string directoryPath = Path.Combine("UploadedImages");
                //    var arr = model.Image?.FileName.Split(".");
                //    var ext = arr[arr.Length - 1];
                //    var picHash = $"{Hasher.GetHash()}.{ext}";
                //    string filepath = Path.Combine(directoryPath, picHash);

                //    using (var stream = new FileStream(filepath, FileMode.Create))
                //    {
                //        model.Image?.CopyTo(stream);
                //    }
                //    toregister.ImageUrl = picHash;
                //}

                _distributorRepository.Add(toregister);
                _unitOfWork.SaveChanges();

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = "distributor registered succesfully";
                return response;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                response.Message = errorMessage;
                return response;
            }
        }

        public ApplicationResult<DistributosViewDto> GetDistributors()
        {
            var response = new ApplicationResult<DistributosViewDto>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "",
                Data = null
            };

            try
            {
                var distributors = _distributorRepository.GetAll();
                if (distributors != null)
                {
                    List<DistributorViewDto> distributorsViewlist = new();
                    foreach (var distributor in distributors)
                    {
                        DistributorViewDto item = new();
                        item.Name = distributor.Name;
                        item.Surname = distributor.Surname;
                        item.Birthday = distributor.Birthday;
                        item.Gender = distributor.Gender;
                        item.PrivateDocType = distributor.PrivateDocType;
                        item.DocSerialNumber = distributor.DocSerialNumber;
                        item.DocNumber = distributor.DocNumber;
                        item.GiveOutDate = distributor.GiveOutDate;
                        item.DocExpDate = distributor.DocExpDate;
                        item.PrivateNumber = distributor.PrivateNumber;
                        item.GivingAuthority = distributor.GivingAuthority;
                        item.ContactType = distributor.ContactType;
                        item.ContactInfo = distributor.ContactInfo;
                        item.AddressType = distributor.AddressType;
                        item.Address = distributor.Address;

                        if (distributor.RecomendatorId != null)
                        {
                            var recomendator = _distributorRepository.Get((int)distributor.RecomendatorId);
                            item.Recomendator = recomendator.Name;
                        }
                        else
                        {
                            item.Recomendator = "this person doesnot have recomendator";
                        }

                        distributorsViewlist.Add(item);
                    }
                    DistributosViewDto data = new();
                    data.Distributors = distributorsViewlist;
                    response.Message = "distributors fetched succesfully";
                    response.Data = data;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                response.Message = errorMessage;
            }
            return response;
        }

        public ApplicationResult<DistributorViewDto> DeleteDistributor(int distrId)
        {
            var response = new ApplicationResult<DistributorViewDto>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };

            try
            {
                var personToDelete = _distributorRepository.Get(distrId);
                if (personToDelete != null)
                {
                    _distributorRepository.Remove(distrId);
                    _unitOfWork.SaveChanges();
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "distributor deleted succesfully";
                    return response;
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "distributor not found";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
        }

        public ApplicationResult<int?> EditDistributor(DistributorEditDto editDto)
        {
            var response = new ApplicationResult<int?>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "",
                Data = null
            };
            try
            {
                var personToEdit = _distributorRepository.Get(editDto.Id);
                if (personToEdit != null)
                {
                    personToEdit.Name = editDto.Name;
                    personToEdit.Surname = editDto.Surname;
                    personToEdit.Birthday = editDto.Birthday;
                    personToEdit.Gender = editDto.Gender;
                    personToEdit.PrivateDocType = editDto.PrivateDocType;
                    personToEdit.DocSerialNumber = editDto.DocSerialNumber;
                    personToEdit.DocNumber = editDto.DocNumber;
                    personToEdit.GiveOutDate = editDto.GiveOutDate;
                    personToEdit.DocExpDate = editDto.DocExpDate;
                    personToEdit.PrivateNumber = editDto.PrivateNumber;
                    personToEdit.GivingAuthority = editDto.GivingAuthority;
                    personToEdit.ContactType = editDto.ContactType;
                    personToEdit.ContactInfo = editDto.ContactInfo;
                    personToEdit.AddressType = editDto.AddressType;
                    personToEdit.Address = editDto.Address;
                    
                    _unitOfWork.SaveChanges();
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "person updated succesfully";
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "distributor not found";
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        private int? CheckForLimitAndHierarchy(int recomendatorId)
        {
            var recomendator = _distributorRepository.Query().SingleOrDefault(k => k.Id == recomendatorId);
            if (recomendator != null)
            {
                if (recomendator.NumOfRecomsGiven != (int)MaxValueTypes.MaxRecommendation)
                {
                    recomendator.NumOfRecomsGiven += 1;
                    if (recomendator.HierarchyLevel != (int)MaxValueTypes.MaxHierarchyLevel - 1)
                    {
                        return recomendator.HierarchyLevel;
                    }
                    else
                    {
                        return -2;
                    }
                }
                else
                {
                    return -1;
                }
            }
            return null;
        }

        private int CheckforHierarchyLevel(int recomendatorId)
        {
            var recomendator = _distributorRepository.Query().SingleOrDefault(k => k.Id == recomendatorId);
            if (recomendator != null)
            {
                if (recomendator.NumOfRecomsGiven != (int)MaxValueTypes.MaxRecommendation)
                {
                    recomendator.NumOfRecomsGiven += 1;
                    return 1;
                }
            }
            return 2;
        }

    }
}
