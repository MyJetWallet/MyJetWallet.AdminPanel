using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Calls.Grpc;
using MyCrm.Calls.Grpc.Contracts;
using MyCrm.Calls.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmPhonePoolGrpcServiceMoq: IMyCrmPhonePoolGrpcService
    {
        public IAsyncEnumerable<PhonePoolGrpcModel> GetPhonePoolsAsync(GetPhonesPoolsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async ValueTask<PhonePoolGrpcModel> GetPhonePoolAsync(GetPhonePoolGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask InsertOrUpdateAsync(PhonePoolGrpcModel model)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask DeletePhonePoolAsync(DeletePhonePoolGrpcRequest model)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AssignPhonePoolToBusinessGroupAsync(AssignPhonePoolToBusinessGroupRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<PhonePoolGrpcModel> GetBusinessGroupPhonePoolAsync(GetBusinessGroupPhonePoolRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}