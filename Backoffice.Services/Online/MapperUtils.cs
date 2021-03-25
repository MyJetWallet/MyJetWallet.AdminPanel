using System;
using Backoffice.Abstractions.Models;
using MyCrm.TraderOnlineData.Grpc.Responses.Models;

namespace Backoffice.Services.Online
{
    public static class MapperUtils
    {
        public static ITraderOnline FromGrpcToDomain(this TraderOnlineDataGrpcModel grpcModel)
        {
            return new TraderOnlineModel
            {
                TraderId = grpcModel.TraderId,
                LastTime = grpcModel.LastSeen ?? DateTime.UtcNow,
                Place = grpcModel.Place,
                UserAgent = grpcModel.UserAgent,
                Email = grpcModel.Email,
                FullName = grpcModel.FullName.Trim(),
                Country = grpcModel.Country,
                RetentionManager = grpcModel.RetentionManager,
                AssignedUserId = grpcModel.ConversionManager,
                Owner = grpcModel.Office
            };
        }
    }
}