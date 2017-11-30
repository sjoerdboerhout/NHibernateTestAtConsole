using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTestAtConsole.Entities
{
  public interface IPropertyValue
  {
    DateTime LastModified { get; set; }
  }
}
