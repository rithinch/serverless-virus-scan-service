using nClam;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DocumentScanningService.Functions.Models
{
    #region ScanResponse
    public class ScanResponse
    {
        public string Result { get; set; }
        public string Threat { get; set;}
    }
    #endregion
}
