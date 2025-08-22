using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model {


    public class Movie
    {
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }

        public Movie(string title, TimeSpan duration, string genre)
        {
            Title = title;
            Duration = duration;
            Genre = genre;
        }

        public override string ToString()
        {
            return string.Join(";",
                Title,
                Duration.ToString(@"hh\:mm"),
                Genre
            );

        }

        public static Movie FromString(string line)
        {
            var parts = line.Split(';');

            if (parts.Length != 3 ||
                !TimeSpan.TryParseExact(parts[1], @"hh\:mm", null, out TimeSpan duration))

            {
                throw new ArgumentException("Invalid movie format", nameof(line));
            }

            return new Movie(parts[0], duration, parts[2]);

        }
    }
}
