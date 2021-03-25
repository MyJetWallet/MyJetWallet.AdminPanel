using System;
using Backoffice.Abstractions.Models;
using MyCrm.PersonalData.Grpc.Contracts;
using MyCrm.Withdrawals.Grpc.Models;

namespace Backoffice.Services.Withdrawal
{
    public static class ModelsUtils
    {
        public static IWithdrawalModel ToDomain(this MyCrmWithdrawGrpcModel src)
        {
            return new WithdrawalModel
            {
                Id = src.Id,
                Registered = src.Registered,
                TraderId = src.TraderId,
                AccountId = src.AccountId,
                Owner = src.Owner,
                Currency = src.Currency,
                Amount = src.Amount,
                PaymentSystem = src.PaymentSystem,
                PaymentSystemData = src.PaymentSystemData,
                Status = src.Status.ToDomain(),
                FullName = src.FullName,
                CountryCode = src.CountryCode,
                Email = src.Email,
                Comment = src.Comment,
                AssignedBackOfficeUserId = src.AssignedBackOfficeUserId,
                BackOfficeUserId = src.BackOfficeUserId,
            };
        }

        public static CrmWithdrawalStatus ToDomain(this CrmWithdrawGrpcStatus src)
        {
            return src switch
            {
                CrmWithdrawGrpcStatus.Pending => CrmWithdrawalStatus.Pending,
                CrmWithdrawGrpcStatus.PendingWithReservation => CrmWithdrawalStatus.PendingWithReservation,
                CrmWithdrawGrpcStatus.Processed => CrmWithdrawalStatus.Processed,
                CrmWithdrawGrpcStatus.Canceled => CrmWithdrawalStatus.Canceled,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
        
        public static CrmWithdrawGrpcStatus ToGrpc(this CrmWithdrawalStatus src)
        {
            return src switch
            {
                CrmWithdrawalStatus.Pending => CrmWithdrawGrpcStatus.Pending,
                CrmWithdrawalStatus.PendingWithReservation => CrmWithdrawGrpcStatus.PendingWithReservation,
                CrmWithdrawalStatus.Processed => CrmWithdrawGrpcStatus.Processed,
                CrmWithdrawalStatus.Canceled => CrmWithdrawGrpcStatus.Canceled,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }

        public static UpdatePersonalDataGrpcRequest ToUpdateGrpc(this IPersonalDataModel src)
        {
            return new UpdatePersonalDataGrpcRequest
            {
                Address = src.Address,
                City = src.City,
                Id = src.Id,
                Phone = src.Phone,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                FirstName = src.FirstName,
                LastName = src.LastName,
                ZipCode = src.ZipCode,
                DateOfBirth = src.BirthDay ?? DateTime.UtcNow,
                MissDocuments = src.MissDocuments
            };
        }
    }
}