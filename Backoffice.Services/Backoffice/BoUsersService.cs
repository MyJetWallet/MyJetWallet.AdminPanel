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