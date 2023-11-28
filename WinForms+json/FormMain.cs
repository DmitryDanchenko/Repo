using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ConfigHelper.Models;
using ConfigHelper.Presenters;

using CvLab.Tensor.DataCaptureCommonLib.Config.Common;
using CvLab.Tensor.TrainDatasetsLib.DatasetsVer4.Config;
using CvLab.Tensor.TrainDatasetsLib.Enums;
using CvLab.TensorCore.Common.SGDK.ProcessingBox;

using Org.BouncyCastle.Utilities.Net;

namespace DCConfigHelper;

public partial class FormMain : Form
{
    private Presenter _presenter = new Presenter();

    public FormMain()
    {
        InitializeComponent();
        comboBoxProtocolVer.Items.AddRange(new object[] { ProtocolVer.Kukin, ProtocolVer.Lysenko });
        comboBoxSensorsLocation.Items.AddRange(new object[] { SensorsLocation.InTheMiddleBetweenSleepers, SensorsLocation.CloseToTheSleeper });
        comboBoxChannelA.Items.AddRange(new object[] { AdditionalChannelDataType.None, AdditionalChannelDataType.Voltage,
            AdditionalChannelDataType.GroundTemperature, AdditionalChannelDataType.RailsTemperature});
        comboBoxChannelB.Items.AddRange(new object[] { AdditionalChannelDataType.None, AdditionalChannelDataType.Voltage,
            AdditionalChannelDataType.GroundTemperature, AdditionalChannelDataType.RailsTemperature});
    }

    private List<(string, TextBox, TextBox)> JoinTextBox()
    {
        return new List<(string, TextBox, TextBox)>()
        {
            new(lblBox1.Text, textBoxCoeffKBox1, textBoxCoeffBBox1),
            new(lblBox2.Text, textBoxCoeffKBox2, textBoxCoeffBBox2),
            new(lblBox3.Text, textBoxCoeffKBox3, textBoxCoeffBBox3),
            new(lblBox4.Text, textBoxCoeffKBox4, textBoxCoeffBBox4),
            new(lblBox5.Text, textBoxCoeffKBox5, textBoxCoeffBBox5),
            new(lblBox6.Text, textBoxCoeffKBox6, textBoxCoeffBBox6),
            new(lblBox7.Text, textBoxCoeffKBox7, textBoxCoeffBBox7),
            new(lblBox8.Text, textBoxCoeffKBox8, textBoxCoeffBBox8),
            new(lblBox9.Text, textBoxCoeffKBox9, textBoxCoeffBBox9),
            new(lblBox10.Text, textBoxCoeffKBox10, textBoxCoeffBBox10),
            new(lblBox11.Text, textBoxCoeffKBox11, textBoxCoeffBBox11),
            new(lblBox12.Text, textBoxCoeffKBox12, textBoxCoeffBBox12),
            new(lblBox13.Text, textBoxCoeffKBox13, textBoxCoeffBBox13),
            new(lblBox14.Text, textBoxCoeffKBox14, textBoxCoeffBBox14),
            new(lblBox15.Text, textBoxCoeffKBox15, textBoxCoeffBBox15),
            new(lblBox16.Text, textBoxCoeffKBox16, textBoxCoeffBBox16),
            new(lblBox17.Text, textBoxCoeffKBox17, textBoxCoeffBBox17),
            new(lblBox18.Text, textBoxCoeffKBox18, textBoxCoeffBBox18),
            new(lblBox19.Text, textBoxCoeffKBox19, textBoxCoeffBBox19),
            new(lblBox20.Text, textBoxCoeffKBox20, textBoxCoeffBBox20),
            new(lblBox21.Text, textBoxCoeffKBox21, textBoxCoeffBBox21),
            new(lblBox22.Text, textBoxCoeffKBox22, textBoxCoeffBBox22),
            new(lblBox23.Text, textBoxCoeffKBox23, textBoxCoeffBBox23),
            new(lblBox24.Text, textBoxCoeffKBox24, textBoxCoeffBBox24)
        };
    }

