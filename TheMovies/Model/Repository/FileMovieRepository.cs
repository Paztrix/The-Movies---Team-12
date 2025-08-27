using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Model;
using TheMovies.Model.Repository;

namespace TheMovies.Model.Repository {
    public class FileMovieRepository : IMovieRepository {
        private readonly string _filePath = "Filmliste.CSV";
        public FileMovieRepository(string filePath) {
            _filePath = filePath;
        }

        //Henter alle film fra CSV-filen
        public List<Movie> GetAllMovies() {
            var movies = new List<Movie>();
            //Hvis filen ikke findes så returneres en tom liste
            if (!File.Exists(_filePath)) return movies;

            //Parser til af læse CSV-filen
            using var parser = new TextFieldParser(_filePath) {
                TextFieldType = FieldType.Delimited //Angiver at filen er kommasepareret
            };
            parser.SetDelimiters(","); //Field-separator er komma
            parser.HasFieldsEnclosedInQuotes = true; //Fields kan være omsluttet af ""

            parser.ReadFields(); //Læser første linje (Header) og ignorerer den

            //Læser restenm af CSV-filen linje for linje
            while (!parser.EndOfData) {
                var fields = parser.ReadFields();
                //Spring linjen over hvis den er ugyldig (Mere end 3 fields)
                if (fields == null || fields.Length < 3) continue;

                var title = fields[0].Trim(); //Titel
                var genre = fields[1].Trim(); //Genre
                //Forsøger at parse filmens varighed i formatet hh:mm
                if (TimeSpan.TryParseExact(fields[2].Trim(), @"hh\:mm", null, out var duration)) {
                    //Tilføjer filmen til listen
                    movies.Add(new Movie(title, genre, duration));
                }
            }
            return movies;
        }

        // Tilføjer en ny film til CSV-filen
        public void AddMovie(Movie movie) {
            bool fileExists = File.Exists(_filePath);

            using (var writer = new StreamWriter(_filePath, append: true)) {
                // Hvis filen ikke findes, skriv header først
                if (!fileExists) {
                    writer.WriteLine("\"Title\",\"Genre\",\"Duration\"");
                }

                // Brug komma-separator og anførselstegn omkring tekstfelter
                writer.WriteLine($"\"{movie.Title}\",\"{movie.Genre}\",{movie.Duration:hh\\:mm}");
            }
        }
    }
}