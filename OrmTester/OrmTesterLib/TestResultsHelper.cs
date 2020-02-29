using OrmTesterLib.Enums;
using OrmTesterLib.TestCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace OrmTesterLib
{
    static class TestResultsHelper
    {
        private static ResourceManager resourceManager;
        private static CultureInfo cultureInfo;
        private const string IsBulkTestProperty = "IsBulkTest";
        private const string OperationTypeProperty = "OperationType";
        private const string RelationshipTypeProperty = "RelationshipType";

        static TestResultsHelper()
        {
            resourceManager = new ResourceManager(typeof(Resources.Resources));
        }

        public static List<Tuple<string, List<TestResult>>> GetGroupedTestResults(this List<TestResult> testResults, CultureInfo cultureInfo)
        {
            TestResultsHelper.cultureInfo = cultureInfo;
            return testResults
                    .GroupBy(tr => new
                    {
                        tr.IsBulkTest,
                        tr.OperationType,
                        tr.RelationshipType
                    })
                    .Select(group => new Tuple<string, List<TestResult>>(GetTestName(group.Key), group.ToList()))
                    .OrderBy(p => p.Item1)
                    .ToList();
        }

        private static string GetTestName(object key)
        {
            string testName = "";
            Type type = key.GetType();
            var isBulkTest = (bool)type.GetProperty(IsBulkTestProperty).GetValue(key);
            var operationType = (OperationType)type.GetProperty(OperationTypeProperty).GetValue(key);
            var relationshipType = (RelationshipType)type.GetProperty(RelationshipTypeProperty).GetValue(key);

            testName = isBulkTest ? resourceManager.GetString("Bulk", cultureInfo) : resourceManager.GetString("Single", cultureInfo);
            testName += " " + resourceManager.GetString(operationType.ToString(), cultureInfo) + " ";
            testName += resourceManager.GetString(relationshipType.ToString(), cultureInfo);

            return testName;
        }
    }
}
