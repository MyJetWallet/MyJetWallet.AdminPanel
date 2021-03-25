using System;
using System.Linq;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.PropertyMocks;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts.Model;
using MyCrm.PersonalData.Grpc.Models;
using MyCrm.TradersDocuments.Grpc.Models;

namespace Backoffice.Services.PersonalData
{
    public static class ModelsUtils
    {
        public static IPersonalDataModel ToDomain(this MyCrmPersonalDataGrpcModel src)
        {
            return new PersonalDataModel
            {
                Id = src.Id,
                Registered = src.Registered,
                Email = src.Email,
                Address = src.Address,
                Phone = src.Phone,
                City = src.City,
                ZipCode = src.ZipCode,
                FirstName = src.FirstName,
                LastName = src.LastName,
                CountryOfRegistration = src.CountryOfRegistration,
                BirthDay = src.BirthDay,
                EmailIsConfirmed = src.EmailIsConfirmed,
                IsInternal = src.IsInternal,
                OwnerId = src.OwnerId,
                AssignedUserId = src.AssignedUserId,
                CrmStatus = src.CrmStatus,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                ExternalInfo = src.ExternalInfo?.Select(itm => itm.ToDomain()),
                TradingStatus = src.TradingStatus,
                RetentionManagerId = src.RetentionManagerId,
                NextCallDate = src.NextCallDate,
                ActivationDate = src.ActivationDate,
                MissDocuments = src.MissDocuments
            };
        }

        public static IPersonalDataExternalData ToDomain(this MyCrmPersonalDataExternalInfo src)
        {
            return new PersonalDataExternalData
            {
                Key = src.Key,
                Value = src.Value
            };
        }
        
        
        public static ITraderUtm ToDomain(this TraderUtmParametersGrpcModel src)
        {
            return new TraderUtm
            {
                Id = src.Id,
                Key = src.Key,
                Value = src.Value,
                TraderId = src.TraderId
            };
        }
        
        public static TraderUtmParametersGrpcModel ToGrpc(this ITraderUtm src)
        {
            return new TraderUtmParametersGrpcModel
            {
                Id = src.Id,
                Key = src.Key,
                Value = src.Value,
                TraderId = src.TraderId
            };
        }
        
        public static BoAdditionalPhoneModel ToDomain(this AdditionalPhoneGrpcModel src)
        {
            return new BoAdditionalPhoneModel
            {
                Id = src.Id,
                Phone = src.Phone
            };
        }   
        
        public static ITraderDocumentModel ToDomain(this CrmTraderDocumentGrpcModel src)
        {
            return new TraderDocumentModel
            {
                Id = src.Id,
                TraderId = src.TraderId,
                DocumentType = src.DocumentType.ToDomain(),
                DateTime = src.DateTime,
                Mime = src.Mime,
                VisibleForClient = src.VisibleForClient,
                Uploader = src.Uploader
            };
        }
        
        public static TraderDocumentType ToDomain(this CrmTraderDocumentTypeGrpc src)
        {
            return src switch
            {
                CrmTraderDocumentTypeGrpc.Id => TraderDocumentType.Id,
                CrmTraderDocumentTypeGrpc.ProofOfAddress => TraderDocumentType.ProofOfAddress,
                CrmTraderDocumentTypeGrpc.FrontCard => TraderDocumentType.FrontCard,
                CrmTraderDocumentTypeGrpc.BackCard => TraderDocumentType.BackCard,
                CrmTraderDocumentTypeGrpc.DepositLetter => TraderDocumentType.DepositLetter,
                CrmTraderDocumentTypeGrpc.Other => TraderDocumentType.Other,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
        
        public static CrmTraderDocumentTypeGrpc ToGrpc(this TraderDocumentType src)
        {
            return src switch
            {
                TraderDocumentType.Id => CrmTraderDocumentTypeGrpc.Id,
                TraderDocumentType.ProofOfAddress => CrmTraderDocumentTypeGrpc.ProofOfAddress,
                TraderDocumentType.FrontCard => CrmTraderDocumentTypeGrpc.FrontCard,
                TraderDocumentType.BackCard => CrmTraderDocumentTypeGrpc.BackCard,
                TraderDocumentType.DepositLetter => CrmTraderDocumentTypeGrpc.DepositLetter,
                TraderDocumentType.Other => CrmTraderDocumentTypeGrpc.Other,
                _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
            };
        }
    }
}