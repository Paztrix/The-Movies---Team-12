using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheMovies.ViewModel;

namespace TheMovies.View {
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class AddMovieView : Window {
        public AddMovieViewModel ViewModel { get; }
        public AddMovieView() {
            InitializeComponent();
            ViewModel = new AddMovieViewModel(() => Close());
            DataContext = ViewModel;
        }
    }
}
