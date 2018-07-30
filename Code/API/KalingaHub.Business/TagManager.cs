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

        public void AddTags(List<string> tags)
        {
            _tagRepository.AddTags(tags);
        }
    }
}
