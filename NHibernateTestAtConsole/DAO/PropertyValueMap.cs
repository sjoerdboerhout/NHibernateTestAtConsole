using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public class PropertyValueMap : ClassMapping<PropertyValue>
  {
    public PropertyValueMap()
    {
      Id(x => x.Guid, map =>
      {
        map.Generator(Generators.GuidComb);
        map.Column("guid");
      });

      Property(x => x.Parent);

      Property(x => x.LastModified);

      ManyToOne(x => x.Property, map =>
      {
        map.Column("property_id");
        //map.Class(typeof(Property));
        map.NotNullable(true);
        map.Cascade(Cascade.None);
      });


      Property(x => x.Value, map =>
      {
        map.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1);
        map.Column("value");
        //m.NotNullable(true);
      });

    }
  }
}