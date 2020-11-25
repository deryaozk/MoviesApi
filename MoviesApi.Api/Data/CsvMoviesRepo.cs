using CsvHelper;
using MoviesApi.Api.Dtos;
using MoviesApi.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MoviesApi.Api.Data
{
    public class CsvMoviesRepo : IMoviesRepo
    {
        public IList<Movie> GetMoviesByMovieId(int id)
        {
            using (TextReader reader = new StreamReader("metadata.csv"))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<Movie>().Where(x => x.MovieId == id).GroupBy(x => new { x.MovieId, x.Language })
                .Select(group => new Movie
                {
                    Id = group.Last().Id,
                    MovieId = group.Key.MovieId,
                    Title = group.Last().Title,
                    Language = group.Key.Language,
                    Duration = group.Last().Duration,
                    ReleaseYear = group.Last().ReleaseYear
                })
                .OrderBy(item => item.Language).ToList();
                
                return records.ToList();
            }         
        }

        public IEnumerable<MovieStatDto> GetMoviesStatics()
        {
            var statList = new List<MovieStatDto>();

            using (TextReader readerStat = new StreamReader("stats.csv"))
            using (var csvReaderStat = new CsvReader(readerStat, CultureInfo.InvariantCulture))
            {
                csvReaderStat.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                var recordsStat = csvReaderStat.GetRecords<MovieStat>().GroupBy(x => x.MovieId).Select(group => new
                {
                    MovieId = group.Key,
                    Count = group.Count(),
                    WatchDurationS = TimeSpan.FromMilliseconds(group.Sum(s => s.WatchDurationMs)).TotalSeconds / group.Count(),

                }).OrderByDescending(x => x.Count).ToList();

                using (TextReader reader = new StreamReader("metadata.csv"))
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture, true))
                {
                    var moviesRecord = csvReader.GetRecords<Movie>().ToList();
                    foreach (var stat in recordsStat)
                    {
                        var record = moviesRecord.Where(x => x.MovieId == stat.MovieId).GroupBy(x => new { x.MovieId, x.ReleaseYear })
                            .OrderByDescending(item => item.Key.ReleaseYear)
                            .Select(group => new MovieStatDto
                            {
                                MovieId = group.Key.MovieId,
                                Title = group.First().Title,
                                ReleaseYear = group.First().ReleaseYear,
                                AverageWatchDurationS = (int)(stat.WatchDurationS),
                                Watches = stat.Count
                            }).FirstOrDefault();

                        if (record != null)
                        {
                            statList.Add(record);
                        }
                    }
                }

                return statList.OrderByDescending(x => x.ReleaseYear).OrderByDescending(y => y.Watches);
            }           
        }

        public void CreateMovie(Movie movie)
        {
            if(movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }
            var database = new List<Movie>();
            database.Add(movie);
        }

    }
}
