using nClam;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentScanningService.Managers
{
    #region FileScanManager
    public class FileScanManager
    {
        #region Class Variables...
        private IClamClient __ClamClient;
        #endregion

        #region Constructors...
        public FileScanManager(IClamClient clamClient)
        {
            __ClamClient = clamClient;
        }
        #endregion

        #region Methods...

        #region Scan
        public async Task<ClamScanResult> Scan(string base64EncodedBytes)
        {
            return await __ClamClient.SendAndScanFileAsync(Convert.FromBase64String(base64EncodedBytes));
        }
        #endregion

        #endregion
    }
    #endregion
}
