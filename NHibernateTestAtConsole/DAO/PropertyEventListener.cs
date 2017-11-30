using System;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.DAO
{
  public class PropertyEventListener : IPreUpdateEventListener, IPreInsertEventListener
  {
    public bool OnPreInsert(PreInsertEvent @event)
    {
      var audit = @event.Entity as IPropertyValue;
      if (audit == null)
        return false;

      var time = DateTime.Now;

      Set(@event.Persister, @event.State, "LastModified", DateTime.Now);

      audit.LastModified = time;

      return false;
    }

    public bool OnPreUpdate(PreUpdateEvent @event)
    {
      var audit = @event.Entity as IPropertyValue;
      if (audit == null)
        return false;

      var time = DateTime.Now;
      
      Set(@event.Persister, @event.State, "LastModified", DateTime.Now);

      audit.LastModified = time;

      return false;
    }

    private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
    {
      var index = Array.IndexOf(persister.PropertyNames, propertyName);
      if (index == -1)
        return;
      state[index] = value;
    }
  }
}