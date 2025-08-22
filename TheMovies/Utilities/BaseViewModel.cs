using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Utilities {
    public class BaseViewModel : INotifyPropertyChanged {
        //Event der udløses ved ændring af property, UI kan lytte og opdatere automatisk
        public event PropertyChangedEventHandler? PropertyChanged;
        /*
         * Metode kaldes når property ændres
         * [CallerMemberName] gør at navnet på property indsættet af compileren
         */
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            //Hvis Event lyttes til udløses det her
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
