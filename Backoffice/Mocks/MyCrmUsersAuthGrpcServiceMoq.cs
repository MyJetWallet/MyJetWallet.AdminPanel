using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Auth.GrpcContracts;
using MyCrm.Auth.GrpcContracts.Contracts;
using MyCrm.Auth.GrpcContracts.Models;

namespace Backoffice.Mocks
{
    public class MyCrmUsersAuthGrpcServiceMoq: IMyCrmUsersAuthGrpcService
    {
        public async IAsyncEnumerable<BackofficeUserGrpcModel> GetBackOfficeUsersAsync()
        {
            var list = new List<BackofficeUserGrpcModel>()
            {
                new BackofficeUserGrpcModel()
                {
                    Id = "alexey.n",
                    AllDataAccess = true,
                    AllOfficeDataAccess = true,
                    AssignedPhonePoolId = "",
                    CanDoRealFund = true,
                    CanDownloadData = true,
                    CanEditAffiliates = true,
                    CanEditReferralLinks = true,
                    CanLoginEdit = true,
                    CanSeeDataWithNoOffice = true,
                    CanUseBalanceCorrection = true,
                    CertAliases = new []{ "alex", "alexey" },
                    ChangeIsInternal = true,
                    Countries = new []{ "ALL" },
                    DataOwnership = "",
                    DataOwnerships = new []{ "ALL" },
                    ExposePersonalData = true,
                    InternalPhoneNumberId = "1234567",
                    IsAdmin = true,
                    IsBlocked = false,
                    Ip = "",
                    LastLogInAttempt = null,
                    PersonalName = "Alexey",
                    ReferralLink = "",
                    Registered = DateTime.Parse("2021-01-01"),
                    Roles = new []{ "ALL" }
                }
            };

            foreach (var model in list)
            {
                yield return model;
            }
        }

        public ValueTask<BackofficeUserGrpcModel> BlockUnblockBackOfficeUserAsync(BlockUserGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask EditBackOfficeUserAsync(BackofficeUserGrpcModel backOfficeUser)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<BackofficeUserGrpcModel> AddBackOfficeUserAsync(AddBackofficeUserGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AddBackOfficeUserIfNotExistsAsync(AddBackofficeUserIfNotExistsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<BackofficeUserGrpcModel> GetAsync(GetBackofficeUsersRequests request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthUserResponse> AuthenticateUserAsync(AuthenticateGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask ChangePasswordAsync(ChangePasswordGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async ValueTask<FindIdResponse> FindIdAsync(FindIdRequest request)
        {
            throw new NotImplementedException();
        }
    }
}