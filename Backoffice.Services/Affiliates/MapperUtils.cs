using Backoffice.Abstractions.Models;
using MyCrm.AffiliateAccess.Grpc.Models;

namespace Backoffice.Services.Affiliates
{
    public static class MapperUtils
    {
        public static IAffiliate ToDomain(this AccessGrpcModel src)
        {
            return new Affiliate
            {
                AffId = src.AffId,
                ApiKey = src.ApiKey,
                Ips = src.Ips,
                DepFrom = src.DepFrom,
                Description = src.Description
            };
        }
        
         public static AccessGrpcModel ToUpdateRequest(this IAffiliate src, string boUserId)
        {
            return new AccessGrpcModel
            {
                AffId = src.AffId,
                ApiKey = src.ApiKey,
                Ips = src.Ips,
                DepFrom = src.DepFrom,
                Description = src.Description,
                BoUserId = boUserId
            };
        }
    }
}