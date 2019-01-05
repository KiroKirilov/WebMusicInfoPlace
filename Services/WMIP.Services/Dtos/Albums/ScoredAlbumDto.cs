using AutoMapper;
using NewsSystem.Common.Mapping.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMIP.Data.Models;

namespace WMIP.Services.Dtos.Albums
{
    public class ScoredAlbumDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double AverageScore { get; set; }

        public string ArtistName { get; set; }
    }
}
