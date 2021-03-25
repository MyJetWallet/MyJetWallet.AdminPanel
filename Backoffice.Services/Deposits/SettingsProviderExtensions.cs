using Backoffice.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backoffice.Components.Deposits
{
    public static class SettingsProviderExtensions
    {
        public static int CalculateTotalWeight(this ICollection<IPaymentProviderSettingsModel> settingsModels )
        {
            if (settingsModels is null || settingsModels.Count == 0)
            {
                return 0;
            }

            return settingsModels.Where(x => !x.IsDeleted).Sum(x => x.Weight);
        }
    }
}
