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
      DynamicUpdate(true);
      Table("property");

      Id(x => x.Guid, m =>
      {
        m.Generator(Generators.GuidComb);
        m.Column("guid");
      });

      Property(x => x.Name, m =>
      {
        m.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1);
        m.Column("name");
        //m.NotNullable(true);
      });

      Property(x => x.LastModified);

      Property(x => x.Value);

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

      //Bag(x => x.Users, collectionMapping =>
      //{
      //  collectionMapping.Table("user_properties");
      //  collectionMapping.Cascade(Cascade.None);
      //  collectionMapping.Key(k => k.Column("user_id"));
      //},
      //          map => map.ManyToMany(p => p.Column("property_id")));
    }
  }
}