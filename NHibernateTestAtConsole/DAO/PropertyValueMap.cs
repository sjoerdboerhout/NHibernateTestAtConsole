﻿using NHibernate;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public class PropertyValueMap : ClassMapping<PropertyValue>
  {
    public PropertyValueMap()
    {
      Table("property_value");

      OptimisticLock(OptimisticLockMode.Version);

      Id(x => x.Guid, map =>
      {
        map.Generator(Generators.GuidComb);
        map.Column("guid");
      });

      //Property(x => x.Parent);
      ManyToOne(x => x.Parent, map =>
      {
        map.Column("parent_id");
        map.NotNullable(true);
        map.Cascade(Cascade.None);
      });

      Property(x => x.LastModified, map =>
      {
        map.Column("lastmodified");
      });

      ManyToOne(x => x.Property, map =>
      {
        map.Column("property_id");
        map.NotNullable(true);
        map.Cascade(Cascade.None);
      });

      Property(x => x.Value, map =>
      {
        map.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1);
        map.Column("value");
        //m.NotNullable(true);
      });

      Version(x => x.Revision, map =>
      {        
        map.Type(NHibernateUtil.Int32);
        map.Column("revision");
      });
    }
  }
}