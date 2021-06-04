using System;
using System.Collections.Generic;
using System.Linq;
using Service.Liquidity.Converter.Domain.Models.Settings;

namespace Backoffice.Components.Converter.Settings.Models
{
    public class ConverterSettingsRepository
    {
        public string Caption { get; set; }
        
        private LiquidityConverterSettings _settings = new();
        public LiquidityConverterSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                Caption = value.BrokerId;
                if (value.CrossAssetSymbols != null && value.CrossAssetSymbols.Count != 0)
                {
                    SetCrossAssets(value.CrossAssetSymbols);
                }
            }
        }

        private string _crossAssets;
        public string CrossAssets
        {
            get => _crossAssets;
            set
            {
                _crossAssets = value;
                _settings.CrossAssetSymbols = Array.ConvertAll(value.Split(','), p => p.Trim()).ToList();
            }
        }

        private void SetCrossAssets(List<string> crossAssetList)
        {
            _crossAssets = string.Empty;
            if (crossAssetList == null || crossAssetList.Count == 0)
            {
                return;
            }
            crossAssetList.ForEach(elem =>
            {
                if (!string.IsNullOrWhiteSpace(_crossAssets))
                {
                    _crossAssets += ", ";
                }
                _crossAssets += elem;
            });
        }
    }
}