using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PersonCar_Set_ManyToOne
{
  public class CarSet
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual PersonSet Person { get; set; }
  }

  public class CarSetMap : ClassMapping<CarSet>
  {
    public CarSetMap()
    {
      Id(x => x.Id, m => m.Generator(Generators.Identity));
      Property(x => x.Name);
      ManyToOne(x => x.Person, m =>
      {
        m.Column("PersonId");
        m.Cascade(Cascade.None);
        m.NotNullable(true);
      });
    }
  }
}