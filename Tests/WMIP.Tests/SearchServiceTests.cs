using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data;
using WMIP.Data.Models;
using WMIP.Data.Models.Enums;
using WMIP.Services;
using WMIP.Services.Dtos.Search;
using WMIP.Tests.Common;
using Xunit;

namespace WMIP.Tests
{
    public class SearchServiceTests : BaseTestClass
    {
        [Fact]
        public void GetResults_ReturnsCorrectResults()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Name = "Kopkame kopkame kopka", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var song2 = new Song { Name = "asdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album1 = new Album { Name = "asdasdkopkasdasdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album2 = new Album { Name = "asdasdkopasdasdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var article1 = new Article { Title = "Kopkame li dostatynchno?" };
            var article2 = new Article { Title = "znae li chovek" };
            var review1 = new Review { Title = "not enough kopkane 2/10" };
            var review2 = new Review { Title = "nz brat" };
            context.Songs.AddRange(song1, song2);
            context.Albums.AddRange(album1, album2);
            context.Articles.AddRange(article1, article2);
            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            var searchService = new SearchService(context, this.Mapper);
            var expected = new List<SearchResultDto>()
            {
                new SearchResultDto() { Title = album1.Name, SearchResultType = SearchResultType.Album },
                new SearchResultDto() { Title = song1.Name, SearchResultType = SearchResultType.Song },
                new SearchResultDto() { Title = article1.Title, SearchResultType = SearchResultType.Article },
                new SearchResultDto() { Title = review1.Title, SearchResultType = SearchResultType.Review },
            };

            // Act
            var searchResults = searchService.GetResults("kopka");

            //Assert
            Assert.Equal(expected, searchResults, new SearchComparer());
        }

        [Fact]
        public void GetResults_ReturnsEmptyCollectionIfInvalidTermIsGiven()
        {
            // Arrange
            var context = this.ServiceProvider.GetRequiredService<WmipDbContext>();
            var song1 = new Song { Name = "Kopkame kopkame kopka", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var song2 = new Song { Name = "asdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album1 = new Album { Name = "asdasdkopkasdasdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var album2 = new Album { Name = "asdasdkopasdasdasd", ApprovalStatus = ApprovalStatus.Approved, ReleaseStage = ReleaseStage.Released };
            var article1 = new Article { Title = "Kopkame li dostatynchno?" };
            var article2 = new Article { Title = "znae li chovek" };
            var review1 = new Review { Title = "not enough kopkane 2/10" };
            var review2 = new Review { Title = "nz brat" };
            context.Songs.AddRange(song1, song2);
            context.Albums.AddRange(album1, album2);
            context.Articles.AddRange(article1, article2);
            context.Reviews.AddRange(review1, review2);
            context.SaveChanges();
            var searchService = new SearchService(context, this.Mapper);
            var expected = new List<SearchResultDto>()
            {
                new SearchResultDto() { Title = album1.Name, SearchResultType = SearchResultType.Album },
                new SearchResultDto() { Title = song1.Name, SearchResultType = SearchResultType.Song },
                new SearchResultDto() { Title = article1.Title, SearchResultType = SearchResultType.Article },
                new SearchResultDto() { Title = review1.Title, SearchResultType = SearchResultType.Review },
            };

            // Act
            var searchResults = searchService.GetResults("");

            //Assert
            Assert.Empty(searchResults);
        }

        [Fact]
        public void GetResults_ReturnsEmptyCollectionOnException()
        {
            // Arrange
            var searchService = new SearchService(null, this.Mapper);

            // Act
            var searchResults = searchService.GetResults("asd");

            //Assert
            Assert.Empty(searchResults);
        }
    }

    public class SearchComparer : IEqualityComparer<SearchResultDto>
    {
        public bool Equals(SearchResultDto x, SearchResultDto y)
        {
            return x.Title.GetHashCode() + x.SearchResultType.GetHashCode() == y.Title.GetHashCode() + y.SearchResultType.GetHashCode();
        }

        public int GetHashCode(SearchResultDto obj)
        {
            // Doesn't get called
            return 1;
        }
    }
}
