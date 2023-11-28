using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConfigHelper.Presenters;
using CvLab.Framework.Standard.Extensions;
using CvLab.Tensor.DataCaptureCommonLib.Config.Common;
using CvLab.Tensor.TrainDatasetsLib.DatasetsVer4.Config;
using CvLab.Tensor.TrainDatasetsLib.DatasetsVer4.ConfigsForWAS;
using CvLab.Tensor.TrainDatasetsLib.Enums;
using CvLab.TensorCore.Common.SGDK.ProcessingBox;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;

namespace ConfigHelper.Models;

public class BoxManager
{
    public List<BoxEndPoint> Boxes{ get; set; }

    private BoxEndPoint _box;

    public SelectedSensor[] NumbersMkDeadSensor;

    public SelectedSensor[] NumbersMkSensorAbruptlyChangesZeroLvl;

    public SelectedSensor[] NumbersMkUncorrectNoiseAfterTrain;

    public SelectedSensor[] NumbersMkPulseNoise;

    public SelectedSensor[] NumbersMkBoxIsUnsutableForWeightCalculating;

    public SelectedSensor[] NumbersMkSignalIsTooHigh;

    public SelectedSensor[] NumbersMkInvertedSignal;

    public SelectedSensor[] NumbersMkUnableToFixDac;

    public SelectedSensor[] NumbersMkStdIsTooHighOrLow;

    public SelectedSensor[] NumbersMkSensorsAreMutuallyConfused;

    public SelectedSensor[] NumbersMkBoxIsUnsutableForTrainDetecting;


    public void GetSensorsDefects(int numberBox)
    {
        if (Boxes == null)
            return;
        _box = Boxes.First(BoxNumber => BoxNumber.BoxNumber == numberBox);
        var signalDefectConfig = _box.SignalDefects;
        NumbersMkDeadSensor = GetNumbersMkDeadSensor(signalDefectConfig);
        NumbersMkSensorAbruptlyChangesZeroLvl = GetNumbersMkSensorAbruptlyChangesZeroLvl(signalDefectConfig);
        NumbersMkUncorrectNoiseAfterTrain = GetNumbersMkUncorrectNoiseAfterTrain(signalDefectConfig);
        NumbersMkPulseNoise = GetNumbersMkPulseNoise(signalDefectConfig);
        NumbersMkBoxIsUnsutableForWeightCalculating = GetNumbersMkBoxIsUnsutableForWeightCalculating(signalDefectConfig);
        NumbersMkSignalIsTooHigh = GetNumbersMkSignalIsTooHigh(signalDefectConfig);
        NumbersMkInvertedSignal = GetNumbersMkInvertedSignal(signalDefectConfig);
        NumbersMkUnableToFixDac = GetNumbersMkUnableToFixDac(signalDefectConfig);
        NumbersMkStdIsTooHighOrLow = GetNumbersMkStdIsTooHighOrLow(signalDefectConfig);
        NumbersMkSensorsAreMutuallyConfused = GetNumbersMkSensorsAreMutuallyConfused(signalDefectConfig);
        NumbersMkBoxIsUnsutableForTrainDetecting = GetNumbersMkBoxIsUnsutableForTrainDetecting(signalDefectConfig);
    }

