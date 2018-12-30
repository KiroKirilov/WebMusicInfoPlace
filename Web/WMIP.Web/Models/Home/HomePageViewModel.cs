using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMIP.Web.Models.Albums;
using WMIP.Web.Models.Articles;

namespace WMIP.Web.Models.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<ArticleDisplayViewModel> LatestArticles { get; set; }

        public IEnumerable<ScoredAlbumViewModel> UsersMostAcclaimed { get; set; }

        public IEnumerable<ScoredAlbumViewModel> CriticsMostAcclaimed { get; set; }

        public IEnumerable<DatedAlbumViewModel> LatestReleases { get; set; }

        public IEnumerable<DatedAlbumViewModel> ComingSoon { get; set; }
    }
}
