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
    }
  }
}