using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Backoffice.ClassComponents
{
    public class CsvDownload<T> : ComponentBase
    {
        [Inject] public IJSRuntime JsRuntime { get; set; }

        public async Task DownloadCsv(IEnumerable<T> data)
        {
            await JsRuntime.InvokeAsync<object>(
                "FileSaveAs",
                "report.csv",
                Encoding.UTF8.GetString(WriteCsvToMemory(data))
            );
        }

        private byte[] WriteCsvToMemory(IEnumerable<T> records, CsvConfiguration csvConfiguration = null)
        {
            csvConfiguration  ??= new CsvConfiguration(CultureInfo.CurrentCulture)
            { 
                Delimiter = "|"
            };
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var csvWriter = new CsvWriter(streamWriter, csvConfiguration);

            csvWriter.WriteRecords(records);
            streamWriter.Flush();
            return memoryStream.ToArray();
        }
    }
}