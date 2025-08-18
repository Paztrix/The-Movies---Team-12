using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Model;

namespace TheMovies.Model.Repository {
    public class FileMovieRepository : IMovieRepository {
        private readonly string _filePath = "Filmliste.CSV";
        public FileMovieRepository(string filePath) {
            _filePath = filePath;
        }

        /*
        public List<Movie> GetAllMovies() {
            var movies = new List<Movie>();
            if (!File.Exists(_filePath)) return movies;

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines.Skip(1)) {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var columns = line.Split(';');
                if (columns.Length < 3) continue;

                if (!TimeSpan.TryParse(columns[1], out var duration)) continue;

                movies.Add(new Movie {
                    Title = columns[0],
                    Duration = duration,
                    Genre = columns[2]
                });
            }
            return movies;
        }
        */

        public List<Movie> GetAllMovies() {
            try {
                using(StreamReader sr = new StreamReader(_filePath)) {
                    List<Movie> movies = new List<Movie>();
                    string line;

                    while((line = sr.ReadLine()) != null) {
                        if(!string.IsNullOrEmpty(line)) {
                            movies.Add(Movie.FromString(line));
                        }
                    }
                    return movies;
                }
            } catch(IOException ex) {
                Console.WriteLine($"Error reading from file: {ex.Message}");
                return new List<Movie>();
            }
        }

        public void AddMovie(Movie movie) {
            bool fileExists = File.Exists(_filePath);
            using (var writer = new StreamWriter(_filePath, append: true)) {
                if (!fileExists) {
                    writer.WriteLine($"{movie.Title};{movie.Duration:hh\\:mm};{movie.Genre}");
                }
                writer.WriteLine($"{movie.Title};{movie.Duration};{movie.Genre}");
            }

        }
    }
}