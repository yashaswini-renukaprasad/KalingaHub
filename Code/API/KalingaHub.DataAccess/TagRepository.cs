using KalingaHub.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.DataAccess
{
    public class TagRepository
    {
        public List<Guid> AddTags(List<string> tags)
        {
            try
            {
                var tagIds = new List<Guid>();

                using (var db = new KalingaHubDBModel())
                {
                    var qry = from t in db.Tags
                              select t.Name;
                    foreach (var obj in tags)
                    {
                        if (!qry.Contains(obj))
                        {
                            Tag tag = new Tag { Id = Guid.NewGuid(), Name = obj };
                            tagIds.Add(tag.Id);
                            db.Tags.Add(tag);
                        }
                    }

                    db.SaveChanges();
                    return tagIds;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Tag> GetTags(List<string> tagNames)
        {
            List<Tag> tags;
            using (var db = new KalingaHubDBModel())
            {
                tags = (from t in db.Tags
                              where tagNames.Contains(t.Name.ToLower())
                              select t).ToList();
            }
            return tags;
        }

    }
}
