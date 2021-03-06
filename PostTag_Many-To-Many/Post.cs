﻿using System.Collections.Generic;
using NHibernate.Mapping.ByCode;

namespace PostTag_Many_To_Many
{
  public class Post
  {
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual IList<Tag> Tags { get; set; } = new List<Tag>();

    public override string ToString()
    {
      string tags = "";
      foreach (var tag in Tags)
      {
        tags += "-- " + tag.Name + "\n";
      }
      tags = tags.Trim();

      return string.Format("POST: UUID: {0}, Name: {1}, Nr of tags: {2}, Tags:\n{3}",
        Id,
        Name,
        Tags.Count,
        tags);
    }
  }

  public class PostMap : NHibernate.Mapping.ByCode.Conformist.ClassMapping<Post>
  {
    public PostMap()
    {
      Id(
        t => t.Id,
        t =>
        {
          t.Generator(Generators.HighLow, g => g.Params(new {max_low = 100}));
          t.Column("post_id");
        });

      Property(x => x.Name);

      Bag(t => t.Tags, bag =>
        {
          bag.Table("tags_posts");
          bag.Cascade(Cascade.None);
          bag.Lazy(CollectionLazy.NoLazy);
          bag.Key(k => k.Column("post_id"));
        },
        t => t.ManyToMany(c => { c.Column("tag_id"); }));
    }
  }
}