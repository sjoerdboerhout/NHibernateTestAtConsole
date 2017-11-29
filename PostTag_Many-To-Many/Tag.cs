using System;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PostTag_Many_To_Many
{
  public class Tag
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual IList<Post> Posts { get; set; } = new List<Post>();

    public override string ToString()
    {
      string posts = "";
      foreach (var post in Posts)
      {
        posts += "-- " + post.Name + "\n";
      }
      posts = posts.Trim();

      return string.Format("TAG: UUID: {0}, Name: {1}, Nr of posts: {2}, Posts:\n{3}",
        Id,
        Name,
        Posts.Count,
        posts);
    }
  }

  public class TagMap : ClassMapping<Tag>
  {
    public TagMap()
    {
      Id(
        t => t.Id,
        t =>
        {
          t.Generator(Generators.HighLow, g => g.Params(new {max_low = 100}));
          t.Column("tag_id");
        });

      Property(x => x.Name);

      Bag(t => t.Posts, bag =>
        {
          bag.Table("tags_posts");
          bag.Cascade(Cascade.None);
          bag.Lazy(CollectionLazy.NoLazy);
          bag.Key(k => k.Column("tag_id"));
        },
        t => t.ManyToMany(c => { c.Column("post_id"); }));
    }
  }
}