    private SelectedSensor[] GetNumbersMkDeadSensor(SignalDefectConfig[] signalDefectConfig)
    {
        var MkDeadSensor = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.DeadSensor);
        var numbersMkDeadSensor = MkDeadSensor.Select(s => s.SelectedSensor).ToArray();
        return numbersMkDeadSensor;
    }

    public void SetNumbersMkSensorsDefects(int numberBox, CheckedListBox checkedListDeadMk, CheckedListBox checkedListZeroChange,
        CheckedListBox checkedListConstantNoise, CheckedListBox checkedListPulseNoise, CheckedListBox checkedListBadBoxingForWeight,
        CheckedListBox checkedListOutOfRange, CheckedListBox checkedListInvertedSignal, CheckedListBox checkedListNotReact,
        CheckedListBox checkedListSKO, CheckedListBox checkedListConfused, CheckedListBox checkedListBadGrabbing)
    {
        if (Boxes == null)
            return;

        var boxConfig = Boxes.First(f => f.BoxNumber == numberBox);

        var signalDefekts = GetSignalDefects( checkedListDeadMk, checkedListZeroChange,
        checkedListConstantNoise, checkedListPulseNoise, checkedListBadBoxingForWeight,
        checkedListOutOfRange, checkedListInvertedSignal, checkedListNotReact,
        checkedListSKO, checkedListConfused, checkedListBadGrabbing);

        var NewBox = new BoxEndPoint()
        {
            BoxNumber = boxConfig.BoxNumber,
            ProtocolVer = boxConfig.ProtocolVer,
            DistanceBetweenSensorsMetre = boxConfig.DistanceBetweenSensorsMetre,
            SensorsLocation = boxConfig.SensorsLocation,
            IpAddress = boxConfig.IpAddress,
            Channels = boxConfig.Channels,
            BoxCoeffsForward = boxConfig.BoxCoeffsForward,
            WeightByFunctionConfig = boxConfig.WeightByFunctionConfig,
            SignalDefects = signalDefekts.ToArray(),
        };

        RemoveBox(numberBox);
        Boxes.Add(NewBox);
        Boxes.Sort((x, y) => x.BoxNumber.CompareTo(y.BoxNumber));
    }

    public void ShowBoxInfio(TextBox textBoxIdAddBox, TextBox textBoxIpAddress, TextBox textBoxDistanceBetweenSensorsMetre, ComboBox comboBoxProtocolVer,
        ComboBox comboBoxSensorsLocation, ComboBox comboBoxChannelA, ComboBox comboBoxChannelB)
    {
        if (textBoxIdAddBox != null)
            return;
        textBoxIdAddBox.Text = _box.BoxNumber.ToString();
        textBoxIpAddress.Text = _box.IpAddress.ToString();
        textBoxDistanceBetweenSensorsMetre.Text = _box.DistanceBetweenSensorsMetre.ToString();
        comboBoxProtocolVer.SelectedItem = _box.ProtocolVer;
        comboBoxSensorsLocation.SelectedItem = _box.SensorsLocation;
        comboBoxChannelA.SelectedItem = _box.Channels[0].AdditionalChannelDataType;
        if(_box.ProtocolVer == ProtocolVer.Kukin)
            comboBoxChannelB.SelectedItem = _box.Channels[1].AdditionalChannelDataType;
        else
            comboBoxChannelB.Enabled = false;
    }

    private List<SignalDefectConfig> GetSignalDefects(CheckedListBox checkedListDeadMk, CheckedListBox checkedListZeroChange,
        CheckedListBox checkedListConstantNoise, CheckedListBox checkedListPulseNoise, CheckedListBox checkedListBadBoxingForWeight,
        CheckedListBox checkedListOutOfRange, CheckedListBox checkedListInvertedSignal, CheckedListBox checkedListNotReact,
        CheckedListBox checkedListSKO, CheckedListBox checkedListConfused, CheckedListBox checkedListBadGrabbing)
    {
       var signalDefekts = new List<SignalDefectConfig>();

       var signalDefect = new SignalDefectConfig
       {
           SignalDefect = SignalDefect.BoxIsUnsutableForWeightCalculating,
           SelectedSensor = SelectedMkBadGrabbingAndBadBoxingForWeight(checkedListBadBoxingForWeight)
       };
        if (signalDefect.SelectedSensor != SelectedSensor.None)
            signalDefekts.Add(signalDefect);

        if (TryGetSelectedMkSensorsAreMutuallyConfused(checkedListConfused, out var selectedSensor))
        {
            signalDefect =
           new SignalDefectConfig
           {
               SignalDefect = SignalDefect.SensorsAreMutuallyConfused,
               SelectedSensor = selectedSensor
           };
        }

        if (signalDefect.SelectedSensor != SelectedSensor.None)
            signalDefekts.Add(signalDefect);

        signalDefect = new SignalDefectConfig
        {
            SignalDefect = SignalDefect.BoxIsUnsutableForTrainDetecting,
            SelectedSensor = SelectedMkBadGrabbingAndBadBoxingForWeight(checkedListBadGrabbing)
        };
        if (signalDefect.SelectedSensor != SelectedSensor.None)
            signalDefekts.Add(signalDefect);

        signalDefekts.AddRange(GetSignalDefect(checkedListDeadMk, SignalDefect.DeadSensor));

        signalDefekts.AddRange(GetSignalDefect(checkedListZeroChange, SignalDefect.SensorAbruptlyChangesZeroLvl));

        signalDefekts.AddRange(GetSignalDefect(checkedListConstantNoise, SignalDefect.UncorrectNoiseAfterTrain));

        signalDefekts.AddRange(GetSignalDefect(checkedListPulseNoise, SignalDefect.PulseNoise));

        signalDefekts.AddRange(GetSignalDefect(checkedListOutOfRange, SignalDefect.SignalIsTooHigh));

        signalDefekts.AddRange(GetSignalDefect(checkedListInvertedSignal, SignalDefect.InvertedSignal));

        signalDefekts.AddRange(GetSignalDefect(checkedListNotReact, SignalDefect.UnableToFixDac));

        signalDefekts.AddRange(GetSignalDefect(checkedListSKO, SignalDefect.StdIsTooHighOrLow));

        return signalDefekts;
    }

    private List<SignalDefectConfig> GetSignalDefect(CheckedListBox checkedList, SignalDefect typeSignalDefect)
    {
        var signalDefekts = new List<SignalDefectConfig>();

        var selectedSensors = SelectedMk(checkedList);
        foreach (var item in selectedSensors)
        {
            var signalDefect =
            new SignalDefectConfig
            {
                SignalDefect = typeSignalDefect,
                SelectedSensor = item
            };
            if (signalDefect.SelectedSensor != SelectedSensor.None)
                signalDefekts.Add(signalDefect);
        }
        return signalDefekts;
    }

    private SelectedSensor SelectedMkBadGrabbingAndBadBoxingForWeight(CheckedListBox checkedListBox)
    {
        List<string> selectedMk = new List<string>();
        foreach (var mk in checkedListBox.CheckedIndices)
        {
            selectedMk.Add(mk.ToString());
        }

        if (selectedMk.Count == 0)
            return SelectedSensor.None;
        else
            return SelectedSensor.AllSensors;
    }

    private bool TryGetSelectedMkSensorsAreMutuallyConfused(CheckedListBox checkedListBox, out SelectedSensor selectedSensor)
    {
        List<string> selectedMk = new List<string>();
        foreach (var mk in checkedListBox.CheckedIndices)
        {
            selectedMk.Add(mk.ToString());
        }

        selectedSensor = SelectedSensor.None;
        if (selectedMk.Count == 0)
            return true;

        var Mk1 = selectedMk.Contains("0");
        var Mk2 = selectedMk.Contains("1");
        var Mk3 = selectedMk.Contains("2");
        var Mk4 = selectedMk.Contains("3");

        if (Mk1 && Mk2 && Mk3 && Mk4)
        {
            selectedSensor = SelectedSensor.AllSensors;
            return true;
        }

        else if (Mk1 && Mk3 && !Mk2 && !Mk4)
        {
            selectedSensor = SelectedSensor.Sensor1and3;
            return true;
        }

        else if (Mk1 && Mk4 && !Mk3 && !Mk2)
        {
            selectedSensor = SelectedSensor.Sensor1and4;
            return true;
        }

        else if (Mk2 && Mk3 && !Mk1 && !Mk4)
        {
            selectedSensor = SelectedSensor.Sensor2and3;
            return true;
        }

        else if (Mk2 && Mk4 && !Mk1 && !Mk3)
        {
            selectedSensor = SelectedSensor.Sensor2and4;
            return true;
        }
        else
         return false;
    }

    private List<SelectedSensor> SelectedMk(CheckedListBox checkedListBox)
    {
        var selectedMk = new List<SelectedSensor>();

        List<string> chekedMk = new List<string>();
        foreach (var mk in checkedListBox.CheckedIndices)
        {
            chekedMk.Add(mk.ToString());
        }

        if (chekedMk.Count == 0)
        {
            selectedMk.Add(SelectedSensor.None);
            return selectedMk;
        }

        var Mk1 = chekedMk.Contains("0");
        var Mk2 = chekedMk.Contains("1");
        var Mk3 = chekedMk.Contains("2");
        var Mk4 = chekedMk.Contains("3");

        if (Mk1 && Mk2 && Mk3 && Mk4)
        {
            selectedMk.Add(SelectedSensor.AllSensors);
            return selectedMk;
        }

        else if (Mk1)
        {
            selectedMk.Add(SelectedSensor.Sensor1);
        }

        if (Mk2)
        {
            selectedMk.Add(SelectedSensor.Sensor2);
        }

        if (Mk3)
        {
            selectedMk.Add(SelectedSensor.Sensor3);
        }

        if (Mk4)
        {
            selectedMk.Add(SelectedSensor.Sensor4);
        }
        return selectedMk;
    }

    public void AddBox(TextBox textBoxIdBox, ComboBox comboBoxProtocolVer, ComboBox comboBoxSensorsLocation,
        TextBox textBoxIpAddress, TextBox textBoxDistanceBetweenSensorsMetre, ComboBox comboBoxChannelA, ComboBox comboBoxChannelB)
    {
        if (comboBoxProtocolVer.SelectedItem == null || comboBoxChannelA.SelectedItem == null || comboBoxChannelB.SelectedItem == null ||
            comboBoxSensorsLocation.SelectedItem == null)
            return;
        var channels = new List<BoxEndPointChannel>();
        var boxCoeffsForward = new BoxCoeffsConfig() { SensorCoeffsKvToTonne = new[] { 0.0037, 0.0037, 0.0037, 0.0037 }, CoeffK = 1 };

        if (comboBoxProtocolVer.SelectedItem.ToString() == ProtocolVer.Kukin.ToString())
        {
            var boxEndPointChannelA = new BoxEndPointChannel()
            {
                Port = 23,
                AdditionalChannelDataType = (AdditionalChannelDataType)comboBoxChannelA.SelectedItem
            };
            var boxEndPointChannelB = new BoxEndPointChannel()
            {
                Port = 26,
                AdditionalChannelDataType = (AdditionalChannelDataType)comboBoxChannelB.SelectedItem
            };
            channels.Add(boxEndPointChannelA);
            channels.Add(boxEndPointChannelB);

            boxCoeffsForward = new BoxCoeffsConfig() { SensorCoeffsKvToTonne = new[] { 0.0012, 0.0012, 0.0012, 0.0012 }, CoeffK = 1 };
        }
        else
        {
            channels.Add(
                new BoxEndPointChannel
                {
                    Port = 23,
                    AdditionalChannelDataType = (AdditionalChannelDataType)comboBoxChannelA.SelectedItem
                });
        }

        var box = new BoxEndPoint()
        {
            BoxNumber = Convert.ToInt16(textBoxIdBox.Text),
            ProtocolVer = (ProtocolVer)comboBoxProtocolVer.SelectedItem,
            SensorsLocation = (SensorsLocation)comboBoxSensorsLocation.SelectedItem,
            IpAddress = textBoxIpAddress.Text,
            Channels = channels.ToArray(),
            DistanceBetweenSensorsMetre = float.Parse(textBoxDistanceBetweenSensorsMetre.Text),
            BoxCoeffsForward = boxCoeffsForward
        };
        Boxes.Add(box);
        Boxes.Sort((x, y) => x.BoxNumber.CompareTo(y.BoxNumber));
    }

    public void RemoveBox(int boxNumber)
    {
        var box = Boxes.First(f => f.BoxNumber == boxNumber);
        Boxes.Remove(box);
    }

    public void SaveCoeff(List<(string, TextBox, TextBox)> textBoxesCoeff)
    {
        if (Boxes == null)
            return;
        for (int i = 0; i < Boxes.Count(); i++)
        {
            var boxConfig = Boxes[i];
            var textBoxCoeff = textBoxesCoeff.First(f => f.Item1 == "Box" + boxConfig.BoxNumber);
            var coeffK = textBoxCoeff.Item2.Text.ToString();
            var coeffB = textBoxCoeff.Item3.Text.ToString();

            var newCoeffK = 1.0;
            var newCoeffB = 0.0;
            try
            {
                newCoeffK = Convert.ToDouble(coeffK, new NumberFormatInfo { NumberDecimalSeparator = "," });
                newCoeffB = Convert.ToDouble(coeffB, new NumberFormatInfo { NumberDecimalSeparator = "," });
            }
            catch (Exception)
            {
                newCoeffK = Convert.ToDouble(coeffK, new NumberFormatInfo { NumberDecimalSeparator = "." });
                newCoeffB = Convert.ToDouble(coeffB, new NumberFormatInfo { NumberDecimalSeparator = "." });
            }
            
            if (boxConfig.BoxCoeffsForward.CoeffK != newCoeffK ||
                boxConfig.BoxCoeffsForward.CoeffB != newCoeffB)
            {
                var newBox = new BoxEndPoint()
                {
                    BoxNumber = boxConfig.BoxNumber,
                    ProtocolVer = boxConfig.ProtocolVer,
                    DistanceBetweenSensorsMetre = boxConfig.DistanceBetweenSensorsMetre,
                    SensorsLocation = boxConfig.SensorsLocation,
                    IpAddress = boxConfig.IpAddress,
                    Channels = boxConfig.Channels,
                    WeightByFunctionConfig = boxConfig.WeightByFunctionConfig,
                    SignalDefects = boxConfig.SignalDefects,
                    BoxCoeffsForward = new BoxCoeffsConfig()
                    {
                        CoeffK = newCoeffK,
                        CoeffB = newCoeffB,
                        TemperatureCoeff = boxConfig.BoxCoeffsForward.TemperatureCoeff,
                        ReserveProp1 = boxConfig.BoxCoeffsForward.ReserveProp1,
                        ReserveProp2 = boxConfig.BoxCoeffsForward.ReserveProp2,
                        ReserveProp3 = boxConfig.BoxCoeffsForward.ReserveProp3,
                        SensorCoeffsKvToTonne = boxConfig.BoxCoeffsForward.SensorCoeffsKvToTonne
                    },
                };
                RemoveBox(boxConfig.BoxNumber);
                Boxes.Add(newBox);
                Boxes.Sort((x, y) => x.BoxNumber.CompareTo(y.BoxNumber));
            }
        }
    }

    private SelectedSensor[] GetNumbersMkSensorAbruptlyChangesZeroLvl(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorAbruptlyChangesZeroLvl = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.SensorAbruptlyChangesZeroLvl);
        var numbersMkSensorAbruptlyChangesZeroLvl = MkSensorAbruptlyChangesZeroLvl.Select(s => s.SelectedSensor).ToArray();
        return numbersMkSensorAbruptlyChangesZeroLvl;
    }

    private SelectedSensor[] GetNumbersMkUncorrectNoiseAfterTrain(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectNoiseAfterTrain = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.UncorrectNoiseAfterTrain);
        var numbersMkUncorrectNoiseAfterTrain = MkSensorUncorrectNoiseAfterTrain.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectNoiseAfterTrain;
    }

    private SelectedSensor[] GetNumbersMkPulseNoise(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectPulseNoise = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.PulseNoise);
        var numbersMkUncorrectPulseNoise = MkSensorUncorrectPulseNoise.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectPulseNoise;
    }

    private SelectedSensor[] GetNumbersMkBoxIsUnsutableForWeightCalculating(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectBoxIsUnsutableForWeightCalculating = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.BoxIsUnsutableForWeightCalculating);
        var numbersMkUncorrectBoxIsUnsutableForWeightCalculating = MkSensorUncorrectBoxIsUnsutableForWeightCalculating.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectBoxIsUnsutableForWeightCalculating;
    }

    private SelectedSensor[] GetNumbersMkSignalIsTooHigh(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectSignalIsTooHigh = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.SignalIsTooHigh);
        var numbersMkUncorrectSignalIsTooHigh = MkSensorUncorrectSignalIsTooHigh.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectSignalIsTooHigh;
    }

    private SelectedSensor[] GetNumbersMkInvertedSignal(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectInvertedSignal = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.InvertedSignal);
        var numbersMkUncorrectInvertedSignal = MkSensorUncorrectInvertedSignal.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectInvertedSignal;
    }

    private SelectedSensor[] GetNumbersMkUnableToFixDac(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectUnableToFixDac = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.UnableToFixDac);
        var numbersMkUncorrectUnableToFixDac = MkSensorUncorrectUnableToFixDac.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectUnableToFixDac;
    }

    private SelectedSensor[] GetNumbersMkStdIsTooHighOrLow(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectStdIsTooHighOrLow = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.StdIsTooHighOrLow);
        var numbersMkUncorrectStdIsTooHighOrLow = MkSensorUncorrectStdIsTooHighOrLow.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectStdIsTooHighOrLow;
    }

    private SelectedSensor[] GetNumbersMkSensorsAreMutuallyConfused(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectSensorsAreMutuallyConfused = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.SensorsAreMutuallyConfused);
        var numbersMkUncorrectSensorsAreMutuallyConfused = MkSensorUncorrectSensorsAreMutuallyConfused.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectSensorsAreMutuallyConfused;
    }

    private SelectedSensor[] GetNumbersMkBoxIsUnsutableForTrainDetecting(SignalDefectConfig[] signalDefectConfig)
    {
        var MkSensorUncorrectBoxIsUnsutableForTrainDetecting = signalDefectConfig.Where(w => w.SignalDefect == SignalDefect.BoxIsUnsutableForTrainDetecting);
        var numbersMkUncorrectBoxIsUnsutableForTrainDetecting = MkSensorUncorrectBoxIsUnsutableForTrainDetecting.Select(s => s.SelectedSensor).ToArray();
        return numbersMkUncorrectBoxIsUnsutableForTrainDetecting;
    }
}
