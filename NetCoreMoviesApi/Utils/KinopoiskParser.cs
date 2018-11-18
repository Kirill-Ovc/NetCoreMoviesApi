using NetCoreMoviesApi.Data;
using System;
using System.Collections.Generic;
using AngleSharp;
using AngleSharp.Parser.Html;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace NetCoreMoviesApi.Utils
{
    public static class KinopoiskParser
    {
        private const string kinopoisk = "https://www.kinopoisk.ru";
        private const string urltop250 = kinopoisk  + "/top/";

        public async static Task<List<MovieInfo>> GetTopMovies()
        {
            var page = await NetworkHelper.GetPageHtml(urltop250);

            var movies = Parse250Page(page);

            return movies;
        }

        private static List<MovieInfo> Parse250Page(string pageHtml)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(pageHtml);
            var topTableRows = document.QuerySelectorAll("table.js-rum-hero table tr[id]").ToArray();

            var movies = new List<MovieInfo>();
            foreach (var item in topTableRows)
            {
                if (!item.Id.Contains("top250_place")) continue;
                var itemOrderValue = item.QuerySelector("td:nth-child(1) > a").GetAttribute("name");
                var itemNameRu = item.QuerySelector("tr a.all").TextContent;
                var pathName = item.QuerySelector("tr a.all").GetAttribute("href");
                var itemNameEng = item.QuerySelector("span.text-grey")?.TextContent;
                var itemRatingValue = item.QuerySelector("a.continue").TextContent;
                int.TryParse(itemOrderValue, out int orderNum);
                double.TryParse(itemRatingValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double rating);

                movies.Add(new MovieInfo
                {
                    Name = itemNameEng ?? itemNameRu,
                    NameRu = itemNameRu,
                    Url = kinopoisk + pathName,
                    TopOrder = orderNum,
                    Rating = rating
                });
            }

            return movies;
        }

        public static MovieInfo GetMovieByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
