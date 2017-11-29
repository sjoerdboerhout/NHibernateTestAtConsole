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
      foreach (var tag in Posts)
      {
        posts += "-- " + tag + "\n";
      }
      posts = posts.Trim();

      return string.Format("\n-UUID: {0}\n-Name: {1}\n-Nr of posts: {2}\n-Posts: {3}\n",
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
        bag.Key(k => k.Column("tag_id"));
      }, 
      t => t.ManyToMany(c =>
      {
        c.Column("post_id");
      }));
    }
  }
}