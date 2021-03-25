using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using Backoffice.Services.Withdrawal;
using DotNetCoreDecorators;
using Microsoft.AspNetCore.Http;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts.Contracts;
using MyCrm.MyCrmTradersUtmParametersGrpcContracts.Model;
using MyCrm.PersonalData.Grpc;
using MyCrm.PersonalData.Grpc.Contracts;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice;
using MyCrm.TraderMarketingSalesData.Grpc.Backoffice.Contracts.Requests;
using MyCrm.TradersDocuments.Grpc;
using MyCrm.TradersDocuments.Grpc.Contracts;

namespace Backoffice.Services.PersonalData
{
    public class PersonalDataService : IPersonalDataService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmPersonalDataGrpcService _dataSource;
        private readonly IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService _achievementReader;
        private readonly IMyCrmTradersDocumentsGrpcService _tradersDocumentsGrpc;
        private readonly IMyCrmTraderUrmParametersGrpcService _tradersUtmService;

        public PersonalDataService(IHttpContextAccessor ctx, IMyCrmPersonalDataGrpcService dataSource,
            IMyCrmReaderTraderMarketingSalesDataForBackofficeGrpcService reader,
            IMyCrmTradersDocumentsGrpcService tradersDocumentsGrpc, IMyCrmTraderUrmParametersGrpcService utmService)
        {
            _ctx = ctx;
            _dataSource = dataSource;
            _achievementReader = reader;
            _tradersDocumentsGrpc = tradersDocumentsGrpc;
            _tradersUtmService = utmService;
        }

        public async ValueTask<IPersonalDataModel> GetTraderPersonaDataAsync(string traderId)
        {
            var request = new GetPersonalDataGrpcRequest
                {TraderId = traderId, BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};

            var response = await _dataSource.GetAsync(request);
            return response.Trader.ToDomain();
        }

        public async ValueTask AddAdditionalPhoneAsync(string traderId, string phone)
        {
            var request = new CreateAdditionalPhoneGrpcRequest {TraderId = traderId, Phone = phone};
            await _dataSource.AddAdditionalPhoneAsync(request);
        }

        public async ValueTask<IEnumerable<IBoAdditionalPhoneModel>> GetAdditionalPhonesAsync(string traderId)
        {
            var request = new GetAdditionalPhonesGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId};

            return await _dataSource.GetAdditionalPhonesAsync(request).SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask DeleteAdditionalNumberAsync(string traderId, string numberId)
        {
            var request = new DeleteAdditionalPhoneGrpcRequest
                {TraderId = traderId, BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), Id = numberId};

