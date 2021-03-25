using System;
using Backoffice.Abstractions.Models;
using MyCrm.AutoOwnerProfiles.Grpc.Models;

namespace Backoffice.Services.AutoOwnerProfiles
{
    public static class MapperUtils
    {
        public static IAutoOwnerProfileModel ToDomain(this AutoOwnerProfileGrpcModel src)
        {
            return new AutoOwnerProfileModel
            {
                Id = src.Id,
                OfficeId = src.OfficeId,
                BrandId = src.BrandId,
                IsAffiliateTraffic = src.IsAffiliateTraffic,
                SupportedCountries = src.SupportedCountries ?? Array.Empty<string>()
            };
        }

        public static AutoOwnerProfileGrpcModel ToGrpc(this IAutoOwnerProfileModel src)
        {
            return new AutoOwnerProfileGrpcModel
            {
                Id = src.Id,
                OfficeId = src.OfficeId,
                BrandId = src.BrandId,
                IsAffiliateTraffic = src.IsAffiliateTraffic,
                SupportedCountries = src.SupportedCountries
            };
        }
    }
}