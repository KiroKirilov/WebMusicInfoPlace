using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Constants.Enums;
using WMIP.Data;
using WMIP.Data.Models.Enums;
using WMIP.Services.Contracts;
using WMIP.Services.Dtos.Search;

namespace WMIP.Services
{
    public class SearchService : ISearchService
    {
        private readonly WmipDbContext context;
        private readonly IMapper mapper;

        public SearchService(WmipDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<SearchResultDto> GetResults(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<SearchResultDto>();
            }

            searchTerm = searchTerm.ToLower();

            try
            {
                var results = this.context.Albums
                    .Where(a => a.Name.ToLower().Contains(searchTerm) && a.ApprovalStatus == ApprovalStatus.Approved && a.ReleaseStage != ReleaseStage.Secret)
                    .Select(a => this.mapper.Map<SearchResultDto>(a))
                .Concat(this.context.Songs
                    .Where(s => s.Name.ToLower().Contains(searchTerm) && s.ApprovalStatus == ApprovalStatus.Approved && s.ReleaseStage != ReleaseStage.Secret)
                    .Select(a => this.mapper.Map<SearchResultDto>(a))
                .Concat(this.context.Articles
                        .Where(a => a.Title.ToLower().Contains(searchTerm))
                        .Select(a => this.mapper.Map<SearchResultDto>(a))
                 .Concat(this.context.Reviews
                        .Where(r => r.Title.ToLower().Contains(searchTerm))
                        .Select(a => this.mapper.Map<SearchResultDto>(a))))).ToList();

                return results;
            }
            catch
            {
                return new List<SearchResultDto>();
            }
        }
    }
}
