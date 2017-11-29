using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NHibernateTestAtConsole.DAO;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole
{
  class Program
  {
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

            User user = new User()
            {
              FirstName = "Sjoerd",
              LastName = "Boerhout",
              LastModified = DateTime.Now
            };
            session.SaveOrUpdate(user);
            Console.WriteLine("Save user: " + user);

            Property property = new Property()
            {
              Name = "X",
              LastModified = DateTime.Now
            };
            property.AddValue(new PropertyValue()
            {
              Parent = property.Guid,
              Property = property,
              LastModified = DateTime.Now.AddMinutes(-1),
              Value = "Y"
            });
            property.AddValue(new PropertyValue()
            {
              Parent = property.Guid,
              Property = property,
              LastModified = DateTime.Now,
              Value = "Z"
            });

            session.SaveOrUpdate(property);
            Console.WriteLine("Save property: " + property);

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
  }
}
