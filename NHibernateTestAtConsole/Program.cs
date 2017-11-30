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
      User _user;

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
            _user = user;
            Console.WriteLine("Save user: " + user);

            Property property = new Property()
            {
              Name = "X",
              LastModified = DateTime.Now
            };
            property.AddValue(new PropertyValue()
            {
              Parent = user,
              Property = property,
              LastModified = DateTime.Now.AddMinutes(-1),
              Value = "Y"
            });
            property.AddValue(new PropertyValue()
            {
              Parent = user,
              Property = property,
              LastModified = DateTime.Now,
              Value = "Z"
            });

            user.Properties.Add(property);
            session.SaveOrUpdate(property);
            Console.WriteLine("Save property: " + property);

            transaction.Commit();
            Console.WriteLine("Transaction completed.");
          }

          session.Flush();

          using (ITransaction transaction = session.BeginTransaction())
          {
            Console.WriteLine("Ready to update a value!");

            Property property = (from prop in session.Query<Property>()
                              select prop)
              .First(x => x.Name.Equals("X"));

            property.AddValue(new PropertyValue()
            {
              Parent = _user,
              Property = property,
              LastModified = DateTime.Now,
              Value = "ZZ"
            });
            
            session.SaveOrUpdate(property);
            Console.WriteLine("Update property: " + property);

            transaction.Commit();
            Console.WriteLine("Transaction completed.");
          }



          using (var transaction = session.BeginTransaction())
          {
            var users = (from user in session.Query<User>()
                         select user)
              .OrderBy(x => x.LastModified).ToList();

            foreach (var user in users)
            {
              Console.WriteLine("User: {0}\n\n", user);
            }

            var properties = (from property in session.Query<Property>()
                              select property)
              .OrderBy(x => x.Name).ToList();
            foreach (var property in properties)
            {
              Console.WriteLine("Property: {0}\n\n", property);
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
