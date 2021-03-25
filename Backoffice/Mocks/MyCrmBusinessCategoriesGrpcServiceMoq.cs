using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.BusinessCategories.Grpc;
using MyCrm.BusinessCategories.Grpc.Contracts;
using MyCrm.BusinessCategories.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmBusinessCategoriesGrpcServiceMoq: IMyCrmBusinessCategoriesGrpcService
    {
        public async IAsyncEnumerable<BusinessCategoryGrpcModel> GetAsync(GetBusinessCategoriesGrpcRequest request)
        {
            yield return new BusinessCategoryGrpcModel()
            {
                Id = request.BackOfficeUserId,
                IsDisabled = false,
                Name = request.BackOfficeUserId,
                PhonePoolId = "PhonePoolId"
            };
        }

        public ValueTask InsertOrReplaceAsync(BusinessCategoryGrpcModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}