    private void ShowSensorsDefects(BoxManager boxMeneger)
    {
        _presenter.ShowDefect(checkedListDeadMk, boxMeneger.NumbersMkDeadSensor);
        _presenter.ShowDefect(checkedListZeroChange, boxMeneger.NumbersMkSensorAbruptlyChangesZeroLvl);
        _presenter.ShowDefect(checkedListConstantNoise, boxMeneger.NumbersMkUncorrectNoiseAfterTrain);
        _presenter.ShowDefect(checkedListPulseNoise, boxMeneger.NumbersMkPulseNoise);
        _presenter.ShowDefect(checkedListBadBoxingForWeight, boxMeneger.NumbersMkBoxIsUnsutableForWeightCalculating);
        _presenter.ShowDefect(checkedListOutOfRange, boxMeneger.NumbersMkSignalIsTooHigh);
        _presenter.ShowDefect(checkedListInvertedSignal, boxMeneger.NumbersMkInvertedSignal);
        _presenter.ShowDefect(checkedListNotReact, boxMeneger.NumbersMkUnableToFixDac);
        _presenter.ShowDefect(checkedListSKO, boxMeneger.NumbersMkStdIsTooHighOrLow);
        _presenter.ShowDefect(checkedListConfused, boxMeneger.NumbersMkSensorsAreMutuallyConfused);
        _presenter.ShowDefect(checkedListBadGrabbing, boxMeneger.NumbersMkBoxIsUnsutableForTrainDetecting);

        _presenter.BoxMeneger.ShowBoxInfio(textBoxIdAddBox, textBoxIpAddress, textBoxDistanceBetweenSensorsMetre, comboBoxProtocolVer, comboBoxSensorsLocation,
            comboBoxChannelA, comboBoxChannelB);
    }

    private void comboBoxIdBox_SelectedIndexChanged_1(object sender, EventArgs e)
    {
        var selectedState = Convert.ToInt32(comboBoxIdBox.SelectedItem);
        _presenter.BoxMeneger.GetSensorsDefects(selectedState);
        ShowSensorsDefects(_presenter.BoxMeneger);
        textBoxDelBox.Text = selectedState.ToString();
    }

    private void btnDelBox_Click(object sender, EventArgs e)
    {
        var boxes = _presenter.GetBoxes();
        if (boxes == null)
            return;

        var boxesNambersFromConfig = boxes.Select(s => s.BoxNumber.ToString()).ToArray();
        if (!boxesNambersFromConfig.Contains(textBoxDelBox.Text))
            return;

        var BoxNumberForDel = Convert.ToInt32(textBoxDelBox.Text);
        _presenter.BoxMeneger.RemoveBox(BoxNumberForDel);
        _presenter.UpdateItemsBox(comboBoxIdBox);

        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;
    }

    private void btnAddBox_Click(object sender, EventArgs e)
    {
        _presenter.BoxMeneger.AddBox(textBoxIdAddBox, comboBoxProtocolVer, comboBoxSensorsLocation, textBoxIpAddress, textBoxDistanceBetweenSensorsMetre,
            comboBoxChannelA, comboBoxChannelB);
        _presenter.UpdateItemsBox(comboBoxIdBox);

        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;
    }

    private void btnSaveDataBox_Click(object sender, EventArgs e)
    {
        var selectedState = Convert.ToInt32(comboBoxIdBox.SelectedItem);
        _presenter.BoxMeneger.SetNumbersMkSensorsDefects(selectedState, checkedListDeadMk, checkedListZeroChange, checkedListConstantNoise,
            checkedListPulseNoise, checkedListBadBoxingForWeight, checkedListOutOfRange, checkedListInvertedSignal, checkedListNotReact,
            checkedListSKO, checkedListConfused, checkedListBadGrabbing);

        _presenter.BoxMeneger.GetSensorsDefects(selectedState);
        ShowSensorsDefects(_presenter.BoxMeneger);

        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;
    }

