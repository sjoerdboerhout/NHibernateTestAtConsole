using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestAtConsole.Entities
{
  public class Property
  {
    public virtual Guid Guid { get; protected set; }

    public virtual string Name { get; set; }

    public virtual DateTime LastModified { get; set; }

    public virtual IList<PropertyValue> Values { get; set; } = new List<PropertyValue>();

    public virtual string Value
    {
      get
      {
        if (Values?.Count > 0)
          return Values.OrderBy(s => s.LastModified).First().Value;

        return "";
      }
    }

    public virtual void AddValue(PropertyValue propertyValue)
    {
      Values?.Add(propertyValue);
    }

    public override string ToString()
    {
      string values = "";
      foreach (var value in Values)
      {
        values += "-- " + value + "\n";
      }

      values = values.Trim();

      return string.Format("\n-UUID: {0}\n-Name: {1}\n-Value: {2}\n-Value count: {3}\n-Values: \n{4}\n-Last modified: {5}\n",
        Guid,
        Name,
        Value,
        Values.Count,
        values,
        LastModified);
    }
  }
}
