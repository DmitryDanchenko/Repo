using System.Data;
using ClosedXML.Excel;

namespace ConfigHelper.Models;

public class ExcelManager
{
    public DataTable LoadDataCaptureConfig()
    {
        var filePath = string.Empty;
        var fileContent = new DataTable();

        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "(*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;

                var workbook = new XLWorkbook(filePath);
                var xlWorksheet = workbook.Worksheet(1);
                var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

                var col = range.ColumnCount();
                var row = range.RowCount();

                fileContent.Clear();
                for (var i = 1; i <= col; i++)
                {
                    var column = xlWorksheet.Cell(1, i);
                    fileContent.Columns.Add(column.Value.ToString());
                }

                var firstHeadRow = 0;
                foreach (var item in range.Rows())
                {
                    if (firstHeadRow != 0)
                    {
                        var array = new object[col];
                        for (var y = 1; y <= col; y++)
                        {
                            array[y - 1] = item.Cell(y).Value;
                        }
                        fileContent.Rows.Add(array);
                    }
                    firstHeadRow++;
                }
            }
        }
        return fileContent;
    }

    public void ShowCoeff(List<(string, TextBox, TextBox)> textBoxesCoeff)
    {
        var excelManager = new ExcelManager();
        foreach (DataRow row in excelManager.LoadDataCaptureConfig().Rows)
        {
            var textBoxsCoeff = textBoxesCoeff.First(f => f.Item1 == "Box" + row.ItemArray[1]);
            if (row.ItemArray[3].ToString() != "")
                textBoxsCoeff.Item2.Text = row.ItemArray[3].ToString();

            if (row.ItemArray[4].ToString() != "")
                textBoxsCoeff.Item3.Text = row.ItemArray[4].ToString();
        }
    }
}
