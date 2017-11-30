using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NHibernateTestAtConsole.Entities;
using Property = NHibernateTestAtConsole.Entities.Property;

namespace NHibernateTestAtConsole.DAO
{
  public class PropertyMap : ClassMapping<Property>
  {
    public PropertyMap()
    {
      Table("property");

      Id(x => x.Guid, map =>
      {
        map.Generator(Generators.GuidComb);
        map.Column("guid");
      });

      Property(x => x.Name, map =>
      {
        map.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1);
        map.Column("name");
      });

      Property(x => x.LastModified, map =>
      {
        map.Column("lastmodified");
      });

      Property(x => x.Value, map =>
      {
        map.Column("current_value");
      });

      Set(x => x.Values, c =>
        {
          c.Key(k => { k.Column("property_id"); });
          c.Cascade(Cascade.Persist);
          c.Lazy(CollectionLazy.Lazy);
          c.OrderBy("LastModified desc");
        }, r => { r.OneToMany(); }
      );

      Bag(t => t.Users, bag =>
        {
          bag.Table("user_properties");
          bag.Cascade(Cascade.None);
          bag.Lazy(CollectionLazy.NoLazy);
          bag.Key(k => k.Column("property_id"));
        },
        t => t.ManyToMany(c => { c.Column("user_id"); }));
    }
  }
}