using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernateTestAtConsole.DAO;

namespace PersonCar_Set_ManyToOne
{
  class Program
  {
    private const string DatabaseFilePath = @"database.db";

    static void Main(string[] args)
    {
      using (ISession session = NHibernateHelper.OpenSession())
      {
        if (!session.Transaction.IsActive)
        {
          // populate the database
          using (ITransaction transaction = session.BeginTransaction())
          {
            Console.WriteLine("Ready to execute a query!");

            PersonSet John = new PersonSet { Name = "John" };
            John.Cars = new List<CarSet> {
                new CarSet { Name = "BMW",Person = John},
                  new CarSet { Name = "BM" ,Person = John }};
            session.Save(John);
            transaction.Commit();

            Console.WriteLine("Transaction completed.");
          }

          session.Flush();
        }
      }

      Console.ReadKey();
    }
  }
}