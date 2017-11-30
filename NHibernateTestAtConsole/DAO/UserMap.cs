using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public class UserMap : ClassMapping<User>
  {
    public UserMap()
    {
      DynamicUpdate(true);
      Table("user");

      Id(x => x.Guid, m =>
      {
        //m.Type(IIdentifierType);
        m.Generator(Generators.GuidComb);
        m.Column("guid");
      });

      //Property(x => x.FirstName, y => y.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1) );
      Property(x => x.FirstName, m =>
      {
        m.Length(SqlClientDriver.MaxSizeForLengthLimitedString + 1);
        m.Column("username");
        //m.NotNullable(true);
      });

      Property(x => x.LastName);

      Property(x => x.LastModified);

      Bag(t => t.Properties, bag =>
        {
          bag.Table("user_properties");
          bag.Cascade(Cascade.None);
          bag.Lazy(CollectionLazy.NoLazy);
          bag.Key(k => k.Column("user_id"));
        },
        t => t.ManyToMany(c => { c.Column("property_id"); }));
    }
  }
}