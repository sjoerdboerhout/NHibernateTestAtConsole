using System;
using System.Collections.Generic;
using System.Linq;

namespace NHibernateTestAtConsole.Entities
{
  public class Property
  {
    private string _currVal = "";

    public virtual Guid Guid { get; protected set; }

    public virtual string Name { get; set; }

    public virtual DateTime LastModified { get; set; }

    public virtual ICollection<PropertyValue> Values { get; set; } = new List<PropertyValue>();

    public virtual string Value
    {
      get
      {
        if (_currVal.Length > 0)
          return _currVal;

        if (Values?.Count > 0)
          return Values.OrderByDescending(s => s.LastModified).First().Value;

        return "";
      }

      set { _currVal = value; }
    }

    public virtual void AddValue(PropertyValue propertyValue)
    {
      Values?.Add(propertyValue);
    }

    public virtual ICollection<User> Users { get; set; } = new List<User>();


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
