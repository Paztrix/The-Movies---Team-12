using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheMovies.Model.Repository
{
    public class FileMovieRepository : IMovieRepository
    {
        private readonly string _filePath;
        public FileMovieRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();
            if (!File.Exists(_filePath)) return movies;

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var columns = line.Split(';');
                if (columns.Length < 3) continue;

                if (!TimeSpan.TryParse(columns[1], out var duration)) continue;            
                    
                movies.Add(new Movie
                {
                    Title = columns[0],
                    Duration = duration,
                    Genre = columns[2]
                });
            }
            return movies;

        }

        public void AddMovie(Movie movie)
        {
            bool fileExists = File.Exists(_filePath);
            using (var writer = new StreamWriter(_filePath, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Title;Duration;Genre");
                }
                writer.WriteLine($"{movie.Title};{movie.Duration};{movie.Genre}");
            }

        }

    }
}