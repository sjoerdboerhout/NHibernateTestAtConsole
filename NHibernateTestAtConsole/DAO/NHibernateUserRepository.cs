using System;
using NHibernate;
using NHibernateTestAtConsole.Entities;
using NHibernateTestAtConsole.Model;

namespace NHibernateTestAtConsole.DAO
{
  public class NHibernatePersonRepository : IUserRepository
  {
    public void Save(User person)
    {
      using (ISession session = NHibernateHelper.OpenSession())
      using (ITransaction transaction = session.BeginTransaction())
      {
        session.Save(person);
        transaction.Commit();
      }
    }

    public User Get(Guid id)
    {
      using (ISession session = NHibernateHelper.OpenSession())
        return session.Get<User>(id);
    }

    public void Update(User person)
    {
      using (ISession session = NHibernateHelper.OpenSession())
      using (ITransaction transaction = session.BeginTransaction())
      {
        session.Update(person);
        transaction.Commit();
      }
    }

    public void Delete(User person)
    {
      using (ISession session = NHibernateHelper.OpenSession())
      using (ITransaction transaction = session.BeginTransaction())
      {
        session.Delete(person);
        transaction.Commit();
      }
    }

    public long RowCount()
    {
      using (ISession session = NHibernateHelper.OpenSession())
      {
        return session.QueryOver<User>().RowCountInt64();
      }
    }
  }
}
