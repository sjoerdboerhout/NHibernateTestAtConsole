using System;
using System.Linq;
using System.Threading;
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
              LastName = "Boerhout"
            };
            session.SaveOrUpdate(user);
            _user = user;
            Console.WriteLine("Save user: " + user);

            Property property = new Property()
            {
              Name = "X"
            };
            property.AddValue(new PropertyValue()
            {
              Parent = _user,
              Value = "Y"
            });
            property.AddValue(new PropertyValue()
            {
              Parent = _user,
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

            Property property = (from p in session.Query<Property>()
                where p.Name == "X" select p).FirstOrDefault();
            Thread.Sleep(1000);

            if (property != null)
            {
              property.AddValue(new PropertyValue()
              {
                Parent = _user,
                Value = "ZZ"
              });

              session.SaveOrUpdate(property);
              Thread.Sleep(1000);

              //update value must be in different transaction
              //else no new audit entry is created
              property.Values.First().Value = "test";
              session.SaveOrUpdate(property);

              Console.WriteLine("Update property: " + property);
            }

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
