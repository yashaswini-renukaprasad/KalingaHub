using KalingaHub.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Business
{
    public class TagManager
    {
        readonly TagRepository _tagRepository;

        public TagManager()
        {
            _tagRepository = new TagRepository();
        }

        public List<Guid> AddTags(List<string> tags)
        {
            var tagIds = _tagRepository.AddTags(tags);
            return tagIds;
        }

        
    }
}
