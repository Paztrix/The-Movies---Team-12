using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheMovies.Model;
using TheMovies.Model.Repository;
using TheMovies.Utilities;

namespace TheMovies.ViewModel {
    public class MovieViewModel : BaseViewModel {
        private readonly IMovieRepository _movieRepository;
        private string _title;
        private string _genre;
        private TimeSpan _duration;

        public ObservableCollection<Movie> Movies { get; }

        public string Title {
            get => _title;
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                    AddMovieCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Genre {
            get => _genre;
            set {
                if (_genre != value) {
                    _genre = value;
                    OnPropertyChanged(nameof(Genre));
                    AddMovieCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public TimeSpan Duration {
            get => _duration;
            set {
                if (_duration != value) {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                    AddMovieCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand AddMovieCommand { get; }

        public MovieViewModel(IMovieRepository movieRepository) {
            _movieRepository = movieRepository;
            Movies = new ObservableCollection<Movie>(_movieRepository.GetAllMovies());
            AddMovieCommand = new RelayCommand(execute => AddMovie(), canExecute => CanAddMovie());
        }

        public MovieViewModel() : this(new FileMovieRepository("Filmliste.CSV")) { }

        public void AddMovie() {
            if(!CanAddMovie()) {
                return;
            }

            var newMovie = new Movie(Title, Duration, Genre);

            _movieRepository.AddMovie(newMovie);
            Movies.Add(newMovie);
        }

        public bool CanAddMovie() {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Genre) && Duration > TimeSpan.Zero;
        }
    }
}