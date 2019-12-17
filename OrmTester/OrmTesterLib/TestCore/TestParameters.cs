using System;

namespace OrmTesterLib.TestCore
{
    public class TestParameters
    {
        public bool SingleCreateNoRelationship { get; set; }

        public bool SingleCreateOneToOne { get; set; }

        public bool SingleCreateOneToMany { get; set; }

        public bool SingleCreateManyToMany { get; set; }

        public bool SingleUpdateNoRelationship { get; set; }

        public bool SingleUpdateOneToOne { get; set; }

        public bool SingleUpdateOneToMany { get; set; }

        public bool SingleUpdateManyToMany { get; set; }

        public bool SingleDeleteNoRelationship { get; set; }

        public bool SingleDeleteOneToOne { get; set; }

        public bool SingleDeleteOneToMany { get; set; }

        public bool SingleDeleteManyToMany { get; set; }

        public Tuple<bool, int> BulkCreateNoRelationship { get; set; }

        public Tuple<bool, int> BulkCreateOneToOne { get; set; }

        public Tuple<bool, int> BulkCreateOneToMany { get; set; }

        public Tuple<bool, int> BulkCreateManyToMany { get; set; }

        public Tuple<bool, int> BulkUpdateNoRelationship { get; set; }

        public Tuple<bool, int> BulkUpdateOneToOne { get; set; }

        public Tuple<bool, int> BulkUpdateOneToMany { get; set; }

        public Tuple<bool, int> BulkUpdateManyToMany { get; set; }

        public Tuple<bool, int> BulkDeleteNoRelationship { get; set; }

        public Tuple<bool, int> BulkDeleteOneToOne { get; set; }

        public Tuple<bool, int> BulkDeleteOneToMany { get; set; }

        public Tuple<bool, int> BulkDeleteManyToMany { get; set; }
    }
}