            await _dataSource.DeleteAdditionalPhonesAsync(request);
        }

        public async ValueTask UpdateAdditionalNumberAsync(string traderId, string numberId, string phone)
        {
            var request = new UpdateAdditionalPhoneGrpcRequest {TraderId = traderId, Id = numberId, Phone = phone};
            await _dataSource.UpdateAdditionalPhoneAsync(request);
        }

        public async ValueTask<IPersonalDataModel> FindByPhraseAsync(string phrase)
        {
            var request = new GetByPhraseGrpcRequest {UserId = await _ctx.HttpContext.GetBoUserId(), Phrase = phrase};

            return await _dataSource.GetByPhraseAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .FirstOrDefaultAsync();
        }

        public async ValueTask UpdateOwnershipAsync(string[] tradersId, string ownerId, string userIdToAssign, string retentionManagerId)
        {
            var boUserRequest = new AssignBackOfficeUserGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TradersId = tradersId,
                UserIdToAssign = userIdToAssign
            };

            var ownerRequest = new AssignOwnerGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TradersId = tradersId, OwnerId = ownerId};
            
            var request = new AssignBackOfficeUserGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TradersId = tradersId,
                UserIdToAssign = retentionManagerId
            };

            await _dataSource.AssignBackOfficeUserGrpcRequest(boUserRequest);
            await _dataSource.AssignOwnerAsync(ownerRequest);
            await _dataSource.AssignRetentionManagerAsync(request);
        }

        public async ValueTask UpdateRetentionManagerAsync(string[] tradersId, string userIdToAssign)
        {
            var request = new AssignBackOfficeUserGrpcRequest
            {
                BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TradersId = tradersId,
                UserIdToAssign = userIdToAssign
            };

            await _dataSource.AssignRetentionManagerAsync(request);
        }

        public async ValueTask UpdateIsInternalAsync(string[] tradersId, bool isInternal)
        {
            var request = new UpdateIsInternalGrpcRequest
                {TradersId = tradersId, IsInternal = isInternal, BackOfficeUserId = await _ctx.HttpContext.GetBoUserId()};

            await _dataSource.UpdateIsInternalAsync(request);
        }

        public async ValueTask UpdateExternalDataAsync(string traderId, string key, string newValue)
        {
            var request = new UpdateTraderExternalDataGrpcRequest
                {BoUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId, KeyId = key, Value = newValue};

            await _dataSource.UpdateTraderExternalDataAsync(request);
        }

        public async ValueTask AssignNextCallDateAsync(string traderId, DateTime date)
        {
            var request = new UpdateNextCallDateGrpcRequest
                {BoUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId, Date = date};

            await _dataSource.UpdateNextCallDateAsync(request);
        }

        public async ValueTask<bool> UpdateEmailAsync(string email, string traderId, string boUserId)
        {
            var request = new UpdateEmailGrpcRequest {Email = email, BackOfficeUserId = boUserId, Id = traderId};
            return (await _dataSource.UpdateEmailAsync(request)).IsOk;
        }

        public async ValueTask UpdateAsync(IPersonalDataModel pd)
        {
            await _dataSource.UpdateAsync(pd.ToUpdateGrpc());
        }

        public async ValueTask UpdateConfirmAsync(string traderId, bool isConfirm)
        {
            var request = new ConfirmGrpcRequest {TraderId = traderId, Confirmed = isConfirm};
            await _dataSource.ConfirmAsync(request);
        }

        public async Task<(IBoAchievementsStatus, IBoAchievementsStatus)> GetTraderAchievementStatus(string traderId)
        {
            var request = new GetTraderMarketingSalesDataGrpcRequest
                {BackOfficeUserId =await _ctx.HttpContext.GetBoUserId(), TraderId = traderId};
            var traderStatus = (await _achievementReader.GetLiveAsync(request)).TraderMarketingSalesData;

            return (BoAchievementsStatus.Create(traderStatus.AchievementCRMStatus),
                BoAchievementsStatus.Create(traderStatus.AchievementStatus));
        }

        public async ValueTask<IEnumerable<ITraderDocumentModel>> GetUploadedDocumentsAsync(string traderId)
        {
            var request = new GetUploadedTraderDocumentsGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId};

            return await _tradersDocumentsGrpc.GetUploadedDocumentsAsync(request)
                .SelectAsync(itm => itm.ToDomain()).AsListAsync();
        }

        public async ValueTask<(string mime, byte[] data)> GetDocumentContentAsync(string traderId, string documentId)
        {
            var request = new GetDocumentContentGrpcRequest
                {BackOfficeUserId = await _ctx.HttpContext.GetBoUserId(), TraderId = traderId, DocumentId = documentId};

            var response = await _tradersDocumentsGrpc.GetDocumentContentAsync(request);
            return (response.Content.Mime, response.Content.Data);
        }

        public async ValueTask UploadDocumentAsync(string traderId, string fileName, TraderDocumentType type, byte[] file,
            bool clientVisible)
        {
            var request = new UploadDocumentCrmGrpcRequest
                {TraderId = traderId, FileName = fileName, DocumentType = type.ToGrpc(), Data = file, VisibleForClient = clientVisible};

            await _tradersDocumentsGrpc.UploadDocumentAsync(request);
        }

        public async ValueTask DeleteDocumentAsync(string traderId, string documentId)
        {
            var request = new DeleteDocumentCrmGrpcRequest {TraderId = traderId, DocumentId = documentId};
            await _tradersDocumentsGrpc.DeleteDocumentAsync(request);
        }

        public async ValueTask ChangeVisibilityAsync(string traderId, string documentId)
        {
            var request = new ChangeDocumentVisibilityCrmGrpcRequest {TraderId = traderId, DocumentId = documentId};
            await _tradersDocumentsGrpc.ChangeVisibilityAsync(request);
        }

        public async Task<IEnumerable<ITraderUtm>> GetTraderUtm(string traderId)
        {
            var request = new GetTraderUrmParametersGrpcRequest {TraderId = traderId};
            return await _tradersUtmService.GetTraderUtmParametersAsync(request)
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask AddOrUpdateTraderUtm(string traderId, string key, string value)
        {
            var request = new UpdateTraderUtmsGrpcRequest
            {
                TraderId = traderId,
                TraderUtms = new List<TraderUrmKeyValueModel> {new TraderUrmKeyValueModel {Key = key, Value = value}}
            };
            await _tradersUtmService.UpdateTraderUtmsAsync(request);
        }
    }
}