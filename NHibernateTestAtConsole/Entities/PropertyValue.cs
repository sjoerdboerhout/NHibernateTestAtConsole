using System;
using NHibernate.Envers.Configuration.Attributes;
using NHibernateTestAtConsole.Model;

namespace NHibernateTestAtConsole.Entities
{
  public class PropertyValue : Entity<int>,IPropertyValue
  {
    public virtual Guid Guid { get; protected set; }

    public virtual Property Property { get; set; }

    public virtual User Parent { get; set; }

    public virtual string Value { get; set; }

    public virtual DateTime LastModified { get; set; } = DateTime.Now;

    public virtual int Revision { get; set; }

    public override string ToString()
    {
      return string.Format("PropertyValue: UUID: {0}, Property uuid: {1}, Parent: {2}, Value: {3}, Last modified: {4}, Revision: {5}",
        Guid,
        Property.Guid,
        Parent.Guid,
        Value,
        LastModified,
        Revision);
    }
  }
}
