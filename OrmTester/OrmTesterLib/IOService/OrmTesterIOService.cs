using OfficeOpenXml;
using OrmTesterLib.StatisticParametersCalculator;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;

namespace OrmTesterLib.IOService
{
    public class OrmTesterIOService
    {
        private const string TestDirectoryName = "Test Results";
        private string CurrentDayTestDirectoryPattern = "Test Results {0}";
        private string CurrentDayTestFilePattern = "TestResult_{0}.dat";
        private const string MissingTestResultsError = "MissingTestResultsError";
        private const string DifferentCountOfGroupsError = "DifferentCountOfGroupsError";
        private readonly string efWorksheetHeader = "Entity Framework execution time [ms]";
        private readonly string nhWorksheetHeader = "nHibernate execution time [ms]";
        private readonly ResourceManager resourceManager;
        private BinaryFormatter binaryFormatter;
        private readonly CultureInfo cultureInfo;

        public OrmTesterIOService(CultureInfo cultureInfo)
        {
            resourceManager = new ResourceManager(typeof(Resources.Resources));
            binaryFormatter = new BinaryFormatter();
            this.cultureInfo = cultureInfo;
        }

        public void SaveTestToFile(string path, List<StatisticParameter> statisticParameters)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException(resourceManager.GetString("HomeDirectoryPathError") + " Path: " + path);

            string testDirectoryPath = path + "\\" + TestDirectoryName;
            string currentDayTestDirectory = testDirectoryPath + "\\" + string.Format(CurrentDayTestDirectoryPattern, DateTime.Now.ToString("dd-MM-yyyy"));

            Directory.CreateDirectory(currentDayTestDirectory);
            string filePath = currentDayTestDirectory + "\\" + string.Format(CurrentDayTestFilePattern, DateTime.Now.ToString("dd_MM_yyyy_HH_mm"));

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                try
                {
                    binaryFormatter.Serialize(fs, statisticParameters);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Serialization exception: {ex.Message}");
                }
            }
        }

        public List<StatisticParameter> LoadTestFromFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException(resourceManager.GetString("FileNotExistError") + " Path: " + path);

            List<StatisticParameter> statisticParameters = new List<StatisticParameter>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                try
                {
                    statisticParameters = binaryFormatter.Deserialize(fs) as List<StatisticParameter>;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Deserialization exception: {ex.Message}");
                }
            }

            return statisticParameters;
        }

        public void SaveTestToExcel(string path, List<TestResult> efTestResults, List<TestResult> nHibernateTestResults)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                if (efTestResults == null || nHibernateTestResults == null || efTestResults.Count == 0 || nHibernateTestResults.Count == 0)
                    throw new ArgumentNullException(resourceManager.GetString(MissingTestResultsError, cultureInfo));

                var efGroupedResults = efTestResults.GetGroupedTestResults(cultureInfo);
                var nHiberanateGroupedResults = nHibernateTestResults.GetGroupedTestResults(cultureInfo);

                if (efGroupedResults.Count != nHiberanateGroupedResults.Count)
                    throw new ArgumentException(resourceManager.GetString(DifferentCountOfGroupsError, cultureInfo));

                for (int i = 0; i < efGroupedResults.Count; i++)
                {
                    var worksheet = excel.Workbook.Worksheets.Add(efGroupedResults[i].Item1);
                    worksheet.Cells["A1"].Value = efWorksheetHeader;
                    worksheet.Cells["A1"].Style.Font.Bold = true;
                    worksheet.Cells["B1"].Value = nhWorksheetHeader;
                    worksheet.Cells["B1"].Style.Font.Bold = true;
                    worksheet.Cells["A2"].LoadFromCollection(efGroupedResults[i].Item2.Select(tr => tr.ExecutionTime.TotalMilliseconds.ToString()));
                    worksheet.Cells["B2"].LoadFromCollection(nHiberanateGroupedResults[i].Item2.Select(tr => tr.ExecutionTime.TotalMilliseconds.ToString()));
                    worksheet.Cells.AutoFitColumns();
                }

                if (File.Exists(path))
                    path = path.Insert(path.Length - 5, $"_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm")}");

                FileInfo fileInfo = new FileInfo(path);
                excel.SaveAs(fileInfo);
            }
        }
    }
}
