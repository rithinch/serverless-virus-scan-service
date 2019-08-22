using DocumentScanningService.Functions.Models;
using DocumentScanningService.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using nClam;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentScanningService.Functions
{
    public class Scan
    {
        #region Constants...
        private const string INVALID_REQUEST_MESSAGE = "Invalid Request.";
        #endregion

        #region Function Definition...
        [FunctionName("Scan")]
        public static async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            IActionResult _ActionResult = new BadRequestObjectResult(INVALID_REQUEST_MESSAGE); ;

            ScanRequest _RequestModel = await GetModelFromRequestBodyAsync(req.Body);

            if (!string.IsNullOrEmpty(_RequestModel.Base64EncodedBytes))
            {
                _ActionResult = new UnprocessableEntityResult();

                ClamScanResult _Result = await new FileScanManager(new ClamClient(Environment.GetEnvironmentVariable("CLAM_AV_SERVER"))).Scan(_RequestModel.Base64EncodedBytes);

                if (_Result != null)
                {
                    ScanResponse _Respone = new ScanResponse()
                    {
                        Result = Enum.GetName(typeof(ClamScanResults), _Result.Result),
                        Threat = _Result.InfectedFiles?.First().VirusName ?? string.Empty
                    };

                    _ActionResult = new JsonResult(_Respone);
                }
            }

            return _ActionResult;
        }
        #endregion

        #region Methods...

        #region GetModelFromRequestBodyAsync
        private static async Task<ScanRequest> GetModelFromRequestBodyAsync(Stream body)
        {
            string _RequestBody = await new StreamReader(body).ReadToEndAsync();
            ScanRequest _Data = new ScanRequest() { Base64EncodedBytes = _RequestBody};
            return _Data;
        }
        #endregion

        #endregion
    }
}
