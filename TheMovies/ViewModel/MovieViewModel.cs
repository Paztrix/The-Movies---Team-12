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
using TheMovies.View;

namespace TheMovies.ViewModel {
    public class MovieViewModel : BaseViewModel {
        private readonly IMovieRepository _movieRepository;
        private string _title;
        private string _genre;
        private TimeSpan _duration;

        //ObservableCollection af film som UI kan binde til
        public ObservableCollection<Movie> Movies { get; }

        public string Title {
            get => _title;
            set {
                if (_title != value) {
                    _title = value;
                    OnPropertyChanged(nameof(Title)); //Opdatere UI
                }
            }
        }
        public string Genre {
            get => _genre;
            set {
                if (_genre != value) {
                    _genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }
        public TimeSpan Duration {
            get => _duration;
            set {
                if (_duration != value) {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        //Relay command til at tilføje film
        public RelayCommand AddMovieCommand { get; }

        //Constructor med repository som parameter
        public MovieViewModel(IMovieRepository movieRepository) {
            _movieRepository = movieRepository;
            //Henter film of tilføjer dem til ObservableCollection
            Movies = new ObservableCollection<Movie>(_movieRepository.GetAllMovies());
            //Initialisere Command med metode til AddMovie og tjekke om CanAddMovie
            AddMovieCommand = new RelayCommand(execute => AddMovie(), canExecute => CanAddMovie());
        }

        //Standard constructor med fil-baseret repossitory
        public MovieViewModel() : this(new FileMovieRepository("Filmliste.CSV")) { }

        public void AddMovie() {
            //Opretter et nyt vindue til at tilføje en film
            var addMovieView = new AddMovieView {
                Owner = Application.Current.MainWindow //Sætter hovedvinduet som ejer
            };

            //Viser vinduet som modal dialog (Vindue der vises ovenpå eksisterende vindue, som brugeren skal interagere med før de kan vende tilbage)
            var result = addMovieView.ShowDialog();

            //Tjekker om brugeren trykkede OK eller blev bekræftet gennem ViewModel
            if (result == true || addMovieView.ViewModel.IsConfirmed) {
                //Forsøger at parse duration fra string til TimeSpan
                if (TimeSpan.TryParse(addMovieView.ViewModel.Duration, out var duration)) {
                    var newMovie = new Movie(
                        addMovieView.ViewModel.Title,
                        addMovieView.ViewModel.Genre,
                        duration
                    );

                    _movieRepository.AddMovie(newMovie);
                    Movies.Add(newMovie);
                } else {
                    MessageBox.Show("Ugyldig varighed", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Tjekker om en film kan tilføjes, ved at se om alle felter er udfyldt
        public bool CanAddMovie() {
            return true;
        }
    }
}