using OrmTesterLib.StatisticParametersCalculator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace OrmTesterLib.IOService
{
    public class OrmTesterIOService
    {
        private const string TestDirectoryName = "Test Results";
        private string CurrentDayTestDirectoryPattern = "Test Results {0}";
        private string CurrentDayTestFilePattern = "TestResult_{0}.dat";
        private readonly ResourceManager resourceManager;
        private BinaryFormatter binaryFormatter;

        public OrmTesterIOService()
        {
            resourceManager = new ResourceManager(typeof(Resources.Resources));
            binaryFormatter = new BinaryFormatter();
        }

        public void SaveTestToFile(string path, List<StatisticParameter> statisticParameters)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException(resourceManager.GetString("HomeDirectoryPathError") + " Path: " + path);

            string testDirectoryPath = path + "\\" + TestDirectoryName;
            string currentDayTestDirectory = testDirectoryPath + "\\" + string.Format(CurrentDayTestDirectoryPattern, DateTime.Now.ToString("dd-MM-yyyy"));

            Directory.CreateDirectory(currentDayTestDirectory);
            string filePath = testDirectoryPath + "\\" + string.Format(CurrentDayTestFilePattern, DateTime.Now.ToString("dd_MM_yyyy_HH_mm"));

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
            using(FileStream fs = new FileStream(path, FileMode.Open))
            {
                try
                {
                    statisticParameters = binaryFormatter.Deserialize(fs) as List<StatisticParameter>;
                }
                catch(Exception ex)
                {
                    throw new Exception($"Deserialization exception: {ex.Message}");
                }
            }

            return statisticParameters;
        }
    }
}
