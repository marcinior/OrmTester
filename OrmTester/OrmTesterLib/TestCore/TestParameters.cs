using System;

namespace OrmTesterLib.TestCore
{
    public class TestParameters
    {
        public Tuple<bool, int> SingleCreateNoRelationship { get; set; }

        public Tuple<bool, int> SingleCreateOneToOne { get; set; }

        public Tuple<bool, int> SingleCreateOneToMany { get; set; }

        public Tuple<bool, int> SingleCreateManyToMany { get; set; }

        public Tuple<bool, int> SingleUpdateNoRelationship { get; set; }

        public Tuple<bool, int> SingleUpdateOneToOne { get; set; }

        public Tuple<bool, int> SingleUpdateOneToMany { get; set; }

        public Tuple<bool, int> SingleUpdateManyToMany { get; set; }

        public Tuple<bool, int> SingleDeleteNoRelationship { get; set; }

        public Tuple<bool, int> SingleDeleteOneToOne { get; set; }

        public Tuple<bool, int> SingleDeleteOneToMany { get; set; }

        public Tuple<bool, int> SingleDeleteManyToMany { get; set; }

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
