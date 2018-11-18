using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMoviesApi.Data
{
    public class MovieInfo
    {
        public string Name { get; set; }

        public string NameRu { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int VotesCount { get; set; }

        public int TopOrder { get; set; }

        public string Url { get; set; }
    }
}
