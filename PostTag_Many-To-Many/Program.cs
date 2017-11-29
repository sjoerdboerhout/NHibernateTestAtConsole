using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace PostTag_Many_To_Many
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

            Tag tag1 = new Tag { Name = "Tag 1" };
            Tag tag2 = new Tag { Name = "Tag 2" };
            Tag tag3 = new Tag { Name = "Tag 3" };
            Tag tag4 = new Tag { Name = "Tag 4" };
            Tag tag5 = new Tag { Name = "Tag 5" };
            session.Save(tag1);
            session.Save(tag2);
            session.Save(tag3);
            session.Save(tag4);
            session.Save(tag5);

            Post postA = new Post { Name = "Post A" };
            postA.Tags.Add(tag1);
            postA.Tags.Add(tag3);
            postA.Tags.Add(tag5);
            session.Save(postA);

            Post postB = new Post { Name = "Post B" };
            postB.Tags.Add(tag2);
            postB.Tags.Add(tag4);
            postB.Tags.Add(tag5);
            session.Save(postB);
            transaction.Commit();

            Console.WriteLine("Transaction completed.");
          }

          session.Flush();

          using (var transaction = session.BeginTransaction())
          {
            var posts = (from post in session.Query<Post>()
                         select post)
              .OrderBy(x => x.Id).ToList();

            foreach (var post in posts)
            {
              Console.WriteLine("Post: {0}", post);
            }

            var tags = (from tag in session.Query<Tag>()
                              select tag)
              .OrderBy(x => x.Name).ToList();
            foreach (var tag in tags)
            {
              Console.WriteLine("Tag: {0}", tag);
            }

          }
        }
      }

      Console.ReadKey();
    }
  }
}