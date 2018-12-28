using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMIP.Web.Models.Reviews
{
    public class AllReviewsViewModel
    {
        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public string SelectedFilter { get; set; }

        public IEnumerable<DisplayReviewViewModel> Reviews { get; set; }
    }
}
