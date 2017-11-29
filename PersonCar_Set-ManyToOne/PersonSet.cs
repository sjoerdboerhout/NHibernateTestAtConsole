using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode;

namespace PersonCar_Set_ManyToOne
{
  public class PersonSet
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual ICollection<CarSet> Cars { get; set; }
  }

  public class PersonSetMap : NHibernate.Mapping.ByCode.Conformist.ClassMapping<PersonSet>
  {
    public PersonSetMap()
    {
      Id(x => x.Id, m => m.Generator(Generators.Identity));
      Property(x => x.Name);
      Set(x => x.Cars, c =>
        {
          c.Key(k => { k.Column("PersonId"); });
          c.Cascade(Cascade.Persist);
          c.Lazy(CollectionLazy.NoLazy);
        }, r => { r.OneToMany(); }
      );
    }
  }
}