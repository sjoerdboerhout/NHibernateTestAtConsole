using System;

namespace NHibernateTestAtConsole.Model
{
  public abstract class Entity<TId>
  {
    public virtual TId Id { get; protected set; }

    public override bool Equals(object obj)
    {
      return Equals(obj as Entity<TId>);
    }

    public virtual bool IsTransient
    {
      get
      {
        return ((Id == null) || Id.Equals(default(TId)));
      }
    }

    private Type GetUnproxiedType()
    {
      return GetType();
    }

    public virtual bool Equals(Entity<TId> other)
    {
      if (ReferenceEquals(this, other))
      {
        return true;
      }

      if ((other == null) || !GetType().Equals(other.GetUnproxiedType()))
        return false;

      // if both objects have ID setted check the Ids
      if ((!IsTransient) &&
          (!other.IsTransient) &&
          (Id.Equals(other.Id)))
      {
        return true;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return Equals(Id, default(TId)) ? base.GetHashCode() : Id.GetHashCode();
    }
  }
}
