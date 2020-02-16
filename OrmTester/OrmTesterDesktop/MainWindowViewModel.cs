using OrmTesterLib.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OrmTesterDesktop
{
    //TODO Ask if we will generate data for every iteration or just for realtion and multiply of tests (is bulk or single)
    public class MainWindowViewModel
    {
        public bool IsBulk { get; set; }

        public double EFResults { get; set; }

        public double NHibernateResult { get; set; }

        public double Difference { get; set; }

        public string TestedRelationshipType { get; private set; }

        public void SetRelationshipType(RelationshipType type)
        {
            switch (type)
            {
                case RelationshipType.None:
                    this.TestedRelationshipType = Properties.Resources.None;
                    break;
                case RelationshipType.ManyToMany:
                    this.TestedRelationshipType = Properties.Resources.ManyToMany;
                    break;
                case RelationshipType.OneToMany:
                    this.TestedRelationshipType = Properties.Resources.OneToMany;
                    break;
                case RelationshipType.OneToOne:
                    this.TestedRelationshipType = Properties.Resources.OneToOne;
                    break;
            }
        }
    }
}
