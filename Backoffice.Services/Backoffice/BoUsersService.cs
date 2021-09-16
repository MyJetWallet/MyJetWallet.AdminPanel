using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;

namespace Backoffice.Services.Backoffice
{
    public class BoUsersService : IBoUsersService
    {
        public BoUsersService()
        {
        }

        public async ValueTask<IBackOfficeUser> GetBoUserById(string boUserId)
        {
            if (string.IsNullOrEmpty(boUserId))
                return null;
            
            //todo: прикрутить тут авторизацию юзеров по БД
            return new BackOfficeUser()
            {
                Id = boUserId,
                IsAdmin = true,
                IsBlocked = false,
                PersonalName = "Alexey",
                Registered = DateTime.Parse("2021-01-01")
            };
        }

        public async ValueTask<IBackOfficeUser> UpdateBoUser(IBackOfficeUser boUser)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<IEnumerable<IBackOfficeUser>> GetBoUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}