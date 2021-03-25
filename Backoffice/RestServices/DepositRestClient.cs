using System;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Finance.PciDssPublic.HttpContracts.Requests;
using Finance.PciDssPublic.HttpContracts.Responses;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

namespace Backoffice.RestServices
{
    public class DepositRestClient
    {
        private string MonfexApi;
        private string AlianceApi;
        private string HpApi;

        public DepositRestClient(string monfexApi, string hpApi, string alApi)
        {
            HpApi = hpApi;
            AlianceApi = alApi;
            MonfexApi = monfexApi;
        }

        public async Task<CreatePciDssInvoiceResponse> MakeDeposit(CreatePciDssInvoiceRequest request, IPersonalDataModel pd)
        {
            var json = JsonConvert.SerializeObject(request);
            
            var response = await $"{GetDepositLink(pd)}"
                .AppendPathSegments("deposit", "CreatePciDssInvoice")
                .AllowHttpStatus("400")
                .WithHeader("Content-Type", "application/json")
                .PostStringAsync(json)
                .ReceiveString();

            return JsonConvert.DeserializeObject<CreatePciDssInvoiceResponse>(response);
        }

        private string GetDepositLink(IPersonalDataModel pd)
        {
            var brand = pd
                .ExternalInfo?
                .FirstOrDefault(itm => itm.Key == "brandId");

            if (brand == null)
            {
                return MonfexApi;
            }

            if (brand.Value.IsMonfex())
            {
                return MonfexApi;
            }

            return brand.Value.IsHp() ? HpApi : AlianceApi;
        }
    }
}