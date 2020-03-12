using OrmTesterLib.Enums;
using System;

namespace OrmTesterLib.TestCore
{
    public class TestResult
    {
        public TimeSpan ExecutionTime { get; set; }

        public bool IsBulkTest { get; set; }

        public OperationType OperationType { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public int NumberOfRecords { get; set; }
    }
}
