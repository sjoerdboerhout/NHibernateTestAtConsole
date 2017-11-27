using System;
using NHibernateTestAtConsole.Entities;

namespace NHibernateTestAtConsole.Model
{
  public interface IUserRepository
  {
    /// <summary>
    /// Get person entity by id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>person</returns>
    User Get(Guid id);

    /// <summary>
    /// Save person entity
    /// </summary>
    /// <param name="person">person</param>
    void Save(User person);

    /// <summary>
    /// Update person entity
    /// </summary>
    /// <param name="person">person</param>
    void Update(User person);

    /// <summary>
    /// Delete person entity
    /// </summary>
    /// <param name="person">person</param>
    void Delete(User person);

    /// <summary>
    /// Row count person in db
    /// </summary>
    /// <returns>number of rows</returns>
    long RowCount();
  }
}
