using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Model;
using TheMovies.Model.Repository;
using TheMovies.MVVM;

namespace TheMovies.ViewModel {
    public class MovieViewModel : ViewModelBase
    {
        public ObservableCollection<Movie> movies { get; set; }
        private static Movie selectedMovie { get; set; }
        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set { selectedMovie = value; OnPropertyChanged(); }
        }

        public MovieViewModel() 
        {
            movies = new ObservableCollection<Movie>(FileMovieRepository.movies);
        }

        public RelayCommand AddMovie = new RelayCommand(execute => FileMovieRepository.AddMovie(selectedMovie));        
        public RelayCommand DeleteMovie = new RelayCommand(execute => FileMovieRepository.DeleteMovie(selectedMovie));
    }
}
