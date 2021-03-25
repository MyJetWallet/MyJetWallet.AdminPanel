using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using MyCrm.AffiliateAccess.Grpc;
using MyCrm.AffiliateAccess.Grpc.Models;

namespace Backoffice.Services.Affiliates
{
    public class AffiliatesService : IAffiliateService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IMyCrmAffiliateAccessServiceGrpc _datasource;

        public AffiliatesService(IHttpContextAccessor ctx, IMyCrmAffiliateAccessServiceGrpc datasource)
        {
            _ctx = ctx;
            _datasource = datasource;
        }

        public async Task<IReadOnlyList<IAffiliate>> GetAsync()
        {
            var grpcResponse = await _datasource.GetAccessAsync();

            if (grpcResponse.Items == null)
                return Array.Empty<IAffiliate>();

            return grpcResponse.Items.Select(itm => itm.ToDomain()).ToList();
        }

        public async Task AddOrUpdate(IAffiliate src)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            await _datasource.SaveAccessAsync(src.ToUpdateRequest(id));
        }

        public async Task DeleteAsync(string apiKey)
        {
            var id = await _ctx.HttpContext.GetBoUserId();
            var request = new DeleteGrpcModel {ApiKey = apiKey, BoUserId = id};
            await _datasource.DeleteAsync(request);
        }
    }
}