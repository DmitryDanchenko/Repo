using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConfigHelper.Models;
using CvLab.Framework.Standard.Extensions;
using CvLab.Tensor.DataCaptureCommonLib.Config.Common;
using CvLab.Tensor.TrainDatasetsLib.Enums;
using Newtonsoft.Json;

namespace ConfigHelper.Presenters;

public class Presenter
{
    public TensorDataCaptureConfig TensorDataCaptureConfig;

    private DataCaptureConfigTools _dataCaptureConfigTools = new DataCaptureConfigTools();

    public BoxManager BoxMeneger = new BoxManager();

    public ExcelManager ExcelManager = new ExcelManager();

   public string LoadDataCaptureConfig( string contentJson )
   {
        var fileContent = string.Empty;
        TensorDataCaptureConfig = _dataCaptureConfigTools.Load(contentJson);

        var boxes = TensorDataCaptureConfig.CaptureManagerConfig.Boxes.ToList();
        BoxMeneger.Boxes = boxes;

        fileContent = JsonConvert.SerializeObject(TensorDataCaptureConfig, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
        return fileContent;
   }

    private void UchekOldCheckedListBox(CheckedListBox checkedListBox)
    {
        for (var i = 0; i < checkedListBox.Items.Count; i++)
        {
            checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
        }
    }

    public void ShowDefect(CheckedListBox checkedListBox, SelectedSensor[] sensors)
    {
        if(BoxMeneger.Boxes==null)
            return;
        UchekOldCheckedListBox(checkedListBox);
       foreach (var sensorDef in sensors)
       {
            switch ((int)sensorDef)
            {
                case 0:
                    break;
                case 6:
                    checkedListBox.SetItemChecked(0, true);
                    checkedListBox.SetItemChecked(2, true);
                    break;
                case 7:
                    checkedListBox.SetItemChecked(0, true);
                    checkedListBox.SetItemChecked(3, true);
                    break;
                case 8:
                    checkedListBox.SetItemChecked(1, true);
                    checkedListBox.SetItemChecked(2, true);
                    break;
                case 9:
                    checkedListBox.SetItemChecked(1, true);
                    checkedListBox.SetItemChecked(2, true);
                    break;
                case 11:
                    checkedListBox.SetItemChecked(0, true);
                    checkedListBox.SetItemChecked(1, true);
                    checkedListBox.SetItemChecked(2, true);
                    checkedListBox.SetItemChecked(3, true);
                    break;
                default:
                    checkedListBox.SetItemChecked((int)sensorDef -1, true);
                    break;
            }
       }
    }

    public List<BoxEndPoint> GetBoxes()
    {
        return BoxMeneger.Boxes;
    }

    public void UpdateItemsBox(ComboBox comboBoxIdBox)
    {
        if (BoxMeneger.Boxes == null)
            return;
        var boxsesId = BoxMeneger.Boxes.Select(s => s.BoxNumber.ToString()).ToArray();
        comboBoxIdBox.Items.Clear();
        comboBoxIdBox.Items.AddRange(boxsesId);
    }

    public bool TryGetUpdateConfig(out string fileContent)
    {
        fileContent = "The config is not filled in correctly";
        if(TensorDataCaptureConfig == null)
            return false;
        var newTensorDataCaptureConfig = new TensorDataCaptureConfig()
        {
            ServiceBus = TensorDataCaptureConfig.ServiceBus,
            TrainDatasetsDb = TensorDataCaptureConfig.TrainDatasetsDb,
            ServiceNetAddress = TensorDataCaptureConfig.ServiceNetAddress,
            ServicePort = TensorDataCaptureConfig.ServicePort,
            TensorControlPointId = TensorDataCaptureConfig.TensorControlPointId,
            TaskBusDelayMinutes = TensorDataCaptureConfig.TaskBusDelayMinutes,
            DatasetsCachingTimeoutMin = TensorDataCaptureConfig.DatasetsCachingTimeoutMin,
            CaptureManagerConfig = new CaptureManagerConfig()
            {
                InitialBucketSize = TensorDataCaptureConfig.CaptureManagerConfig.InitialBucketSize,
                BucketSizeBeforeTrain = TensorDataCaptureConfig.CaptureManagerConfig.BucketSizeBeforeTrain,
                BucketSizeDuringTrain = TensorDataCaptureConfig.CaptureManagerConfig.BucketSizeDuringTrain,
                StdLimitToDetectTrain = TensorDataCaptureConfig.CaptureManagerConfig.StdLimitToDetectTrain,
                Groups = TensorDataCaptureConfig.CaptureManagerConfig.Groups,
                Boxes = BoxMeneger.Boxes.ToArray()
            }
        };
        TensorDataCaptureConfig = newTensorDataCaptureConfig;

        if (!newTensorDataCaptureConfig.IsConfigFilledCorrectly())
            return false;
       
    fileContent = JsonConvert.SerializeObject(newTensorDataCaptureConfig, Formatting.Indented, new Newtonsoft.Json.Converters.StringEnumConverter());
    return true;
    }
}
