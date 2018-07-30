using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalingaHub.Models
{
    public class SearchParameters
    {
        public E_SEARCHORDER OrderBy { get; set; }
        public SearchFilter Filter { get; set; }
        public SearchParameters(SearchFilter searchFilter, E_SEARCHORDER orderBy)
        {
            Filter = searchFilter;
            OrderBy = orderBy;
        }
        public SearchParameters()
        {
            OrderBy = E_SEARCHORDER.ORDER_UPVOTES;
        }

        public enum E_SEARCHORDER
        {
            ORDER_UPVOTES,
            ORDER_LATEST
        }

    }
}
