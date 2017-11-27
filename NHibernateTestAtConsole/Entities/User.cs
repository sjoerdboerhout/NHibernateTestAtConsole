using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestAtConsole.Entities
{
  public class User
  {
    public virtual Guid Guid { get; set; }

    public virtual String FirstName { get; set; }

    public virtual String LastName { get; set; }

    public virtual DateTime LastModified { get; set; }

    //public virtual List<Property> Properties { get; set; } = new List<Property>();

    //public virtual Property AddProperty(Property property)
    //{
    //  Properties.Add(property);
    //  return property;
    //}

    //public override string ToString()
    //{
    //  return string.Format("\n-UUID: {0}\n-Last modified: {1}\n-Nr of properties: {2}\n",
    //                        Guid,
    //                        LastModified,
    //                        Properties.Count);
    //}

    public override string ToString()
    {
      return string.Format("\n-UUID: {0}\n-Name: {1} {2}\n-Last modified: {3}\n",
        Guid,
        FirstName,
        LastName,
        LastModified);
    }
  }
}