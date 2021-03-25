using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.PropertyMocks;
using MyCrm.Kyc.Grpc.Models;

namespace Backoffice.Services.KYC
{
    public static class MapperUtils
    {
        public static ITraderToCheckKyc FromGrpcToDomain(this MyCrmTraderToCheckKycModel src)
        {
            return new TraderToCheckKyc
            {
                Id = src.Id,
                Email = new EmailField(src.Email),
                Registered = src.Registered,
                CountryIso3 = src.CountryCode,
                FullName = (src.FirstName+" "+src.LastName).Trim(),
                MissingDocuments = src.MissDocuments
            };
        }
        
        public static CrmKycStatus ToGrpcKycStatus(this BoKycState src)
        {
            return src switch
            {
                BoKycState.Approved => CrmKycStatus.Approved,
                BoKycState.RestrictedCountry => CrmKycStatus.RestrictedCountry,
                BoKycState.WaitingForApprove => CrmKycStatus.Pending,
                _ => CrmKycStatus.Registered
            };
        }
        
        public static BoKycState GrpcToDomainKycStatus(this CrmKycStatus src)
        {
            return src switch
            {
                CrmKycStatus.Approved => BoKycState.Approved,
                CrmKycStatus.RestrictedCountry => BoKycState.RestrictedCountry,
                CrmKycStatus.Pending => BoKycState.WaitingForApprove,
                _ => BoKycState.NeedToFillData
            };
        }
    }
}