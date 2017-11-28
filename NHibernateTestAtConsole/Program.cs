using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateTestAtConsole.DAO;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole
{
  class Program
  {
    private const string DatabaseFilePath = @"database.db";

    static void Main(string[] args)
    {
      //List<User> Users = new List<User>();

      using (ISession session = NHibernateHelper.OpenSession())
      {
        if (!session.Transaction.IsActive)
        {
          // populate the database
          using (ITransaction transaction = session.BeginTransaction())
          {
            Console.WriteLine("Ready to execute a query!");

            Property property = new Property()
            {
              Name = "X",
              LastModified = DateTime.Now
            };
            property.AddValue(new PropertyValue()
            {
              Parent = property.Guid,
              Property = property.Guid,
              LastModified = DateTime.Now.AddMinutes(-1),
              Value = "Y"
            });
            property.AddValue(new PropertyValue()
            {
              Parent = property.Guid,
              Property = property.Guid,
              LastModified = DateTime.Now,
              Value = "Z"
            });
            //session.SaveOrUpdate(property);
            //Console.WriteLine("Save property: " + property);

            User user = new User()
            {
              FirstName = "Sjoerd",
              LastName = "Boerhout",
              LastModified = DateTime.Now
            };

            session.SaveOrUpdate(user);
            Console.WriteLine("Save user: " + user);


            transaction.Commit();
            Console.WriteLine("Transaction completed.");
          }

          session.Flush();

          using (var transaction = session.BeginTransaction())
          {
            var users = (from user in session.Query<User>()
                         select user)
              .OrderBy(x => x.LastModified).ToList();

            foreach (var user in users)
            {
              Console.WriteLine("User: {0}", user);
            }

            var properties = (from property in session.Query<Property>()
                              select property)
              .OrderBy(x => x.Name).ToList();
            foreach (var property in properties)
            {
              Console.WriteLine("Property: {0}", property);
            }

          }
        }
        else
        {
          Console.WriteLine("a transaction is already active... ");
        }

        Console.ReadKey();
      }
    }


    private static ISessionFactory CreateSessionFactory(string databasePath)
    {
      return null;
      /*
      return Fluently.Configure()
          .Database(SQLiteConfiguration.Standard
              .UsingFile(databasePath)
              .ShowSql)
          .Mappings(m =>
          m.FluentMappings.
            AddFromAssembly(Assembly.GetExecutingAssembly()))

        .ExposeConfiguration(BuildSchema)
        .BuildSessionFactory();
      */
    }

    private static void BuildSchema(Configuration config)
    {
      try
      {
        if (File.Exists(DatabaseFilePath))
          File.Delete(DatabaseFilePath);

        //new SchemaUpdate(config)
        //  .Execute(true, true);

        // this NHibernate tool takes a configuration (with mapping info in)
        // and exports a database schema from it
        new SchemaExport(config)
          .Create(true, true);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
      }
    }
  }
}
