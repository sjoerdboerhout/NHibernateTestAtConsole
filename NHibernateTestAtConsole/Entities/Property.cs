using System;
using System.Collections.Generic;
using System.Linq;
using NHibernateTestAtConsole.Model;

namespace NHibernateTestAtConsole.Entities
{
  public class Property : Entity<int>,IPropertyValue
  {
    private string _currVal = "";

    public virtual Guid Guid { get; protected set; }

    public virtual string Name { get; set; }

    public virtual DateTime LastModified { get; set; } = DateTime.Now;

    public virtual ICollection<PropertyValue> Values { get; set; } = new List<PropertyValue>();

    public virtual string Value
    {
      get
      {
        //if (_currVal.Length > 0)
        //  return _currVal;

        if (Values.Any())
          return Values.OrderByDescending(s => s.LastModified).ThenBy(t => t.Revision).First().Value;

        return "";
      }

      set
      {
        //Values.Add(new PropertyValue()
        //{
        //  // Gives null pointer if this is the first value!
        //  Parent = Values.OrderByDescending(s => s.LastModified).First().Parent,
        //  Property = this,
        //  Value = value
        //});
        // Update to add a new propertyvalue if this is the first value
        if (Values.Any())
        {
          Values.OrderByDescending(s => s.LastModified).ThenBy(t => t.Revision).First().Value = value;
        }
      }
    }

    public virtual void AddValue(PropertyValue propertyValue)
    {
      propertyValue.Property = this;
      Values?.Add(propertyValue);
    }

    public virtual ICollection<User> Users { get; set; } = new List<User>();


    public override string ToString()
    {
      string values = "";
      foreach (var value in Values.OrderByDescending(s => s.LastModified).ThenBy(t => t.Revision))
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