    private void comboBoxProtocolVer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBoxProtocolVer.SelectedItem.ToString() == ProtocolVer.Kukin.ToString())
            comboBoxChannelB.Enabled = true;
        else
            comboBoxChannelB.Enabled = false;
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
        // this.Cursor = Cursors.WaitCursor;
        // richTextBox.Cursor = Cursors.WaitCursor;
        tabControl.Cursor = Cursors.Arrow;
        _pageDefectsBoxes.Cursor = Cursors.Arrow;
        groupCreateNewBox.Cursor = Cursors.Arrow;
        checkedListDeadMk.Cursor = Cursors.Arrow;
        groupDeadMK.Cursor = Cursors.Arrow;
        lblDeadMK.Cursor = Cursors.Arrow;
        textBoxInfoError.Text = "";
        if (string.IsNullOrWhiteSpace(richTextBox.Text))
        {
            textBoxInfoError.Text = "not config!!!";
            return;
        }
        richTextBox.Text = _presenter.LoadDataCaptureConfig(richTextBox.Text);
        Text = _presenter.TensorDataCaptureConfig.TensorControlPointId.ToString();
        _presenter.UpdateItemsBox(comboBoxIdBox);

        var textBoxesCoeff = JoinTextBox();
        var boxes = _presenter.GetBoxes();
        for (var i = 0; i < boxes.Count(); i++)
        {
            var boxnamber = boxes[i].BoxNumber;
            var textBoxsCoeff = textBoxesCoeff.First(f => f.Item1 == "Box" + boxnamber);
            textBoxsCoeff.Item2.Text = boxes[i].BoxCoeffsForward.CoeffK.ToString();
            textBoxsCoeff.Item3.Text = boxes[i].BoxCoeffsForward.CoeffB.ToString();
        }
        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;

        //someVeryLongAndImportantFunction();
        //this.Cursor = Cursors.Default;
    }

    private void btnSaveCoeffi_Click(object sender, EventArgs e)
    {
        var boxes = _presenter.GetBoxes();
        if (boxes == null)
            return;
        var textBoxesCoeff = JoinTextBox();
        _presenter.BoxMeneger.SaveCoeff(textBoxesCoeff);

        for (var i = 0; i < boxes.Count(); i++)
        {
            var boxnamber = boxes[i].BoxNumber;
            var textBoxsCoeff = textBoxesCoeff.First(f => f.Item1 == "Box" + boxnamber);
            textBoxsCoeff.Item2.Text = boxes[i].BoxCoeffsForward.CoeffK.ToString();
            textBoxsCoeff.Item3.Text = boxes[i].BoxCoeffsForward.CoeffB.ToString();
        }

        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;
    }

    private void btnReadDataFromFile_Click(object sender, EventArgs e)
    {
        var excelManager = new ExcelManager();
        var textBoxesCoeff = JoinTextBox();
        _presenter.ExcelManager.ShowCoeff(textBoxesCoeff);
    }

    private void btnReadCoeffForSensor_Click(object sender, EventArgs e)
    {
        var excelManager = new ExcelManager();
        var boxes = _presenter.GetBoxes();
        var rows = excelManager.LoadDataCaptureConfig().Rows;
        for (var i = 0; i < boxes.Count(); i++)
        {
            for (var j = 0; j < boxes[i].Channels.Count(); j++)
            {
                var boxIpPort = $"{boxes[i].IpAddress}:{boxes[i].Channels[j].Port}";

                var coeffs = new List<KvToTonneCoefficient>();

                foreach (DataRow row in rows)
                {
                    if (row.ItemArray[1].ToString() == boxIpPort)
                    {
                        var coeff = new KvToTonneCoefficient(Convert.ToInt32(row.ItemArray[2]), Convert.ToDouble(row.ItemArray[3]), boxIpPort);
                        coeffs.Add(coeff);
                    }
                }
                coeffs = KvToTonneCoefficient.AddMissingDefaltCoeffs(coeffs, boxes.First().ProtocolVer);

                boxes[i] = new BoxEndPoint()
                {
                    BoxNumber = boxes[i].BoxNumber,
                    LineNumber = boxes[i].LineNumber,
                    ProtocolVer = boxes[i].ProtocolVer,
                    DistanceBetweenSensorsMetre = boxes[i].DistanceBetweenSensorsMetre,
                    SensorsLocation = boxes[i].SensorsLocation,
                    IpAddress = boxes[i].IpAddress,
                    Channels = boxes[i].Channels,
                    BoxCoeffsForward = new BoxCoeffsConfig()
                    {
                        CoeffK = boxes[i].BoxCoeffsForward.CoeffK,
                        CoeffB = boxes[i].BoxCoeffsForward.CoeffB,
                        TemperatureCoeff = boxes[i].BoxCoeffsForward.TemperatureCoeff,
                        ReserveProp1 = boxes[i].BoxCoeffsForward.ReserveProp1,
                        ReserveProp2 = boxes[i].BoxCoeffsForward.ReserveProp2,
                        ReserveProp3 = boxes[i].BoxCoeffsForward.ReserveProp3,
                        SensorCoeffsKvToTonne = coeffs.Select(s => s.Coeff).ToArray()
                    },
                };
            }
        }
        if (_presenter.TryGetUpdateConfig(out var fileContent))
            richTextBox.Text = fileContent;
        else
            textBoxInfoError.Text = fileContent;
    }
}
