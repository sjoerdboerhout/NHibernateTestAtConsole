﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public static class NHibernateHelper
  {
    private static ISessionFactory _sessionFactory;
    private static Configuration _configuration;
    private static HbmMapping _mapping;
    private static PropertyEventListener _propertyEventListener = new PropertyEventListener();

    public static ISession OpenSession()
    {
      InitializeAudit();
      //Open and return the nhibernate session
      return SessionFactory.OpenSession();
    }

    public static ISessionFactory SessionFactory
    {
      get
      {
        if (_sessionFactory == null)
        {
          //Create the session factory
          _sessionFactory = Configuration.BuildSessionFactory();

          new SchemaExport(_configuration)
          .Create(true, true);
        }
        return _sessionFactory;
      }
    }

    public static Configuration Configuration
    {
      get
      {
        if (_configuration == null)
        {
          //Create the nhibernate configuration
          _configuration = CreateConfiguration();

          _configuration.SetListener(ListenerType.PreInsert, _propertyEventListener);
          _configuration.SetListener(ListenerType.PreUpdate, _propertyEventListener);
        }

        return _configuration;
      }
    }

    public static HbmMapping Mapping
    {
      get
      {
        if (_mapping == null)
        {
          //Create the mapping
          _mapping = CreateMapping();
        }
        return _mapping;
      }
    }

    public static void InitializeAudit()
    {
      var enversConf = new NHibernate.Envers.Configuration.Fluent.FluentConfiguration();

      enversConf.Audit<Property>();
      enversConf.Audit<User>();
      enversConf.Audit<PropertyValue>();

      Configuration.IntegrateWithEnvers(enversConf);
    }

    private static Configuration CreateConfiguration()
    {
     
      var configuration = new Configuration();

      //Loads properties from hibernate.cfg.xml
      configuration.Configure();

      //Loads nhibernate mappings 
      configuration.AddDeserializedMapping(Mapping, null);

      return configuration;
    }

    private static HbmMapping CreateMapping()
    {
      var mapper = new ModelMapper();

      //Add the person mapping to the model mapper
      mapper.AddMappings(new List<System.Type> {typeof(UserMap)});
      mapper.AddMappings(new List<System.Type> { typeof(PropertyMap) });
      mapper.AddMappings(new List<System.Type> { typeof(PropertyValueMap) });

      //Create and return a HbmMapping of the model mapping in code
      return mapper.CompileMappingForAllExplicitlyAddedEntities();
    }
  }
}