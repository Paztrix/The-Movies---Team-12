using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model {
    public class Movie {
        public string Title { get; set; }
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }

        public Movie(string title, string genre, TimeSpan duration) {
            Title = title;
            Genre = genre;
            Duration = duration;
        }

        public override string ToString() {
            return $"{Title},{Genre},{Duration}";
        }

        public static Movie FromString(string line) {
            var parts = line.Split(",");
            string title = parts[0].Trim().Trim('"');
            string genre = parts[1].Trim().Trim('"');
            TimeSpan duration = TimeSpan.ParseExact(parts[2].Trim(), @"hh\:mm", null);

            return new Movie(title, genre, duration);
        }
    }
}
