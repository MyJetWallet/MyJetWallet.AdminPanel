using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.PersonalData.Grpc;
using MyCrm.PersonalData.Grpc.Contracts;
using MyCrm.PersonalData.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmPersonalDataGrpcServiceMoq: IMyCrmPersonalDataGrpcService
    {
        public async ValueTask<MyCrmPersonalDataContract> GetAsync(GetPersonalDataGrpcRequest request)
        {
            return new MyCrmPersonalDataContract()
            {
                Trader = new MyCrmPersonalDataGrpcModel()
                {
                    Id = "1",
                    ActivationDate = DateTime.Now,
                    Address = "address",
                    AssignedUserId = "2",
                    BirthDay = DateTime.Now,
                    City = "city",
                    CountryOfCitizenship = "qse",
                    CountryOfRegistration = "asd",
                    CountryOfResidence = "asd",
                    CrmStatus = "ok",
                    Email = "asdad",
                    EmailIsConfirmed = true,
                    ExternalInfo = new MyCrmPersonalDataExternalInfo[] {},
                    FirstName = "alex",
                    IsInternal = false,
                    LastName = "asd",
                    MissDocuments = false,
                    NextCallDate = DateTime.Now,
                    OwnerId = "4",
                    Phone = "123123123",
                    Registered = DateTime.Now,
                    RetentionManagerId = "55",
                    SameEmailGroupTraders = new List<SameEmailGroupTraderGrpcModel>(),
                    SkypeId = "123123",
                    TradingStatus = "ok",
                    ZipCode = "111"
                }
            };
        }

        public ValueTask<MyCrmPersonalDataGrpcModel> GetWithoutPartnerInfoAsync(GetPersonalDataGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<MyCrmPersonalDataGrpcModel> GetTradersAttachedToPartnerAsync(GetPersonalDataGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<MyCrmPersonalDataGrpcModel> GetInternalTradersAsync(GetInternalTradersGrpcContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateIsInternalAsync(UpdateIsInternalGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async IAsyncEnumerable<MyCrmPersonalDataGrpcModel> GetByPhraseAsync(GetByPhraseGrpcRequest request)
        {
            yield return new MyCrmPersonalDataGrpcModel()
            {
                Id = "1",
                ActivationDate = DateTime.Now,
                Address = "address",
                AssignedUserId = "2",
                BirthDay = DateTime.Now,
                City = "city",
                CountryOfCitizenship = "qse",
                CountryOfRegistration = "asd",
                CountryOfResidence = "asd",
                CrmStatus = "ok",
                Email = "asdad",
                EmailIsConfirmed = true,
                ExternalInfo = new MyCrmPersonalDataExternalInfo[] { },
                FirstName = "alex",
                IsInternal = false,
                LastName = "asd",
                MissDocuments = false,
                NextCallDate = DateTime.Now,
                OwnerId = "4",
                Phone = "123123123",
                Registered = DateTime.Now,
                RetentionManagerId = "55",
                SameEmailGroupTraders = new List<SameEmailGroupTraderGrpcModel>(),
                SkypeId = "123123",
                TradingStatus = "ok",
                ZipCode = "111"
            };
        }

        public ValueTask UpdateAsync(UpdatePersonalDataGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<UpdateEmailResponseContract> UpdateEmailAsync(UpdateEmailGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AssignOwnerAsync(AssignOwnerGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AssignBackOfficeUserGrpcRequest(AssignBackOfficeUserGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AssignRetentionManagerAsync(AssignBackOfficeUserGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateTraderExternalDataAsync(UpdateTraderExternalDataGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateNextCallDateAsync(UpdateNextCallDateGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AddAdditionalPhoneAsync(CreateAdditionalPhoneGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AdditionalPhoneGrpcModel> GetAdditionalPhonesAsync(GetAdditionalPhonesGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask DeleteAdditionalPhonesAsync(DeleteAdditionalPhoneGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateAdditionalPhoneAsync(UpdateAdditionalPhoneGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<UtmGrpcModel> GetUtmAsync(GetUtmGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask ConfirmAsync(ConfirmGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<ChangeBrandGrpcResponse> ChangeBrandAsync(ChangeBrandGrpcRequest request)
        {
            throw new NotImplementedException();
        }
    }
}