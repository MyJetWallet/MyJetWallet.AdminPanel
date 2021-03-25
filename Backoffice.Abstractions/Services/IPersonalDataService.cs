using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IPersonalDataService
    {
        ValueTask<IPersonalDataModel> FindByPhraseAsync(string phrase);
        ValueTask AddAdditionalPhoneAsync(string traderId, string phone);
        ValueTask<IEnumerable<IBoAdditionalPhoneModel>> GetAdditionalPhonesAsync(string traderId);
        ValueTask DeleteAdditionalNumberAsync(string traderId, string numberId);
        ValueTask<IPersonalDataModel> GetTraderPersonaDataAsync(string traderId);
        ValueTask UpdateAdditionalNumberAsync(string traderId, string numberId, string phone);
        ValueTask UpdateOwnershipAsync(string[] tradersId, string ownerId, string userIdToAssign, string retentionManagerId);
        ValueTask UpdateRetentionManagerAsync(string[] tradersId, string userIdToAssign);
        ValueTask UpdateIsInternalAsync(string[] tradersId, bool isInternal);
        ValueTask UpdateExternalDataAsync(string traderId, string key, string newValue);
        ValueTask AssignNextCallDateAsync(string traderId, DateTime date);
        ValueTask<bool> UpdateEmailAsync(string email, string traderId, string boUserId);
        ValueTask UpdateAsync(IPersonalDataModel pd);
        ValueTask UpdateConfirmAsync(string traderId, bool isConfirm);
        Task<(IBoAchievementsStatus, IBoAchievementsStatus)> GetTraderAchievementStatus(string traderId);
        ValueTask<IEnumerable<ITraderDocumentModel>> GetUploadedDocumentsAsync(string traderId);
        ValueTask<(string mime, byte[] data)> GetDocumentContentAsync(string traderId, string documentId);
        ValueTask UploadDocumentAsync(string traderId, string fileName, TraderDocumentType type, byte[] file,
            bool clientVisible);
        ValueTask DeleteDocumentAsync(string traderId, string documentId);
        ValueTask ChangeVisibilityAsync(string traderId, string documentId);
        Task<IEnumerable<ITraderUtm>> GetTraderUtm(string traderId);
        ValueTask AddOrUpdateTraderUtm(string traderId, string key, string value);
    }
}