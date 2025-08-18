using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model.Repository {
    public class FileMovieRepository : IMovieRepository 
    {
        public static List<Movie> movies = new List<Movie>();


        public void SaveToFile(List<Movie> movies) { }

        public void LoadFromFile () { }

        public static void AddMovie(Movie movie) 
        {
            movies.Add(movie);
        }

        public static void DeleteMovie(Movie movie)
        {
            movies.Remove(movie);
        }    

    }

}
