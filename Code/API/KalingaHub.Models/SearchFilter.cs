using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Models
{
    public class SearchFilter
    {
        private string tags;

        public String Keyword { get; set; }
        public Guid? Categories { get; set; }
        public List<string> TagList { get; set; }
        public string Tags
        {
            get => tags;
            set
            {
                tags = value;
                TagList = value == null ? null :
                            value.Split(
                              new[] { " ", "\r\n", "\r", "\n" },
                              StringSplitOptions.None
                            ).Where(x => !x.Equals("")).ToList();
            }
        }
    }

}
