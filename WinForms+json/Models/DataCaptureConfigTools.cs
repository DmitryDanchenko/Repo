using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLab.Tensor.DataCaptureCommonLib.Config.Common;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;

namespace ConfigHelper.Models;

public class DataCaptureConfigTools
{
    public TensorDataCaptureConfig Load(string json)
    {
        var dataCaptureConfig = new TensorDataCaptureConfig();
            dataCaptureConfig = JsonConvert.DeserializeObject<TensorDataCaptureConfig>(json);
            return dataCaptureConfig;
    }
}
