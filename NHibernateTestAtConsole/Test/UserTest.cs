using System;
using NHibernateTestAtConsole.Entities;
using NUnit.Framework;

namespace NHibernateTestAtConsole.Test
{
  public class UserTest
  {
    [TestFixture]
    public class PersonTest
    {
      [Test]
      public void GetFullNameTest()
      {
        var user = new User
        {
          FirstName = "Test",
          LastName = "Kees"
        };

        Assert.AreEqual("Test", user.FirstName);
        Assert.AreEqual("Kees", user.LastName);
      }
    }
  }
}