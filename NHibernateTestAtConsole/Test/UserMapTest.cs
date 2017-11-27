using System;
using System.Xml.Serialization;
using NHibernate.Mapping.ByCode;
using NHibernateTestAtConsole.DAO;
using NUnit.Framework;

namespace NHibernateTestAtConsole.Test
{
  [TestFixture]
  public class PersonMapTest
  {
    [Test]
    public void CanGenerateXmlMapping()
    {
      var mapper = new ModelMapper();
      mapper.AddMapping<UserMap>();

      var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
      var xmlSerializer = new XmlSerializer(mapping.GetType());

      xmlSerializer.Serialize(Console.Out, mapping);
    }
  }
}