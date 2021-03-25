using System.Collections.Generic;
using MyCRM.Logs.GrpcContracts;
using MyCRM.Logs.GrpcContracts.Contracts;
using MyCRM.Logs.GrpcContracts.Models;

namespace Backoffice.Mocks
{
    public class MyCrmLogsGrpcServiceMoq: IMyCrmLogsGrpcService
    {
        public IAsyncEnumerable<RegistrationEventGrpcModel> GetRegistrationsAsync(RegistrationLogGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AuthenticationGrpcEvent> GetAuthenticationsAsync(GetAuthenticationsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}