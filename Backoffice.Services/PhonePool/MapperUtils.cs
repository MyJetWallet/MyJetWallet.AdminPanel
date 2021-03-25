using System;
using System.Linq;
using Backoffice.Abstractions.Models;
using MyCrm.Calls.Grpc.Contracts;
using MyCrm.Calls.Grpc.Models;

namespace Backoffice.Services.PhonePool
{
    public static class MapperUtils
    {
        public static PhonePoolModel ToDomain(this PhonePoolGrpcModel src)
        {
            if (src == null)
                return null;
            
            return new PhonePoolModel
            {
                Id = src.Id,
                Name = src.Name,
                Numbers = src.Numbers.Select(itm => itm.ToDomain()).ToList()
            };
        }

        public static IPhoneNumberModel ToDomain(this PhoneNumberGrpcModel src)
        {
            return new PhoneNumberModel
            {
                Id = src.Id,
                Name = src.Name,
                Number = src.Number
            };
        }

        public static CallGrpcRequest ToGrpcCallRequest(this ICallModel src, string boUserId)
        {
            return new CallGrpcRequest
            {
                TraderId = src.TraderId,
                ManagerNumberId = src.ManagerNumberId,
                CustomerNumber = src.CustomerNumber,
                BackOfficeUserId = boUserId,
                ManagerPhoneNumber = src.ManagerPhoneNumber
            };
        }

        public static BoCallResponseEnum ToDomain(this CallResponseEnum sourceEnum)
        {
            return sourceEnum switch
            {
                CallResponseEnum.Fail => BoCallResponseEnum.Fail,
                CallResponseEnum.Success => BoCallResponseEnum.Success,
                CallResponseEnum.NotValid => BoCallResponseEnum.NotValid,
                CallResponseEnum.PermissionDenied => BoCallResponseEnum.PermissionDenied,
                _ => throw new ArgumentOutOfRangeException(nameof(sourceEnum), sourceEnum, null)
            };
        }
    }
}