using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using DotNetCoreDecorators;
using MyCrm.Auth.GrpcContracts;

namespace Backoffice.Services.Backoffice
{
    public class BoUsersService : IBoUsersService
    {
        private readonly IMyCrmUsersAuthGrpcService _authService;

        public BoUsersService(IMyCrmUsersAuthGrpcService authService)
        {
            _authService = authService;
        }

        public async ValueTask<IBackOfficeUser> GetBoUserById(string boUserId)
        {
            if (string.IsNullOrEmpty(boUserId))
                //throw new Exception("Cannot found user with empty ID");
                return null;
            
            //todo: прикрутить тут авторизацию юзеров по БД
            return new BackOfficeUser()
            {
                Id = boUserId,
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
                CertAliases = new[] {"alex", "alexey"},
                ChangeIsInternal = true,
                Countries = new[] {"ALL"},
                DataOwnership = "",
                DataOwnerships = new[] {"ALL"},
                ExposePersonalData = true,
                InternalPhoneNumberId = "1234567",
                IsAdmin = true,
                IsBlocked = false,
                Ip = "",
                LastLogInAttempt = null,
                PersonalName = "Alexey",
                ReferralLink = "",
                Registered = DateTime.Parse("2021-01-01"),
                Roles = new[] {"ALL"}
            };
            
            
            return await _authService.GetBackOfficeUsersAsync()
                .SelectAsync(itm => itm.ToDomain())
                .FirstOrDefaultAsync(itm => itm.Id == boUserId);
        }

        public async ValueTask<IBackOfficeUser> UpdateBoUser(IBackOfficeUser boUser)
        {
            await _authService.EditBackOfficeUserAsync(boUser.ToGrpc());
            return await _authService.GetBackOfficeUsersAsync()
                .SelectAsync(itm => itm.ToDomain())
                .FirstOrDefaultAsync(itm => itm.Id == boUser.Id);
        }

        public async ValueTask<IEnumerable<IBackOfficeUser>> GetBoUsersAsync()
        {
            return await _authService.GetBackOfficeUsersAsync()
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IEnumerable<IBackOfficeUser>> GetBoUsersAsync(IEnumerable<string> offices,
            bool showBlocked = false)
        {
            return await _authService.GetBackOfficeUsersAsync()
                .SelectAsync(itm => itm.ToDomain())
                .WhereAsync(itm => showBlocked || itm.IsBlocked)
                .WhereAsync(itm => offices.Contains(itm.DataOwnership))
                .AsListAsync();
        }
    }
}