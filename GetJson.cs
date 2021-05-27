using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;

namespace MinGyu.Function
{
    public static class GetJson
    {
        [FunctionName("GetJson")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connStrA = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connStrA);
            BlobContainerClient containerA = blobServiceClient.GetBlobContainerClient("json");
            BlobClient blockBlobZ = containerA.GetBlobClient("abc.json");

            if (await blockBlobZ.ExistsAsync())
            {
                using (MemoryStream msZ = new MemoryStream())
                {
                    await blockBlobZ.DownloadToAsync(msZ);
                    string dataA = System.Text.Encoding.UTF8.GetString(msZ.ToArray());
                    return dataA;
                }
            }
            else
            {
                return "No Data";
            }
        }
    }
}
