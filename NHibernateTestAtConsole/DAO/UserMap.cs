using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public class UserMap : ClassMapping<User>
  {
    public UserMap()
    {
      Id(x => x.Guid, m => m.Generator(Generators.GuidComb));
      Property(x => x.FirstName);
      Property(x => x.LastName);
    }
  }
}