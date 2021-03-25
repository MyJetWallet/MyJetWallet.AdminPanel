using System;
using Backoffice.Abstractions.Models;
using MyCrm.Deposits.Grpc.Models;

namespace Backoffice.Services.Deposits
{
    public static class ModelsUtils
    {
        public static IDepositModel ToDomain(this DepositGrpcModel src)
        {
            return new DepositModel
            {
                Id = src.Id,
                PaymentSystemTransactionId = src.PaymentSystemTransactionId,
                Registered = src.Registered,
                LastUpdate = src.LastUpdate,
                PaymentSystem = src.PaymentSystem,
                PaymentSystemCurrency = src.PaymentSystemCurrency,
                PaymentSystemAmount = src.PaymentSystemAmount,
                Currency = src.Currency,
                Amount = src.Amount,
                Status = src.Status.ToDomain(),
                FullName = src.FullName,
                CountryCode = src.CountryCode,
                Email = src.Email,
                TraderId = src.TraderId,
                AccountId = src.AccountId,
                VoidTransactionId = src.VoidTransactionId,
                VoidDepositReason = src.VoidDepositReason.ToDomain(),
                Owner = src.Owner,
                IsVoided = src.IsVoided,
            };
        }
        
        
        public static IPaymentReportModel ToDomain(this MyCrm.PaymentReport.GrpcContracts.Models.PaymentGrpcModel src)
        {
            return new PaymentReportModel
            {
                TraderId = src.TraderId,
                AccountId = src.AccountId,
                AccountManagerWhenCreated = src.AccountManagerWhenCreated,
                AffiliateId = src.AffiliateId,
                Amount = src.Amount,
                ConversionStatus = src.ConversionStatus,
                Country = src.Country,
                Currency = src.Currency,
                Email = src.Email,
                FirstName = src.FirstName,
                IsInit = src.IsInit,
                TransactionDate = src.TransactionDate,
                KYC = src.KYC,
                LastName = src.LastName,
                Office = src.Office,
                PaymentProvider = src.PaymentProvider,
                PaymentSystem = src.PaymentSystem,
                PaymentTransactionId = src.PaymentTransactionId,
                PsTransactionId = src.PsTransactionId,
                RetentionManager = src.RetentionManager,
                Status = src.Status,
                TraderRegistrationDate = src.TraderRegistrationDate,
                TradingStatusWhenCreated = src.TradingStatusWhenCreated,
                TransactionType = src.TransactionType,
                IsInternal = src.IsInternal,
                PsAmount = src.PsAmount,
                PsCurrency = src.PsCurrency,
                VoidDepositReason = VoidDepositReason.None,
                VoidTransactionId = src.VoidTransactionId,
                IsVoided = src.IsVoided,
                LastUpdate = src.LastUpdate,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfRegistration = src.CountryOfRegistration,
                CountryOfResidence = src.CountryOfResidence
            };
        }
        
        public static IPaymentProviderSettingsModel ToDomain(this ProvidersStrategySettingsGrpcModel src)
        {
            return new PaymentProviderSettingsModel
            {
                PaymentProviderName = src.PaymentProviderName,
                Brand = src.Brand,
                Weight = src.Weight,
                RestrictedGeo = src.RestrictedGeo ?? Array.Empty<string>(),
                SupportedGeo = src.SupportedGeo ?? Array.Empty<string>()
            };
        }

        public static IPaymentSystemSettingsModel ToDomain(this PaymentSystemSettingsGrpcModel src)
        {
            return new PaymentSystemSettingsModel
            {
                PaymentSystemName = src.PaymentSystemName,
                Brand = src.Brand,
                IsEnable = src.IsEnabled,
                RestrictedGeo = src.RestrictedGeo ?? Array.Empty<string>(),
                SupportedGeo = src.SupportedGeo ?? Array.Empty<string>()
            };
        }


        public static CrmDepositStatus ToDomain(this DepositStatus src)
        {
            return src switch
            {
                DepositStatus.Registered => CrmDepositStatus.Registered,
                DepositStatus.Paid => CrmDepositStatus.Paid,
                DepositStatus.Failed => CrmDepositStatus.Failed,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
        
        public static DepositStatus ToDomain(this CrmDepositStatus src)
        {
            return src switch
            {
                CrmDepositStatus.Registered => DepositStatus.Registered,
                CrmDepositStatus.Paid => DepositStatus.Paid,
                CrmDepositStatus.Failed => DepositStatus.Failed,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
        
        public static CrmVoidDepositReason ToDomain(this VoidDepositReason src)
        {
            return src switch
            {
                VoidDepositReason.None => CrmVoidDepositReason.None,
                VoidDepositReason.ChargeBack => CrmVoidDepositReason.ChargeBack,
                VoidDepositReason.Void => CrmVoidDepositReason.Void,
                VoidDepositReason.Fail => CrmVoidDepositReason.Fail,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
    }
}