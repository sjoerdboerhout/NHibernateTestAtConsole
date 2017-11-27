using System;
using NHibernate.Tool.hbm2ddl;
using NHibernateTestAtConsole.DAO;
using NUnit.Framework;

namespace NHibernateTestAtConsole.Test
{
  [TestFixture]
  public class SchemaTest
  {
    [Test]
    public void CanGenerateSchema()
    {
      var schemaUpdate = new SchemaUpdate(NHibernateHelper.Configuration);
      schemaUpdate.Execute(Console.WriteLine, true);
    }
  }
}
